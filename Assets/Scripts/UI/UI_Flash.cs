using DG.Tweening;
using UnityEngine.UI;

public class UI_Flash : UI_SubItem
{
    #region Birth
    private Sequence Background_Birth()
    {
        Graphic graphic = Get<Graphic>((int)Children.Background);

        return Utility.RecyclableSequence()
            .Append(graphic.DOFade(0.0f, 0.2f).From(1.0f).OnComplete(Destroy));
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
    }
}