using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public enum WeaponType
{
    pistol,
    machineGun,
    shotGun
}
public class ProjectileHandler : MonoBehaviour
{
    [Header("Projectile Data")]
    [SerializeField] private ProjectileController projectilePrefab;

    StatHandler statHandler;

    [Header("Ranged Attack Data")]
    [SerializeField] private int bulletIndex;
    public int BulletIndex { get => bulletIndex; }

    [SerializeField] private float bulletSize = 1f;
    public float BulletSize { get => bulletSize; }

    [SerializeField] private float bulletSpeed = 1f;
    public float BulletSpeed { get => bulletSpeed; }

    [SerializeField] private float bulletSpread = 10f; // 탄 퍼짐 정도
    public float BulletSpread { get => bulletSpread; }

    [SerializeField] private float multileProjectileAngle; // 탄 퍼짐 각도
    public float MultileProjectileAngle { get => multileProjectileAngle; }

    [SerializeField] private int numberofProjectilesPerShot; // 1번 발사할때 나가는 투사체 수
    public int NumberofProjectilesPerShot { get => numberofProjectilesPerShot; }


    public void RangeAttack(WeaponType weaponType, GameObject startPos, GameObject targetPos)
    {
        Vector2 direction = (targetPos.transform.position - startPos.transform.position).normalized;

        switch (weaponType)
        {
            case WeaponType.pistol:
                FireBullet(startPos.transform.position, direction);
                break;

            case WeaponType.machineGun:
                FireMachineGun(startPos.transform.position, direction);
                break;

            case WeaponType.shotGun:
                FireShotgun(startPos.transform.position, direction);
                break;
        }
    }

    private void FireBullet(Vector2 position, Vector2 direction)
    {
        ProjectileController projectile = Instantiate(projectilePrefab, position, Quaternion.identity);
        projectile.TargetToDirection(direction);
    }

    private void FireMachineGun(Vector2 position, Vector2 direction)
    {
        statHandler.AttackDelay = 0.2f;
        numberofProjectilesPerShot = 5;
        for (int i = 0; i < numberofProjectilesPerShot; i++)
        {
            ProjectileController projectile = Instantiate(projectilePrefab, position, Quaternion.identity);
            projectile.TargetToDirection(direction);
        }
    }

    private void FireShotgun(Vector2 position, Vector2 direction)
    {
        numberofProjectilesPerShot = 10;
        float minAngle = -(numberofProjectilesPerShot - 1) * 0.5f * multileProjectileAngle;
        for (int i = 0; i < numberofProjectilesPerShot; i++)
        {
            float angle = minAngle + (i * multileProjectileAngle);
            float randSpread = Random.Range(-bulletSpread, bulletSpread);
            angle += randSpread;

            Vector2 rotatedDirection = Quaternion.Euler(0, 0, angle) * direction;

            FireBullet(position, rotatedDirection);
        }
    }
}