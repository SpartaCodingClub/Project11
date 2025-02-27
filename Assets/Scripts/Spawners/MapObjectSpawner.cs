using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapObjectSpawner : MonoBehaviour
{
    public enum Obstacle
    {
        Cone,
        Barricade,
        Count
    }

    public enum Enemy
    {
        Bat,
        Bear,
        MushRoom,
        Seeder,
        Spider,
        Zombie,
        Count
    }

    private Tilemap ObstacleSpawnArea;
    private Tilemap EnemySpawnArea;
    private Transform Obstacles;
    public Transform Enemies;

    private void Awake()
    {
        ObstacleSpawnArea = gameObject.FindComponent<Tilemap>(nameof(ObstacleSpawnArea));
        EnemySpawnArea = gameObject.FindComponent<Tilemap>(nameof(EnemySpawnArea));
        Obstacles = gameObject.FindComponent<Transform>(nameof(Obstacles));
        Enemies = gameObject.FindComponent<Transform>(nameof(Enemies));
    }

    public void MapObjectSpawn(int obstacleCount, int enemyCount)
    {
        StartCoroutine(MapObjectSpawning(obstacleCount, enemyCount));
    }

    public IEnumerator MapObjectSpawning(int obstacleCount, int enemyCount)
    {
        yield return ObstacleSpawning(obstacleCount);
        yield return EnemySpawning(enemyCount);

        Managers.Game.MonsterCount = Enemies.childCount;
    }

    private IEnumerator ObstacleSpawning(int count)
    {
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                // 랜덤 생성 위치 받아오기
                Vector2 randomPos = GetRandomPosition_Obstacle();

                // 장애물 타입 설정하기
                int randomIndex = Random.Range(0, 3);
                Obstacle obstacleType = randomIndex == 0 ? Obstacle.Barricade : Obstacle.Cone;

                // 장애물 생성하기
                GameObject gameObject = Managers.Resource.Instantiate(obstacleType.ToString(), Obstacles, randomPos);
                BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();

                // 장애물 충돌 검사
                if (IsOverLapping(randomPos, collider) == false)
                {
                    break;
                }

                Managers.Resource.Destroy(gameObject);
            }

            yield return null;
        }
    }

    private IEnumerator EnemySpawning(int count)
    {
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                // 랜덤 생성 위치 받아오기
                Vector2 randomPos = GetRandomPosition_Enemy();

                // 몬스터 타입 설정하기
                Enemy enemyType = (Enemy)Random.Range(0, (int)Enemy.Count);

                // 몬스터 생성하기
                GameObject gameObject = Managers.Resource.Instantiate(enemyType.ToString(), Enemies, randomPos, Define.ENEMIES);
                BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();

                // 몬스터 충돌 검사
                if (IsOverLapping(randomPos, collider) == false)
                {
                    break;
                }

                Managers.Resource.Destroy(gameObject);
            }

            yield return null;
        }
    }

    private Vector2 GetRandomPosition_Obstacle()
    {
        List<Vector3Int> positions = new();

        BoundsInt bounds = ObstacleSpawnArea.cellBounds;
        foreach (var position in bounds.allPositionsWithin)
        {
            if (ObstacleSpawnArea.HasTile(position))
            {
                positions.Add(position);
            }
        }

        if (positions.Count == 0)
        {
            return Vector2.zero;
        }

        Vector3Int randomCell = positions[Random.Range(0, positions.Count)];
        return ObstacleSpawnArea.CellToWorld(randomCell);
    }

    private Vector2 GetRandomPosition_Enemy()
    {
        List<Vector3Int> positions = new();

        BoundsInt bounds = EnemySpawnArea.cellBounds;
        foreach (var position in bounds.allPositionsWithin)
        {
            if (EnemySpawnArea.HasTile(position))
            {
                positions.Add(position);
            }
        }

        if (positions.Count == 0)
        {
            return Vector2.zero;
        }

        Vector3Int randomCell = positions[Random.Range(0, positions.Count)];
        return EnemySpawnArea.CellToWorld(randomCell);
    }

    bool IsOverLapping(Vector2 randomPosition, BoxCollider2D collider)
    {
        return Physics2D.OverlapBox(randomPosition, collider.size * 1.5f, 0);
    }
}