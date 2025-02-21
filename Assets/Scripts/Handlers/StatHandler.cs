using UnityEngine;
using VInspector;

public class StatHandler : MonoBehaviour
{
    [Foldout("Physics Settings")]
    [ShowInInspector] public float MoveSpeed;
    [ShowInInspector] public float JumpPower;
    [ShowInInspector] public float Gravity;
    [EndFoldout]

    [Foldout("Physic Status")]
    [ShowInInspector, ReadOnly] public float VelocityZ;
}