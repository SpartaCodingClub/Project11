using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_WorldSpace
{
    #region Birth
    private Sequence Slider_Birth()
    {
        var child = Get((int)Children.Slider);

        return Utility.RecyclableSequence()
            .Append(child.DOScale(1.0f, 0.5f).From(0.0f).SetEase(Ease.OutBack).OnComplete(Stand));
    }
    #endregion
    #region Death
    private Sequence Slider_Death()
    {
        var child = Get((int)Children.Slider);

        return Utility.RecyclableSequence()
            .Append(child.DOScale(0.0f, 0.5f).OnComplete(Destroy));
    }
    #endregion

    private enum Children
    {
        Slider,
        Text_Value
    }

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        BindSequences(State.Birth, Slider_Birth);
        BindSequences(State.Death, Slider_Death);
    }

    public void UpdateUI(float hp, float maxHp)
    {
        Get<Slider>((int)Children.Slider).value = hp / maxHp;
        Get<TMP_Text>((int)Children.Text_Value).text = Mathf.Ceil(hp).ToString("N0");
    }
}