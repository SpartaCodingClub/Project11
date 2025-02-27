using DG.Tweening;
using TMPro;
using UnityEngine;

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

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        BindSequences(State.Birth, Frame_Birth);
        BindSequences(State.Death, Frame_Death);

        time = Get<TMP_Text>((int)Children.Text);
        Tutorial();
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

        time.text = $"00:{(int)timer}.<size=42>{decimalPart}</size>";
    }

    private void Tutorial()
    {
        UI_Tutorial tutorial = Managers.UI.Show<UI_Tutorial>();
        tutorial.SetMessage(Define.Tutorial_GameStart);
        tutorial.OnDestoryed += () =>
        {
            hasTutorial = false;
            Get((int)Children.Text).DOScale(2.0f, 1.0f).From();
        };

        UpdateUI();
    }
}