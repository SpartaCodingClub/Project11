using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapSpawner : MonoBehaviour
{
    Queue<GameObject> currentGround = new Queue<GameObject>();
    Vector2 spawnPos = Vector2.zero;
    ObstacleSpawner spawner;

    private void Start()
    {
        SpawnMap();
    }
    void SpawnMap()
    {
        Debug.Log("asdf");
        //spawner.area = instantiateGround.transform.Find("ObstaclesArea")?.GetComponent<Tilemap>();
        //if (spawner.area == null)
        //    Debug.Log("찾았다");
        //else
        //    Debug.Log("망했다");

        //if (spawner.area != null)
        //{
        //    spawner.Spawn();
        //    currentGround.Enqueue(instantiateGround);
        //    spawnPos += new Vector2(0, 30f);
        //}
    }
}