using UnityEngine;

public enum ProjectilePattern
{
    Range,
    Melee,
}

public class ProjectileHandler : MonoBehaviour
{
    #region Inspector
    [SerializeField]
    private ProjectileController projectile;

    [Tooltip("공격 횟수")]
    public int AttackCount = 1;

    [Tooltip("탄 퍼짐 각도")]
    public float SpreadAngle = 15.0f;

    [Tooltip("투사체 개수")]
    public int ProjectileCount = 1;
    #endregion

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
            case ProjectilePattern.Range:
                Fire(startPosition, direction);
                break;
            case ProjectilePattern.Melee:
                MeleeAttack(startPosition, direction);
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

            string key = this.projectile.name;
            GameObject projectile = Managers.Resource.Instantiate(key, null, startPosition, Define.PROJECTILE);
            projectile.GetComponent<ProjectileController>().SetProjectile(isPlayer, statHandler.Damage, bulletDirection);
        }
    }

    private void MeleeAttack(Vector2 startPosition, Vector2 targetDirection)
    {

    }

    public void ApplyProjectiles(SkillTable.Data skill)
    {
        AttackCount += skill.AttackCount;
        ProjectileCount += skill.ProjectileCount;
    }
}