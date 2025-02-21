using UnityEngine;

public class Define
{
    #region Animator
    public static readonly int Birth = Animator.StringToHash(nameof(Birth));
    public static readonly int Stand = Animator.StringToHash(nameof(Stand));
    public static readonly int Death = Animator.StringToHash(nameof(Death));
    public static readonly int Attack = Animator.StringToHash(nameof(Attack));
    public static readonly int Move = Animator.StringToHash(nameof(Move));
    public static readonly int Jump = Animator.StringToHash(nameof(Jump));
    #endregion
    #region Name
    public static readonly string MainRenderer = nameof(MainRenderer);
    #endregion
    #region Path Type
    public const string AUDIO = "Audio";
    public const string OBJECT = "Object";
    public const string UI = "UI";
    #endregion
}