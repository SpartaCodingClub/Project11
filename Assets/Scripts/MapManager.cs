using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject[] ground;
    [SerializeField] Transform contain;
    Queue<GameObject> currentGround = new Queue<GameObject>();
    Vector2 spawnPos = Vector2.zero;
    ObstacleSpawner spawner;
    private void Start()
    {
        spawner = GetComponent<ObstacleSpawner>();
    }
    void Update()
    {
        SpawnMap(spawnPos);
    }

    void SpawnMap(Vector2 pos)
    {
        if (currentGround.Count < 2)
        {
            GameObject  instantiateGround = Instantiate(ground[Random.Range(0, ground.Length)], spawnPos, Quaternion.identity, contain);
            spawner.spawnArea = instantiateGround.transform.Find("ObstaclesArea")?.GetComponent<Tilemap>();
            
            if (spawner.spawnArea != null)
            {
                spawner.SpawnObstacle();
                currentGround.Enqueue(instantiateGround);
                spawnPos += new Vector2(0, 30f);
            }
        }
        else
        {
            //Destroy(currentGround.Dequeue());
        }
    }
}
