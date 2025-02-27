using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_TimeBar : UI_SubItem
{
    #region Birth
    private Sequence Frame_Birth()
    {
        var child = Get((int)Children.Frame);

        return Utility.RecyclableSequence()
            .Append(child.DOAnchorPosY(200.0f, 0.5f).From().OnComplete(Stand));
    }
    #endregion
    #region Death
    private Sequence Frame_Death()
    {
        var child = Get((int)Children.Frame);

        return Utility.RecyclableSequence()
            .Append(child.DOAnchorPosY(200.0f, 0.5f).OnComplete(Destroy));
    }
    #endregion

    private enum Children
    {
        Frame,
        Text
    }

    private bool hasTutorial = true;
    private float timer = Define.TIMER;

    private TMP_Text time;
    private Sequence timerStand;
    private Sequence timerWarn;

    private int previousSeconds;
    private Color initialColor;

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        BindSequences(State.Birth, Frame_Birth);
        BindSequences(State.Death, Frame_Death);

        time = Get<TMP_Text>((int)Children.Text);

        timerStand = Utility.RecyclableSequence()
            .Append(Get((int)Children.Text).DOPunchScale(0.2f * Vector2.one, 0.2f));

        var grahpic = Get<Graphic>((int)Children.Text);
        initialColor = grahpic.color;
        timerWarn = Utility.RecyclableSequence()
            .Append(grahpic.DOColor(Color.red, 0.5f).From(initialColor));

        DOVirtual.DelayedCall(1.0f, Tutorial);
    }

    protected override void Deinitialize()
    {
        base.Deinitialize();

        timerStand.Kill();
        timerWarn.Kill();

        Managers.Scene.GetCurrentScene<Scene_Lobby>().GameStart();
    }

    private void Update()
    {
        if (IsDead || hasTutorial)
        {
            return;
        }

        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
            int seconds = (int)timer;

            if (seconds != previousSeconds)
            {
                if (seconds <= 10)
                {
                    timerWarn.Restart();
                }

                timerStand.Restart();
                previousSeconds = seconds;
            }
        }
        else
        {
            timer = 0.0f;
            Death();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        string decimalPart = (timer % 1).ToString("F1");
        decimalPart = decimalPart[(decimalPart.IndexOf('.') + 1)..];

        time.text = $"00:{(int)timer:00}.<size=42>{decimalPart}</size>";
    }

    private void Tutorial()
    {
        UI_Tutorial tutorial = Managers.UI.Show<UI_Tutorial>();
        tutorial.SetMessage(Define.Tutorial_GameStart);
        tutorial.OnDestoryed += () =>
        {
            hasTutorial = false;

            Managers.Resource.Instantiate(nameof(ChestSpawner), null, Managers.Game.Player.transform.position);
            Managers.UI.Show<UI_Lobby>();
        };

        UpdateUI();
    }
}