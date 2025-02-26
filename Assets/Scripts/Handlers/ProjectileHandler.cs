using System.Collections.Generic;
using UnityEngine;

public enum ProjectilePattern
{
    Default,
}

public class ProjectileHandler : MonoBehaviour
{
    #region Inspector
    [SerializeField]
    private List<ProjectileController> projectiles = new();
    #endregion

    // Åº ÆÛÁü °¢µµ
    public float SpreadAngle { get; set; } = 15.0f;

    // °ø°Ý È½¼ö
    public int AttackCount { get; set; } = 1;

    // ÃÑ¾Ë °³¼ö
    public int ProjectileCount { get; set; } = 1;

    private bool isPlayer;

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
            case ProjectilePattern.Default:
                Fire(startPosition, direction);
                break;
        }
    }

    private void Fire(Vector2 startPosition, Vector2 targetDirection)
    {
        float minAngle = (ProjectileCount - 1) * -0.5f * SpreadAngle;
        for (int i = 0; i < ProjectileCount; i++)
        {
            float angle = minAngle + (SpreadAngle * i);
            Vector2 bulletDirection = Quaternion.Euler(0.0f, 0.0f, angle) * targetDirection;

            ProjectileController projectile = Managers.Resource.Instantiate<ProjectileController>(null, startPosition, Define.PROJECTILE);
            projectile.SetProjectile(isPlayer, statHandler.Damage, bulletDirection);
        }
    }
}