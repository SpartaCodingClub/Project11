using DG.Tweening;
using UnityEngine;

public class UI_GameStart : UI_SubItem
{
    #region Birth
    private Sequence Button_Birth()
    {
        var child = Get((int)Children.Button);

        return Utility.RecyclableSequence()
            .Append(child.DOAnchorPosY(-400.0f, 1.0f).From().SetEase(Ease.OutBack).OnComplete(Stand));
    }
    #endregion
    #region Stand
    private Sequence Icon_Stand()
    {
        var child = Get((int)Children.Icon);

        return Utility.RecyclableSequence()
            .Append(child.DOPunchScale(0.1f * Vector2.one, 0.5f).SetDelay(2.5f));
    }
    #endregion
    #region Death
    private Sequence Button_Death()
    {
        var child = Get((int)Children.Button);

        return Utility.RecyclableSequence()
            .Append(child.DOAnchorPosY(-400.0f, 0.5f).OnComplete(Destroy));
    }
    #endregion

    private enum Children
    {
        Button,
        Icon
    }

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        BindSequences(State.Birth, Button_Birth);
        BindSequences(State.Stand, Icon_Stand);
        BindSequences(State.Death, Button_Death);

        BindEvent((int)Children.Button, Death);
    }

    public override void Death()
    {
        base.Death();
        Managers.UI.Show<UI_TimeBar>();
    }
}