using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapSpawner : MonoBehaviour
{
    [SerializeField] GameObject map;
    Queue<GameObject> currentmap = new();
    Vector2 spawnPos = new Vector2(30f, 0);
    Spawner spawner;

    private void Awake()
    {
        spawner = gameObject.GetComponentInChildren<Spawner>();
        if (spawner == null)
        {
            Debug.LogError("Spawner component not found in children!");
        }
    }
    private void Start()
    {
        SpawnMap();
        SpawnMap();
    }

    void SpawnMap()
    {
        GameObject instantiateGround = Instantiate(map, spawnPos, Quaternion.identity);
        spawner.obstacleSpawnArea = instantiateGround.transform.Find("ObstacleArea")?.GetComponent<Tilemap>();
        spawner.monsterSpawnArea = instantiateGround.transform.Find("MonsterArea")?.GetComponent<Tilemap>();

        spawner.ObstacleSpawn();

        currentmap.Enqueue(instantiateGround);
        spawnPos += new Vector2(0, 30f);

    }
    void DestroyMap()
    {
        Destroy(currentmap.Dequeue());
    }
}