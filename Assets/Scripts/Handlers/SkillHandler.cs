using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class SkillHandler : MonoBehaviour
{
    public static SkillHandler instance;
    private List<SkillTable.Data> skillList = new ();

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void SetSkillData(List<SkillTable.Data> skills)
    {
        skillList = skills;
    }

    public void ApplySkill(int skillIndex, PlayerController player)
    {
        StatHandler stat = player.GetComponent<StatHandler>();
        ProjectileHandler projectile = player.GetComponent<ProjectileHandler>();

        SkillTable.Data skill = skillList.Find(s => s.index == skillIndex);

        if (skill == null)
            return;
        switch(skill.SkillType)
        {
            case SkillType.StatHandler:
                stat.ApplyStats(skill);
                break;
            case SkillType.ProjectileHandler:
                projectile.ApplyProjectiles(skill);
                break;
        }
        Debug.Log($"스킬 {skill.SkillName} 적용");
    }
}