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

        GenerateMap(10, 10); // 현재 스테이지
        GenerateMap(10, 5); // 다음 스테이지 미리 생성

        if (Managers.Game.Player == null)
        {
            Managers.Resource.Instantiate<PlayerController>(null, 5.0f * Vector2.down);
            return;
        }

        Managers.Camera.Main.transform.position = new(0.0f, -7.0f, -10.0f);
        Managers.Game.Player.transform.position = 5.0f * Vector2.down;
    }

    private void GenerateMap(int obstacleCount, int monsterCount)
    {
        if (currentSpawner != null && nextSpawner != null)
        {
            Destroy(currentSpawner.gameObject);
            currentSpawner = nextSpawner;
            //currentSpawner.Enemies.gameObject.SetActive(true);
        }

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
        //if(nextSpawner != null)
        //    nextSpawner.Enemies.gameObject.SetActive(false);
        currentStage = nextStage;

        nextStage++;
    }
}