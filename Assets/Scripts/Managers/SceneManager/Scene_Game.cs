using UnityEngine;

public class Scene_Game : Scene_Base
{
    private readonly int MAP_SIZE_Y = 30;

    private MapObjectSpawner currentSpawner;
    private MapObjectSpawner nextSpawner;

    private int currentStage;
    private int nextStage;

    // TODO: TEST CODE
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GenerateMap(10, 5);
        }
    }

    protected override void Initialize()
    {
        base.Initialize();

        Managers.Resource.Instantiate<PlayerController>(null, Vector2.zero);

        GenerateMap(10, 10); // 현재 스테이지
        GenerateMap(10, 5); // 다음 스테이지 미리 생성
    }

    private void GenerateMap(int obstacleCount, int monsterCount)
    {
        GameObject map = Managers.Resource.Instantiate(Define.MAP, null, new(0, MAP_SIZE_Y * nextStage), Define.MAP);
        MapObjectSpawner spawner = map.GetComponent<MapObjectSpawner>();
        spawner.MapObjectSpawn(obstacleCount, monsterCount);

        if (currentSpawner == null)
        {
            currentSpawner = spawner;
        }
        else
        {
            nextSpawner = spawner;
        }

        nextStage++;
    }
}