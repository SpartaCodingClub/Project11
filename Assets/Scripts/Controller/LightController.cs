using DG.Tweening;

public class LightController : BaseController
{
    #region Birth
    private Sequence Light_Stand()
    {
        float initialScale = transform.localScale.x;

        return Utility.RecyclableSequence()
            .Append(transform.DOScale(initialScale * 0.8f, 0.5f))
            .Append(transform.DOScale(initialScale * 1.0f, 0.5f));
    }
    #endregion

    protected override void Initialize()
    {
        base.Initialize();

        BindSequences(State.Stand, Light_Stand);
        Stand();
    }
}