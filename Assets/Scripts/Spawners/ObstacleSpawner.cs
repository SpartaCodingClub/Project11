using GoogleSheet.Type;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Tilemap area;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private int obstacleCount;

    private void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        int attempt = 1000;
        if (area != null && prefabs != null)
        {
            for (int i = 0; i < obstacleCount; i++)
            {
                Vector2 pos = GetRandomPositionInSpawnArea(area);
                GameObject obj = prefabs[Random.Range(0, prefabs.Length)];
                BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();

                if (!IsOverLapping(pos, collider))
                {
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

    bool IsOverLapping(Vector2 pos,BoxCollider2D collider)
    {
        Collider2D hit = Physics2D.OverlapBox(pos, collider.size,0);
        return hit!= null;
    }
}

