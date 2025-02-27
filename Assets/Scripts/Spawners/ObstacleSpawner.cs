using GoogleSheet.Type;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;


public class ObstacleSpawner : MonoBehaviour
{
    public enum Obstacle
    {
        Cone,
        Barricade,
        Count
    }
    public enum Monster
    {
        Bat,
        Bear,
        MushRoom,
        Seeder,
        Snake,
        Spider,
        Zombie,
        Count
    }
    public Tilemap area;
    public Transform obstacleContain;
    public Transform monstercontain;

    public IEnumerator ObstacleSpawn(int count)
    {
        int attempt = 1000;

        if (area != null)
        {
            for (int i = 0; i < count; i++)
            {
                Vector2 pos = GetRandomPositionInSpawnArea(area);

                int ran = Random.Range(0, 3);
                if (ran == 0)
                    ran = (int)Obstacle.Barricade;
                else
                    ran = (int)Obstacle.Cone;
                GameObject obj = Managers.Resource.Instantiate(((Obstacle)ran).ToString(), obstacleContain, pos);
                BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();
                if (IsOverLapping(pos, collider))
                {
                    Managers.Resource.Destroy(obj);
                    i--;
                    attempt--;
                    if (attempt <= 0)
                        yield break;
                }
                yield return null;
            }
        }
    }
    public IEnumerator MonsterSpawn(int count)
    {
        int attempt = 1000;

        if (area != null)
        {
            for (int i = 0; i < count; i++)
            {
                Vector2 pos = GetRandomPositionInSpawnArea(area);
                int ran = Random.Range(0, (int)Monster.Count);
                GameObject obj = Managers.Resource.Instantiate(((Monster)ran).ToString(), monstercontain, pos, Define.ENEMIES);
                BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();
                if (IsOverLapping(pos, collider))
                {
                    Managers.Resource.Destroy(obj);
                    i--;
                    attempt--;
                    if (attempt <= 0)
                        yield break;
                }
                yield return null;
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

    bool IsOverLapping(Vector2 pos, BoxCollider2D collider)
    {
        Collider2D hit = Physics2D.OverlapBox(pos, collider.size * new Vector2(1.5f, 1.5f), 0);
        return hit != null;
    }

}

