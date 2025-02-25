using System.Collections.Generic;
using UnityEngine;

public enum ProjectilePattern
{
    Single,
}

public class ProjectileHandler : MonoBehaviour
{
    #region Inspector
    [SerializeField]
    private List<ProjectileController> projectiles = new();
    #endregion

    private bool isPlayer;
    private float bulletSpread = 10f; // 탄 퍼짐 정도
    private float multileProjectileAngle; // 탄 퍼짐 각도
    private int numberofProjectilesPerShot; // 1번 발사할때 나가는 투사체 수

    private StatHandler statHandler;

    private void Awake()
    {
        isPlayer = GetComponent<PlayerController>() != null;
        statHandler = GetComponent<StatHandler>();
    }

    public void RangeAttack(ProjectilePattern weaponType, Vector2 startPosition, Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - startPosition).normalized;
        switch (weaponType)
        {
            case ProjectilePattern.Single:
                Fire_Single(startPosition, direction);
                break;
        }
    }

    private void Fire_Single(Vector2 startPosition, Vector2 targetDirection)
    {
        ProjectileController projectile = Managers.Resource.Instantiate<ProjectileController>(null, startPosition, Define.PROJECTILE);
        projectile.SetProjectile(isPlayer, statHandler.Damage, targetDirection);
    }

    private void FireShotgun(Vector2 position, Vector2 direction)
    {
        //numberofProjectilesPerShot = 10;
        //float minAngle = -(numberofProjectilesPerShot - 1) * 0.5f * multileProjectileAngle;
        //for (int i = 0; i < numberofProjectilesPerShot; i++)
        //{
        //    float angle = minAngle + (i * multileProjectileAngle);
        //    float randSpread = Random.Range(-bulletSpread, bulletSpread);
        //    angle += randSpread;

        //    Vector2 rotatedDirection = Quaternion.Euler(0, 0, angle) * direction;

        //    FireBullet(position, rotatedDirection);
        //}
    }
}