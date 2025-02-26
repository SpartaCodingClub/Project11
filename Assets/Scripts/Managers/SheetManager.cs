using GoogleSheet.Core.Type;
using System.Collections.Generic;
using UGS;

[UGS(typeof(SkillType))]
public enum SkillType
{
    StatHandler,
    ProjectileHandler
}
public class SheetManager
{
    public void Initialize()
    {
        UnityGoogleSheet.LoadFromGoogle<int, SkillTable.Data>((list, map) =>
        {
            Managers.Skill.skillList = list;
        }, true);
    }
}