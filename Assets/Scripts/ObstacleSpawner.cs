using GoogleSheet.Type;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class ObstacleSpawner : MonoBehaviour
{
    public Tilemap spawnArea;
    public GameObject[] obstaclePrefabs;
    private HashSet<Vector2> usedPositions = new HashSet<Vector2>(); // 중복 방지
    [SerializeField] private int obstacleCount = 40;

    void Start()
    {
        SpawnObstacle();
    }
    void SpawnObstacle()
    {
        usedPositions.Clear();
        int attempt = 1000;
        for (int i = 0; i < obstacleCount; i++)
        {
            Vector2 pos = GetRandomPositionInSpawnArea();
            GameObject obstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            BoxCollider2D collider = obstacle.GetComponent<BoxCollider2D>();

            if (!SetObstacle(pos, collider) && !OverLine(pos, collider))
            {
                SetUsedPosition(pos, collider);
                Instantiate(obstacle, pos + new Vector2(0.5f, 0.5f), Quaternion.identity);
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
    // spawnArea 내 랜덤 좌표 반환
    Vector2 GetRandomPositionInSpawnArea()
    {
        BoundsInt area = spawnArea.cellBounds;
        List<Vector3Int> allArea = new List<Vector3Int>();

        foreach (var pos in area.allPositionsWithin)
        {
            if (spawnArea.HasTile(pos))
                allArea.Add(pos);
        }

        if (allArea.Count == 0)
            return Vector2.zero;

        Vector3Int randPos = allArea[Random.Range(0, allArea.Count)];
        return spawnArea.CellToWorld(randPos);
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
    // 장애물이 겹치는지 판단
    bool SetObstacle(Vector2 position, BoxCollider2D collider)
    {
        Vector2 minPos = position - collider.size * 0.5f;
        Vector2 maxPos = position + collider.size * 0.5f;

        for (int x = Mathf.CeilToInt(minPos.x); x <= Mathf.FloorToInt(maxPos.x); x++)
        {
            for (int y = Mathf.CeilToInt(minPos.y); y <= Mathf.FloorToInt(maxPos.y); y++)
            {
                if (usedPositions.Contains(new Vector2(x, y)))
                    return true;
            }
        }
        return false;
    }
    // 스폰 위치를 넘어갔는지 판단
    bool OverLine(Vector2 position, BoxCollider2D collider)
    {
        Vector2 minPos = position - collider.size * 0.5f;
        Vector2 maxPos = position + collider.size * 0.5f;
        BoundsInt area = spawnArea.cellBounds;

        for (int x = Mathf.CeilToInt(minPos.x); x <= Mathf.FloorToInt(maxPos.x); x++)
        {
            for (int y = Mathf.CeilToInt(minPos.y); y <= Mathf.FloorToInt(maxPos.y); y++)
            {
                Vector3Int tilePos = spawnArea.WorldToCell(new Vector3(x, y, 0));
                if (!spawnArea.HasTile(tilePos))
                    return true;
            }
        }
        return false;
    }
}