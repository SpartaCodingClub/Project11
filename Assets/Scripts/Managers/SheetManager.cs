using GoogleSheet.Core.Type;
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
            //list.ForEach(x =>
            //{
            //    Debug.Log($"{x.index} : {x.SkillType},{x.SkillName},{x.Damage},{x.HP},{x.AttackDelay},{x.AttackRange},{x.MoveSpeed},{x.AttackDelay} ");
            //});
            SkillHandler.instance.SetSkillData(list);
        }, true);
    }
}