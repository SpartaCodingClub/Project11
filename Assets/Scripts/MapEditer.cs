using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapEditer : MonoBehaviour
{
    [SerializeField] GameObject[] ground;
    [SerializeField] Transform contain;
    Queue<GameObject> currentGround = new Queue<GameObject>();
    Vector2 spawnPos = Vector2.zero;
    ObstacleSpawner spawner;
    private void Start()
    {
        spawner = GetComponent<ObstacleSpawner>();
        SpawnMap();
        SpawnMap();
    }

    void SpawnMap()
    {
        GameObject instantiateGround = Instantiate(ground[Random.Range(0, ground.Length)], spawnPos, Quaternion.identity, contain);
        spawner.spawnArea = instantiateGround.transform.Find("ObstaclesArea")?.GetComponent<Tilemap>();

        if (spawner.spawnArea != null)
        {
            spawner.SpawnObstacle();
            currentGround.Enqueue(instantiateGround);
            spawnPos += new Vector2(0, 30f);
        }
    }
    void DestroyMap()
    {
        Destroy(currentGround.Dequeue());
    }
}