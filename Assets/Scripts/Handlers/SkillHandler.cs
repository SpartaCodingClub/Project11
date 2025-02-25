using UnityEngine;
using VInspector;

public class SkillHandler : MonoBehaviour
{
    [Header("Stats Settings")]
    public float Health;
    public float AttackDelay;
    public float AttackRange;

    [Header("Physics Settings")]
    public float MoveSpeed;
    public float JumpPower;
    public float Gravity;

    [Header("Projectile Settings")]
    public float ProjectileRange;
    public float ProjectileNum;
    public float ProjectileDiffusion;
}