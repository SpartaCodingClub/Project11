using UnityEngine;

public class Scene_Lobby : Scene_Base

{
    protected override void Initialize()
    {
        base.Initialize();

        Managers.Resource.Instantiate<PlayerController>(null, Vector2.zero);
    }
}