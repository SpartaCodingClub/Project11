using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class SkillManager
{
    public List<SkillTable.Data> skillList = new ();

    public void ApplySkill(int skillIndex, PlayerController player)
    {
        SkillTable.Data skill = skillList.Find(s => s.index == skillIndex);

        if (skill == null)
        {
            Debug.Log("skill null");
            return;
        }

        StatHandler stat = player.GetComponent<StatHandler>();
        ProjectileHandler projectile = player.GetComponent<ProjectileHandler>();

        if (skill.SkillType == SkillType.StatHandler && stat != null)
        {
            stat.ApplyStats(skill);
            Debug.Log("stat 적용");
        }
        else if (skill.SkillType == SkillType.ProjectileHandler && projectile != null)
        {
            projectile.ApplyProjectiles(skill);
            Debug.Log("projectile 적용");
        }
        else
        {
            Debug.Log($"플레이어에게 {skill.SkillType} 핸들러가 없음");
        }

    }
}