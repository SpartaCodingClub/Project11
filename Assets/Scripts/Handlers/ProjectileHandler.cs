using UnityEngine;
using VInspector;

public class ProjectileHandler : MonoBehaviour
{
    #region Inspector
    [SerializeField]
    private WeaponType weaponType;

    [ShowIf("weaponType", WeaponType.Range)]
    [Tooltip("투사체 프리팹")]
    [SerializeField]
    private ProjectileController projectile;

    [ShowIf("weaponType", WeaponType.Range)]
    [Tooltip("공격 횟수")]
    public int AttackCount = 1;

    [ShowIf("weaponType", WeaponType.Range)]
    [Tooltip("탄 퍼짐 각도")]
    public float SpreadAngle = 15.0f;

    [ShowIf("weaponType", WeaponType.Range)]
    [Tooltip("투사체 개수")]
    public int ProjectileCount = 1;
    [EndFoldout]
    #endregion

    public enum WeaponType
    {
        Melee,
        Range,
    }

    private bool isPlayer;

    private StatHandler statHandler;

    private void Awake()
    {
        isPlayer = GetComponent<PlayerController>() != null;
        statHandler = GetComponent<StatHandler>();
    }

    public void RangeAttack(Vector2 startPosition, Vector2 targetDirection)
    {
        switch (weaponType)
        {
            case WeaponType.Melee:
                MeleeAttack(startPosition, targetDirection);
                break;
            case WeaponType.Range:
                Fire(startPosition, targetDirection);
                break;
        }
    }

    private void MeleeAttack(Vector2 startPosition, Vector2 targetDirection)
    {
        GameObject projectile = Managers.Resource.Instantiate(Define.Melee, null, startPosition, Define.PROJECTILE);

        BoxCollider2D collider = projectile.GetComponent<BoxCollider2D>();
        collider.size = new(statHandler.AttackRange, 1.0f);
        collider.offset = new(statHandler.AttackRange * 0.5f, 0.0f);

        projectile.GetComponent<ProjectileController>().SetProjectile(isPlayer, statHandler.Damage, targetDirection);
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

    public void ApplyProjectiles(SkillTable.Data skill)
    {
        AttackCount += skill.AttackCount;
        ProjectileCount += skill.ProjectileCount;
    }
}