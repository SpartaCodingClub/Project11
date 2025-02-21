using UGS;

public class SheetManager
{
    public void Initialize()
    {
        UnityGoogleSheet.LoadFromGoogle<int, DefaultTable.Data>((list, map) =>
        {
            list.ForEach(x =>
            {
                Debug.Log($"{x.index} : {x.intValue}, {x.strValue}");
            });
        }, true);
    }
}