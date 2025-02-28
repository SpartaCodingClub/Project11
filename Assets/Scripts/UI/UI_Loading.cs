using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Loading : UI_SubItem
{
    #region Birth
    private Sequence Loading_Birth()
    {
        return Utility.RecyclableSequence()
            .Append(canvasGroup.DOFade(1.0f, 1.0f).From(0.0f).OnComplete(Stand));
    }

    private Sequence Animation_Loading_Birth()
    {
        var child = Get((int)Children.Animation_Loading);

        return Utility.RecyclableSequence()
            .Append(child.DOScale(1.0f, 0.5f).From(0.0f).SetDelay(0.5f).SetEase(Ease.OutBack));
    }

    private Sequence Slider_Loading_Birth()
    {
        var child = Get((int)Children.Slider_Loading);

        return Utility.RecyclableSequence()
            .Append(child.DOAnchorPosY(-100.0f, 1.0f).From().SetEase(Ease.OutBack));
    }
    #endregion
    #region Death
    private Sequence Loading_Death()
    {
        return Utility.RecyclableSequence()
            .Append(canvasGroup.DOFade(0.0f, 1.0f).OnComplete(Destroy));
    }

    private Sequence Animation_Loading_Death()
    {
        var child = Get((int)Children.Animation_Loading);

        return Utility.RecyclableSequence()
            .Append(child.DOScale(0.0f, 0.5f).SetDelay(0.5f).SetEase(Ease.InBack));
    }

    private Sequence Slider_Loading_Death()
    {
        var child = Get((int)Children.Slider_Loading);

        return Utility.RecyclableSequence()
            .Append(child.DOAnchorPosY(-100.0f, 1.0f).SetEase(Ease.InBack));
    }
    #endregion

    private enum Children
    {
        Animation_Loading,
        Slider_Loading,
        Text_Info
    }

    private float loadingTimer;
    private Slider slider;
    private TMP_Text loadingText;

    private readonly WaitForSeconds interval = new(0.05f);

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        BindSequences(State.Birth, Loading_Birth, Animation_Loading_Birth, Slider_Loading_Birth);
        BindSequences(State.Death, Loading_Death, Animation_Loading_Death, Slider_Loading_Death);

        slider = Get<Slider>((int)Children.Slider_Loading);
        loadingText = Get<TMP_Text>((int)Children.Text_Info);
    }

    public override void Stand()
    {
        base.Stand();
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        Time.timeScale = 1.0f;

        while (loadingTimer < 1.0f)
        {
            loadingTimer += Time.unscaledDeltaTime;
            if (loadingTimer > 0.99f)
            {
                loadingTimer = 1.0f;
                yield return new WaitForSeconds(1.0f);
            }

            slider.value = loadingTimer;
            loadingText.text = $"NOW LOADING... {loadingTimer * 100.0f:F2}%";
            yield return interval;
        }

        Managers.Scene.LoadScene<Scene_Game>();
        yield return new WaitForSeconds(0.5f);

        Death();
    }
}