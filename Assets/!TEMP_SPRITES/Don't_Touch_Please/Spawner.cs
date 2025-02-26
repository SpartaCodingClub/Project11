using GoogleSheet.Type;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    public Tilemap obstacleSpawnArea;
    public Tilemap monsterSpawnArea;

    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private GameObject[] monsterPrefabs;
    [SerializeField] private int obstacleCount = 40;
    [SerializeField] private int monseterCount = 40;
    [SerializeField] Transform contain;

    private HashSet<Vector2> usedPositions = new HashSet<Vector2>();

    public void SpawnAll()
    {
        usedPositions.Clear();
        Spawn(obstacleSpawnArea, obstaclePrefabs, obstacleCount);
        Spawn(monsterSpawnArea, monsterPrefabs, monseterCount);
    }
    public void ObstacleSpawn()
    {
        usedPositions.Clear();
        Spawn(obstacleSpawnArea, obstaclePrefabs, obstacleCount);
    }
    public void MonsterSpawn()
    {
        Spawn(monsterSpawnArea, monsterPrefabs, monseterCount);
    }


    void Spawn(Tilemap spawnObj, GameObject[] prefabs, int count)
    {
        int attempt = 1000;
        if(spawnObj != null && prefabs != null )
        {
            for (int i = 0; i < count; i++)
            {
                Vector2 pos = GetRandomPositionInSpawnArea(spawnObj);
                GameObject obj = prefabs[Random.Range(0, prefabs.Length)];
                BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();

                if (!OverLine(spawnObj, pos, collider))
                {
                    SetUsedPosition(pos, collider);
                    Instantiate(obj, pos + new Vector2(0.5f, 0.5f), Quaternion.identity);
                }
                else
                {
                    i--;
                    attempt--;
                    if (attempt <= 0)
                        return;
                }
            }
        }
    }
    // spawnArea 내 랜덤 좌표 반환
    Vector2 GetRandomPositionInSpawnArea(Tilemap Area)
    {
        BoundsInt area = Area.cellBounds;
        List<Vector3Int> allArea = new List<Vector3Int>();

        foreach (var pos in area.allPositionsWithin)
        {
            if (Area.HasTile(pos))
                allArea.Add(pos);
        }

        if (allArea.Count == 0)
            return Vector2.zero;

        Vector3Int randPos = allArea[Random.Range(0, allArea.Count)];
        return Area.CellToWorld(randPos);
    }
    // 사용된 좌표 저장
    void SetUsedPosition(Vector2 position, BoxCollider2D collider)
    {
        Vector2 minPos = position - collider.size * 0.5f;
        Vector2 maxPos = position + collider.size * 0.5f;

        for (int x = Mathf.CeilToInt(minPos.x); x <= Mathf.FloorToInt(maxPos.x); x++)
        {
            for (int y = Mathf.CeilToInt(minPos.y); y <= Mathf.FloorToInt(maxPos.y); y++)
            {
                usedPositions.Add(new Vector2(x, y));
            }
        }
    }
    // 스폰 위치를 넘어갔는지, 장애물이 겹치는지 판단
    bool OverLine(Tilemap area, Vector2 position, BoxCollider2D collider)
    {
        Vector2 minPos = position - collider.size * 0.5f;
        Vector2 maxPos = position + collider.size * 0.5f;
        BoundsInt allArea = area.cellBounds;

        for (int x = Mathf.CeilToInt(minPos.x); x <= Mathf.FloorToInt(maxPos.x); x++)
        {
            for (int y = Mathf.CeilToInt(minPos.y); y <= Mathf.FloorToInt(maxPos.y); y++)
            {
                Vector3Int tilePos = area.WorldToCell(new Vector3(x, y, 0));
                if (!area.HasTile(tilePos) || usedPositions.Contains(new Vector2(x, y)))
                    return true;
            }
        }
        return false;
    }
}
