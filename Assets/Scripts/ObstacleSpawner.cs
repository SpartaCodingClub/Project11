using GoogleSheet.Type;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class ObstacleSpawner : MonoBehaviour
{
    public Tilemap spawnArea;
    public GameObject[] obstalePrefabs;
    private List<Vector2> usedPositions = new List<Vector2>();
    [SerializeField] private int obstacleCount = 40;

    void Start()
    {
        SpawnObstacle();
    }

    void SpawnObstacle()
    {
        usedPositions.Clear();
        for (int i = 0; i < obstacleCount; i++)
        {
            Vector2 pos = GetRandomPositionInSpawnArea() + new Vector2(0.5f,0.5f);
            if (pos != Vector2.zero && !OverSize(pos))
            {
                GameObject obstacle = obstalePrefabs[Random.Range(0, obstalePrefabs.Length)];
                BoxCollider2D collider = obstacle.GetComponent<BoxCollider2D>();
                if (!OverLap(pos,collider))
                    Instantiate(obstacle, pos, Quaternion.identity);
                else
                    i--;
            }
            else
                i--;
        }
    }
    // spawnArea에 타일이 있는 장소만 탐색
    Vector2 GetRandomPositionInSpawnArea()
    {
        BoundsInt area = spawnArea.cellBounds;
        List<Vector3Int> allArea = new List<Vector3Int>();

        foreach (var hastile in area.allPositionsWithin)
        {
            if (spawnArea.HasTile(hastile))
                allArea.Add(hastile);
        }
        Vector3Int randPos = allArea[Random.Range(0, allArea.Count)];
        return spawnArea.CellToWorld(randPos);
    }
    // 각 오브젝트 사이의 최소 거리 조절
    bool OverSize(Vector2 position)
    {
        foreach (Vector2 usedPos in usedPositions)
        {
            if (Vector2.Distance(usedPos, position) < 0.9f)
            {
                return true;
            }
        }
        return false;
    }
    // 콜라이더가 겹칠 경우 판단
    bool OverLap(Vector2 position, BoxCollider2D collider)
    {
        Collider2D hit = Physics2D.OverlapBox(position, collider.size-new Vector2(0.1f,0.1f),0);
        return hit != null;
    }
}