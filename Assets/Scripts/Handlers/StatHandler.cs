using UnityEngine;
using VInspector;

public class StatHandler : MonoBehaviour
{
    #region Inspector
    [Foldout("Stats Settings")]
    [Min(0.1f)]
    public float AttackDelay = 1.0f;
    [Min(1.0f)]
    public float AttackRange = 1.0f;
    public float HP = 100.0f;
    public float Damage = 1.0f;
    [EndFoldout]

    [Foldout("Physics Settings")]
    public float MoveSpeed = 1.0f;
    public float JumpPower;
    public float Gravity = 9.8f;
    [EndFoldout]

    [Foldout("Current Status")]
    [ReadOnly] public float VelocityZ;
    [ReadOnly] public float CurrentHP;
    #endregion

    public float AttackSpeed { get { return initialAttackDelay / AttackDelay; } }

    private float initialAttackDelay;

    private UI_WorldSpace hpBar;

    private void Awake()
    {
        initialAttackDelay = AttackDelay;
        CurrentHP = HP;
    }

    private void Start()
    {
        if (gameObject.layer == LayerMask.GetMask(Define.Boss))
        {
            hpBar = Managers.UI.Show<UI_BossHP>();
            hpBar.transform.SetParent(gameObject.FindComponent<Transform>(Define.MainRenderer));
            hpBar.transform.localPosition = Vector2.zero;
        }
    }

    public void ApplyStats(SkillTable.Data skill)
    {
        Damage += skill.Damage;
        HP += skill.HP;
        AttackDelay += skill.AttackDelay;
        AttackDelay = Mathf.Clamp(AttackDelay, 0.1f, 10f);
        AttackRange += skill.AttackRange;
        MoveSpeed += skill.MoveSpeed;
    }

    public void OnDamage(float damage)
    {
        CurrentHP = Mathf.Max(CurrentHP - damage, 0.0f);
        if (hpBar == null)
        {
            hpBar = Managers.UI.Show<UI_HPBar>();
            hpBar.transform.SetParent(gameObject.FindComponent<Transform>(Define.MainRenderer));
            hpBar.transform.localPosition = Vector2.zero;
        }

        if (CurrentHP <= 0)
        {
            hpBar.Death();
        }

        if (gameObject.layer == LayerMask.GetMask(Define.Boss))
        {
            (hpBar as UI_BossHP).UpdateUI(CurrentHP, HP);
        }
        else
        {
            (hpBar as UI_HPBar).UpdateUI(CurrentHP, HP);
        }
    }
}