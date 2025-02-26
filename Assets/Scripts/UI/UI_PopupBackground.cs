using DG.Tweening;

public class UI_PopupBackground : UI_SubItem
{
    #region Birth
    private Sequence Background_Birth()
    {
        return Utility.RecyclableSequence()
            .Append(canvasGroup.DOFade(1.0f, 1.0f).From(0.0f).OnComplete(Stand));
    }
    #endregion
    #region Death
    private Sequence Background_Death()
    {
        return Utility.RecyclableSequence()
            .Append(canvasGroup.DOFade(0.0f, 1.0f).From(1.0f).OnComplete(Destroy));
    }
    #endregion

    private enum Children
    {
        Background
    }

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        BindSequences(State.Birth, Background_Birth);
        BindSequences(State.Death, Background_Death);
    }
}