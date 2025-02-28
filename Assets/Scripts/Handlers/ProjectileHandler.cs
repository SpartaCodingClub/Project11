using System.Collections;
using UnityEngine;
using VInspector;

public class ProjectileHandler : MonoBehaviour
{
    #region Inspector
    [SerializeField]
    private WeaponType weaponType;

    [SerializeField]
    public ProjectileController projectile;

    [ShowIf("weaponType", WeaponType.Range)]
    [Tooltip("공격 횟수"), Range(0, 3)]
    public int AttackCount = 1;

    [ShowIf("weaponType", WeaponType.Range)]
    [Tooltip("공격 횟수 사이의 공격 속도"), Min(0)]
    public float AttakInterval;

    [ShowIf("weaponType", WeaponType.Range)]
    [Tooltip("탄 퍼짐 각도"), Range(0, 360)]
    public float SpreadAngle = 15.0f;

    [ShowIf("weaponType", WeaponType.Range)]
    [Tooltip("투사체 개수"), Range(0, 36)]
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
                StartCoroutine(Fire(startPosition, targetDirection));
                break;
        }
    }

    private void MeleeAttack(Vector2 startPosition, Vector2 targetDirection)
    {
        string key = this.projectile.name;
        GameObject projectile = Managers.Resource.Instantiate(key, null, startPosition, Define.PROJECTILE);

        BoxCollider2D collider = projectile.GetComponent<BoxCollider2D>();
        collider.size = new(statHandler.AttackRange, 1.0f);
        collider.offset = new(statHandler.AttackRange * 0.5f, 0.0f);

        projectile.GetComponent<ProjectileController>().SetProjectile(isPlayer, statHandler.Damage, targetDirection);
    }

    public void ApplyProjectiles(SkillTable.Data skill)
    {
        AttackCount += skill.AttackCount;
        ProjectileCount += skill.ProjectileCount;
    }

    private IEnumerator Fire(Vector2 startPosition, Vector2 targetDirection)
    {
        if (AttackCount > 3.0f)
        {
            Debug.LogWarning($"AttackCount의 값은 3을 초과할 수 없습니다.");
            yield break;
        }

        if (SpreadAngle * ProjectileCount > 360.0f)
        {
            Debug.LogWarning($"SpreadAngle * ProjectileCount의 값은 360을 초과할 수 없습니다!");
            yield break;
        }

        WaitForSeconds interval = new(Mathf.Min(statHandler.AttackDelay / (AttackCount + 1), AttakInterval));
        for (int i = 0; i < AttackCount; i++)
        {
            if (statHandler.HP <= 0.0f)
            {
                yield break;
            }

            float minAngle = (ProjectileCount - 1) * -0.5f * SpreadAngle;
            for (int j = 0; j < ProjectileCount; j++)
            {
                float angle = minAngle + (SpreadAngle * j);
                Vector2 bulletDirection = Quaternion.Euler(0.0f, 0.0f, angle) * targetDirection;

                string key = this.projectile.name;
                GameObject projectile = Managers.Resource.Instantiate(key, null, startPosition, Define.PROJECTILE);
                projectile.GetComponent<ProjectileController>().SetProjectile(isPlayer, statHandler.Damage, bulletDirection);
            }

            if (isPlayer)
            {
                Managers.Audio.Play(Clip.SoundFX_Shooting);
            }

            yield return interval;
        }
    }
}