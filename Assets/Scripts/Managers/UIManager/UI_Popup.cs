using DG.Tweening;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

public abstract class UI_Popup : UI_Base
{
    #region Birth
    private Sequence Popup_Birth()
    {
        return Utility.RecyclableSequence()
            .Append(Popup.DOScale(1.0f, 0.2f).From(0.0f).SetEase(Ease.OutBack));
    }
    #endregion

    public bool Interactable { get { return canvasGroup.interactable; } }
    public int SortingOrder { get { return canvas.sortingOrder; } set { canvas.sortingOrder = value; } }

    protected RectTransform Popup;

    private Canvas canvas;

    protected override void Initialize()
    {
        base.Initialize();

        Popup = gameObject.FindComponent<RectTransform>(nameof(Popup));
        canvas = GetComponent<Canvas>();

        BindSequences(State.Birth, Popup_Birth);
    }

    public override void Death()
    {
        base.Death();
        Destroy();
    }
}