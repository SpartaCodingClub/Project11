using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class _MapSpawner : MonoBehaviour
{
    [SerializeField] GameObject map;
    Queue<GameObject> currentmap = new();
    Vector2 spawnPos = new Vector2(0.5f, 0);
    _Spawner spawner;

    private void Start()
    {
        SpawnMap();
        SpawnMap();
    }
    void SpawnMap()
    {
        GameObject instantiateGround = Managers.Resource.Instantiate(Define.MAP,null,spawnPos,Define.MAP);
        spawner = instantiateGround.GetComponentInChildren<_Spawner>();
        spawner.obstacleSpawnArea = instantiateGround.transform.Find("ObstacleArea")?.GetComponent<Tilemap>();
        spawner.monsterSpawnArea = instantiateGround.transform.Find("MonsterArea")?.GetComponent<Tilemap>();

        spawner.ObstacleSpawn();

        currentmap.Enqueue(instantiateGround);
        spawnPos += new Vector2(0, 30f);

        if(currentmap.Count > 2)
            Destroy(currentmap.Dequeue());
    }
}