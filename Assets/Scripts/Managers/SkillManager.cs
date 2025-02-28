using GoogleSheet.Core.Type;
using System.Collections.Generic;
using UGS;

[UGS(typeof(SkillType))]
public enum SkillType
{
    StatHandler,
    ProjectileHandler
}

public enum Skill
{
    DMG,
    ATK_COUNT,
    PRJ_COUNT,
    HP,
    MOV_SPD,
    ATK_DELAY,
    ATK_RANGE
}

public class SkillManager
{
    public List<SkillTable.Data> SkillTable { get; private set; }

    public void Initialize()
    {
        UnityGoogleSheet.LoadFromGoogle<int, SkillTable.Data>((list, map) => SkillTable = list, true);
    }

    public SkillTable.Data GetSkillData(int index)
    {
        if (SkillTable.Count <= index)
        {
            Debug.LogWarning($"Failed to GetSkillData({index})");
            return null;
        }

        return SkillTable[index];
    }

    public void SetSkillData(int index)
    {
        var skillData = GetSkillData(index);

        var statHandler = Managers.Game.Player.StatHandler;
        statHandler.Damage += skillData.Damage;
        statHandler.HP += skillData.HP;
        statHandler.CurrentHP += skillData.HP;
        statHandler.MoveSpeed += skillData.MoveSpeed;
        statHandler.AttackDelay += skillData.AttackDelay;
        statHandler.AttackRange += skillData.AttackRange;

        var projectileHandler = Managers.Game.Player.GetComponent<ProjectileHandler>();
        projectileHandler.AttackCount += skillData.AttackCount;
        projectileHandler.ProjectileCount += skillData.ProjectileCount;
    }
}