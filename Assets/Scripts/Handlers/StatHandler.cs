using UnityEngine;
using VInspector;

public class StatHandler : MonoBehaviour
{
    [Foldout("Stats Settings")]
    [Min(0.1f)]
    public float AttackDelay = 1.0f;
    [Min(1.0f)]
    public float AttackRange = 1.0f;
    [EndFoldout]

    [Foldout("Physics Settings")]
    public float MoveSpeed = 1.0f;
    public float JumpPower;
    public float Gravity = 9.8f;
    [EndFoldout]

    [Foldout("Physic Status")]
    [ShowInInspector, ReadOnly] public float VelocityZ;

    public float AttackSpeed { get { return initialAttackDelay / AttackDelay; } }

    private float initialAttackDelay;

    private void Awake()
    {
        initialAttackDelay = AttackDelay;
    }
}