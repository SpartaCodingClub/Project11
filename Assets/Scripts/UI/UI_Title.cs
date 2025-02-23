using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Title : UI_Scene
{
    #region Birth
    private Sequence SubTitle_Birth()
    {
        RectTransform child = Get((int)Children.SubTitle);
        CanvasGroup canvasGroup = child.GetComponent<CanvasGroup>();

        return Utility.RecyclableSequence()
            .Append(child.DOAnchorPosY(-100.0f, 1.0f).From(true))
            .Join(canvasGroup.DOFade(1.0f, 1.0f).From(0.0f));
    }

    private Sequence Button_Start_Birth()
    {
        Graphic graphic = Get<Graphic>((int)Children.Button_Start);

        return Utility.RecyclableSequence()
            .Append(graphic.DOFade(0.0f, 1.0f).From(1.0f));
    }

    private Sequence Line_Top_Birth()
    {
        RectTransform child = Get((int)Children.Line_Top);
        CanvasGroup canvasGroup = child.GetComponent<CanvasGroup>();
        float endValue = Get((int)Children.Text_Start).anchoredPosition.y;

        return Utility.RecyclableSequence()
            .Append(canvasGroup.DOFade(1.0f, 0.5f).From(0.0f))
            .Join(child.DOAnchorPosY(endValue, 1.0f).From().SetEase(Ease.OutBack));
    }

    private Sequence Text_Start_Birth()
    {
        Graphic graphic = Get<Graphic>((int)Children.Text_Start);

        return Utility.RecyclableSequence()
            .Append(graphic.DOFade(1.0f, 1.0f).From(0.0f));
    }

    private Sequence Line_Bottom_Birth()
    {
        RectTransform child = Get((int)Children.Line_Bottom);
        Graphic graphic = child.GetComponent<Graphic>();
        float endValue = Get((int)Children.Text_Start).anchoredPosition.y;

        return Utility.RecyclableSequence()
            .Append(graphic.DOFade(1.0f, 0.5f).From(0.0f))
            .Join(child.DOAnchorPosY(endValue, 1.0f).From().SetEase(Ease.OutBack).OnComplete(Stand));
    }
    #endregion
    #region Stand
    private Sequence Text_Start_Stand()
    {
        Graphic grahpic = Get<Graphic>((int)Children.Text_Start);

        return Utility.RecyclableSequence()
            .Append(grahpic.DOFade(0.0f, 1.0f).SetDelay(1.0f))
            .Append(grahpic.DOFade(1.0f, 1.0f));
    }
    #endregion
    #region Death
    private Sequence Background_Death()
    {
        var child = Get((int)Children.Background);

        return Utility.RecyclableSequence()
            .Append(child.DOScale(3.0f, 1.0f).OnComplete(Destroy))
            .Join(canvasGroup.DOFade(0.0f, 1.0f));
    }

    private Sequence Line_Top_Death()
    {
        RectTransform child = Get((int)Children.Line_Top);
        float endValue = Get((int)Children.Text_Start).anchoredPosition.y;

        return Utility.RecyclableSequence()
            .Append(child.DOAnchorPosY(endValue, 0.5f).SetEase(Ease.InBack));
    }

    private Sequence Text_Start_Death()
    {
        Graphic graphic = Get<Graphic>((int)Children.Text_Start);

        return Utility.RecyclableSequence()
            .Append(graphic.DOFade(0.0f, 0.5f));
    }

    private Sequence Line_Bottom_Death()
    {
        RectTransform child = Get((int)Children.Line_Bottom);
        float endValue = Get((int)Children.Text_Start).anchoredPosition.y;

        return Utility.RecyclableSequence()
            .Append(child.DOAnchorPosY(endValue, 0.5f).SetEase(Ease.InBack));
    }
    #endregion

    private enum Children
    {
        Background,
        SubTitle,
        Button_Start,
        Line_Top,
        Text_Start,
        Line_Bottom
    }

    private readonly WaitForSeconds delay = new(2.0f);
    private readonly WaitForSeconds interval = new(0.2f);

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        BindSequences(State.Birth, SubTitle_Birth, Button_Start_Birth);
        BindSequences(State.Birth, Line_Top_Birth, Text_Start_Birth, Line_Bottom_Birth);
        BindSequences(State.Stand, Text_Start_Stand);
        BindSequences(State.Death, Line_Top_Death, Text_Start_Death, Line_Bottom_Death);
        BindSequences(State.Death, Background_Death);

        BindEvent((int)Children.Button_Start, Death);

        Canvas canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Managers.Camera.Main;
    }

    public override void Stand()
    {
        base.Stand();
        StartCoroutine(Lightning());
    }

    public override void Death()
    {
        base.Death();
        StopAllCoroutines();

        Managers.Audio.Play(Clip.SoundFX_Start);
        Managers.Scene.GetCurrentScene<Scene_Title>().Clear();
    }

    public override void Destroy()
    {
        base.Destroy();
        Managers.Scene.LoadScene<Scene_Lobby>();
    }

    private IEnumerator Lightning()
    {
        while (true)
        {
            yield return delay;

            int repeat = 0;
            switch (Random.Range(0, 10))
            {
                case 0:
                case 1:
                    repeat = 1;
                    break;
                case 2:
                    repeat = 2;
                    break;
            }

            for (int i = 0; i < repeat; i++)
            {
                Managers.UI.Show<UI_Flash>();
                yield return interval;
            }
        }
    }
}