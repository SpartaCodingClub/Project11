using GoogleSheet.Core.Type;
using UGS;

public class SheetManager
{
    [UGS(typeof(SkillType))]
    public enum SkillType
    {
        StatHandler,
        ProjectileHandler
    }

    public void Initialize()
    {
        //UnityGoogleSheet.LoadFromGoogle<int, SkillTable.Data>((list, map) =>
        //{
        //    list.ForEach(x =>
        //    {
        //        Debug.Log($"{x.index} : {x.Damage}, {x.attackRange}, {x.projectileCount}");
        //    });
        //}, true);
    }
}