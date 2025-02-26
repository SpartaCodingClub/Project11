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
    #region Message
    public static readonly string[] Tutorial_Move =
    {
        "안녕하세요. 생존자님\n\n" +
        "W, A, S, D와 방향키로\n" +
        "캐릭터를 움직일 수 있습니다.",
    };

    public static readonly string[] Tutorial_Jump =
    {
        "스페이스바를 누르면\n" +
        "점프를 할 수 있습니다.\n",

        "한 가지 팁을 드리자면,\n" +
        "특정 장애물은 점프로\n" +
        "뛰어 넘을 수도 있습니다.",

        "직접 시도해 보세요."
    };

    public static readonly string[] Tutorial_Explore =
    {
        "준비가 되면, 생존을 위한\n" +
        "탐험을 시작하세요.",

        "그럼 행운을 빕니다.",
    };

    public static readonly string[] Tutorial_GameStart =
    {
        "게임이 시작ㄷ",
    };
    #endregion
    #region Name
    public static readonly string Horizontal = nameof(Horizontal);
    public static readonly string Vertical = nameof(Vertical);
    public static readonly string Monster = nameof(Monster);
    public static readonly string Obstacle = nameof(Obstacle);
    public static readonly string Player = nameof(Player);
    public static readonly string Melee = nameof(Melee);
    public static readonly string Bullet = nameof(Bullet);
    public static readonly string Effect = nameof(Effect);
    public static readonly string MainRenderer = nameof(MainRenderer);
    #endregion
    #region Path
    public const string AUDIO = "Audio";
    public const string OBJECT = "Object";
    public const string PROJECTILE = "Projectile";
    public const string UI = "UI";
    #endregion
    #region Value
    public static readonly float TIMER = 30.0f;
    #endregion
}