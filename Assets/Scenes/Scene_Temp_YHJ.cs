using UnityEngine;

public class Scene_Temp_YHJ : Scene_Base
{
    protected override void Initialize()
    {
        base.Initialize();

        Managers.Resource.Instantiate<PlayerController>(null, Vector2.zero);
        Managers.Resource.Instantiate("Bat", null, 5.0f * Vector2.one, Define.ENEMIES);
    }
}