using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Tilemaps;

public class Scene_Game : Scene_Base
{
    List<GameObject> currentGround = new();
    ObstacleSpawner spawner;

    Vector2 spawnPos = Vector2.zero;
    int ObstacleCount = 10;
    int monsterCount = 5;
    float time = 0;
    protected override void Initialize()
    {
        base.Initialize();

        Managers.Resource.Instantiate<PlayerController>(null, Vector2.zero);
        StartCoroutine(StartSettingMap());
    }

    public void Update()
    {
        time += Time.deltaTime;
        if (time > 4)
        {
            Clear();
            time = 0;
        }
    }
    IEnumerator StartSettingMap()
    {
        for (int i = 0; i < 2; i++)
            yield return StartCoroutine(CreateNewGround());
        UpdateGroundActivation();
    }

    IEnumerator CreateNewGround()
    {
        GameObject instantiateGround = Managers.Resource.Instantiate(Define.MAP, this.transform, spawnPos, Define.MAP);
        spawner = instantiateGround.GetComponent<ObstacleSpawner>();
        spawner.area = instantiateGround.transform.GetChild(0).GetChild(0).GetComponent<Tilemap>();

        if (spawner.area != null)
        {
            StartCoroutine(spawner.ObstacleSpawn(ObstacleCount));
            yield return StartCoroutine(spawner.MonsterSpawn(monsterCount));
            currentGround.Add(instantiateGround);
            spawnPos += new Vector2(0, 30f);
        }
    }
    void Clear()
    {
        if (currentGround.Count > 0)
        {
            Managers.Destroy(currentGround[0]);
            currentGround.RemoveAt(0);
        }

        StartCoroutine(RespawnGround());
    }

    IEnumerator RespawnGround()
    {
        yield return StartCoroutine(CreateNewGround());
        UpdateGroundActivation();
    }

    void UpdateGroundActivation()
    {
        if (currentGround.Count >= 2)
        {
            currentGround[0].transform.GetChild(1).gameObject.SetActive(true);
            currentGround[1].transform.GetChild(1).gameObject.SetActive(false);
        }
    }

}