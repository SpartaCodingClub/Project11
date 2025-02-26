using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapSpawner : MonoBehaviour
{
    [SerializeField] GameObject map;
    Queue<GameObject> currentmap = new();
    Vector2 spawnPos = new Vector2(0.5f, 0);
    Spawner spawner;

    private void Start()
    {
        SpawnMap();
        SpawnMap();
    }
    void SpawnMap()
    {
        GameObject instantiateGround = Instantiate(map, spawnPos, Quaternion.identity,transform);
        spawner = instantiateGround.GetComponentInChildren<Spawner>();
        spawner.obstacleSpawnArea = instantiateGround.transform.Find("ObstacleArea")?.GetComponent<Tilemap>();
        spawner.monsterSpawnArea = instantiateGround.transform.Find("MonsterArea")?.GetComponent<Tilemap>();

        spawner.ObstacleSpawn();
        spawner.MonsterSpawn();

        currentmap.Enqueue(instantiateGround);
        spawnPos += new Vector2(0, 30f);

        if(currentmap.Count > 2)
            Destroy(currentmap.Dequeue());
    }
}