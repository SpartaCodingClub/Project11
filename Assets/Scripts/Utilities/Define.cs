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
    #region Axis
    public static readonly string Horizontal = nameof(Horizontal);
    public static readonly string Vertical = nameof(Vertical);
    #endregion
    #region Tutorial
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
        "이제 곧 탐험이 시작됩니다.\n\n" +
        "떠나시기 전에,\n" +
        "맵 곳곳을 살펴보세요.",

        "운이 좋다면,\n" +
        "보너스를 획득할 수 있습니다.",

        "아자아자, 파이팅!\n" +
        "할 수 있다!",

        "곧 타이머가 시작됩니다!"
    };
    #endregion
    #region Layer
    public static readonly string Boss = nameof(Boss);
    public static readonly string Monster = nameof(Monster);
    public static readonly string Obstacle = nameof(Obstacle);
    public static readonly string Player = nameof(Player);
    #endregion
    #region Object
    public static readonly string MainRenderer = nameof(MainRenderer);
    #endregion
    #region Path
    public const string AUDIO = "Audio";
    public const string EFFECT = "Effects";
    public const string ENEMIES = "Enemies";
    public const string ITEMS = "Items";
    public const string MAP = "Map";
    public const string OBJECT = "Objects";
    public const string PROJECTILE = "Projectiles";
    public const string UI = "UI";
    #endregion
    #region Tag
    public static readonly string Bullet = nameof(Bullet);
    #endregion
    #region Value
    public static readonly float TIMER = 30.0f;
    #endregion
}