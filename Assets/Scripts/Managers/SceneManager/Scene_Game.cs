using UnityEngine;

public class Scene_Game : Scene_Base
{
    private readonly int MAP_SIZE_Y = 30;

    private readonly int OBSTACLE_COUNT = 10;
    private readonly int ENEMY_BASE = 5;

    private readonly int POSITION_Y_BASE = -5;

    private readonly int BOSS_STAGE = 3;

    public MapObjectSpawner CurrentSpawner { get; private set; }
    public MapObjectSpawner NextSpawner { get; private set; }

    public UI_Lobby LobbyUI { get; private set; }

    private int currentStage;
    private int nextStage;

    protected override void Initialize()
    {
        
        base.Initialize();

        GenerateMap(); // 현재 스테이지
        GenerateMap(); // 다음 스테이지 미리 생성

        if (Managers.Game.Player == null)
        {
            Managers.Resource.Instantiate<PlayerController>(null, 5.0f * Vector2.down);
            return;
        }

        LobbyUI = Managers.UI.CurrentSceneUI as UI_Lobby;

        Managers.Camera.Main.transform.position = new(0.0f, -5.0f, -10.0f);
        Managers.Game.Player.transform.position = POSITION_Y_BASE * Vector2.up;
    }

    public void GenerateMap()
    {
        if (NextSpawner != null)
        {
            CurrentSpawner.Clear();
            CurrentSpawner = NextSpawner;

            Managers.Game.Player.transform.position = (POSITION_Y_BASE + MAP_SIZE_Y * currentStage) * Vector2.up;
        }

        // 보스 스테이지 이상이라면 더 이상 맵을 생성하지 않음
        if (nextStage == BOSS_STAGE)
        {
            return;
        }

        GameObject map = Managers.Resource.Instantiate(Define.MAP, null, new(0, MAP_SIZE_Y * nextStage), Define.MAP);
        MapObjectSpawner spawner = map.GetComponent<MapObjectSpawner>();
        if (nextStage == BOSS_STAGE - 1)
        {
            spawner.MapObjectSpawn(OBSTACLE_COUNT, 0);
        }
        else
        {
            spawner.MapObjectSpawn(OBSTACLE_COUNT, ENEMY_BASE + nextStage);
        }

        if (CurrentSpawner == null)
        {
            CurrentSpawner = spawner;
        }
        else
        {
            NextSpawner = spawner;
        }

        currentStage = nextStage;
        nextStage++;
    }
}