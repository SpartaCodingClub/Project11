using UnityEngine;
using VInspector;

public enum WeaponType
{
    Bullet,
    Laser
}
public class WeaponHandler : MonoBehaviour
{
    [Foldout("Weapon Settings")]
    public float projectileSpeed;
    public float projectileRange;
    public float criticalProbability;
    [EndFoldout]

    [Foldout("Physic Status")]
    [ShowInInspector, ReadOnly] public float VelocityZ;

}