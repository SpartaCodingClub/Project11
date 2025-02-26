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

    // 탄 퍼짐 각도
    public float SpreadAngle = 15.0f;

    // 공격 횟수
    public int AttackCount = 1;

    // 총알 개수
    public int ProjectileCount = 1;

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
        if (SpreadAngle * ProjectileCount > 360.0f)
        {
            Debug.LogWarning($"SpreadAngle * ProjectileCount는 360도를 초과할 수 없습니다!");
            return;
        }

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