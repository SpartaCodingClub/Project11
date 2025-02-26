using UnityEngine;

public class Define
{
    #region Animator
    public static readonly int Stand = Animator.StringToHash(nameof(Stand));
    public static readonly int Death = Animator.StringToHash(nameof(Death));

    public static readonly int Attack = Animator.StringToHash(nameof(Attack));
    public static readonly int Move = Animator.StringToHash(nameof(Move));
    public static readonly int Jump = Animator.StringToHash(nameof(Jump));

    public static readonly int AttackSpeed = Animator.StringToHash(nameof(AttackSpeed));
    public static readonly int PosX = Animator.StringToHash(nameof(PosX));
    public static readonly int PosY = Animator.StringToHash(nameof(PosY));
    public static readonly int HasSlow = Animator.StringToHash(nameof(HasSlow));
    #endregion
    #region Name
    public static readonly string Horizontal = nameof(Horizontal);
    public static readonly string Vertical = nameof(Vertical);
    public static readonly string Monster = nameof(Monster);
    public static readonly string Obstacle = nameof(Obstacle);
    public static readonly string Player = nameof(Player);
    public static readonly string Melee = nameof(Melee);
    public static readonly string Bullet = nameof(Bullet);
    public static readonly string MainRenderer = nameof(MainRenderer);
    #endregion
    #region PathType
    public const string AUDIO = "Audio";
    public const string OBJECT = "Object";
    public const string PROJECTILE = "Projectiles";
    public const string UI = "UI";
    #endregion
}