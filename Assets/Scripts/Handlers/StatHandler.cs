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

    private UI_HPBar hpBar;

    private void Awake()
    {
        initialAttackDelay = AttackDelay;
        CurrentHP = HP;
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
            if (gameObject.layer == LayerMask.NameToLayer(Define.Boss))
            {
                hpBar.transform.localPosition = 2.0f * Vector2.up;
            }
            else
            {
                hpBar.transform.localPosition = Vector2.zero;
            }
        }

        if (CurrentHP <= 0)
        {
            hpBar.Death();
        }

        hpBar.UpdateUI(CurrentHP, HP);
    }
}