using UnityEngine;

public class Scene_Game : Scene_Base
{
    protected override void Initialize()
    {
        base.Initialize();

        Managers.Resource.Instantiate<PlayerController>(null, Vector2.zero);
        Managers.Resource.Instantiate(Define.MAP, null, Vector2.zero, Define.MAP);
    }
}