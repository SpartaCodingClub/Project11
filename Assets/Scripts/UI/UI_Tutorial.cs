using DG.Tweening;
using UnityEngine.UI;

public class UI_Tutorial : UI_SubItem
{
    #region Birth
    private Sequence Tutorial_Birth()
    {
        return Utility.RecyclableSequence()
            .Append(canvasGroup.DOFade(1.0f, 1.0f));
    }

    private Sequence Text_Birth()
    {
        return Utility.RecyclableSequence();
    }

    private Sequence Character_Birth()
    {
        var child = Get((int)Children.Character);

        return Utility.RecyclableSequence()
            .Append(child.DOAnchorPosX(-500.0f, 1.0f).From());
    }
    #endregion
    #region Stand
    private Sequence Button_Stand()
    {
        var graphic = Get<Graphic>((int)Children.Button);

        return Utility.RecyclableSequence()
            .Append(graphic.DOFade(0.0f, 1.0f).SetDelay(1.0f))
            .Append(graphic.DOFade(1.0f, 1.0f));
    }
    #endregion
    #region Death
    private Sequence Tutorial_Death()
    {
        return Utility.RecyclableSequence();
    }

    private Sequence Character_Death()
    {
        return Utility.RecyclableSequence();
    }
    #endregion

    private enum Children
    {
        Frame,
        Button,
        Text,
        Character
    }

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        BindSequences(State.Birth, Tutorial_Birth, Text_Birth, Character_Birth);
        BindSequences(State.Stand, Button_Stand);
        BindSequences(State.Death, Tutorial_Death, Character_Death);
    }
}