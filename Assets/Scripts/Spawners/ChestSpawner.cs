using System.Collections;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    #region Inspector
    [SerializeField]
    private int chestCount = 10;

    // 중복방지 거리
    [SerializeField]
    private float spawnRadius = 3f;
    #endregion

    private readonly string CHEST = "Chest";
    private readonly float MIN_X = -20.5f, MAX_X = 24.5f;
    private readonly float MIN_Y = -4.1f, MAX_Y = 20.5f;

    //콜라이더 박스 사이즈
    private readonly Vector2 boxSize = new(3.0f, 3.0f);

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        for (int i = 0; i < chestCount; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                Vector2 randomPos = GetRandomPosition();

                var colliders = Physics2D.OverlapBoxAll(randomPos, boxSize, 0);
                if (colliders.Length > 0)
                {
                    continue;
                }

                Managers.Resource.Instantiate(CHEST, null, randomPos);
                yield return new WaitForSeconds(0.2f);
                break;
            }
        }
    }

    //랜덤 생산구역
    public Vector2 GetRandomPosition()
    {
        return new Vector2(
            Random.Range(MIN_X, MAX_X),
            Random.Range(MIN_Y, MAX_Y)
        );
    }
}