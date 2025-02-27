using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    private static readonly string CHEST = "Chest";

    [SerializeField] private GameObject chestPrefabs;
    [SerializeField] private int chestCount = 10;
    // 중복방지 거리
    [SerializeField] private float spawnRadius = 3f;

    //로비맵 좌표    
    [SerializeField] private float minX = -20.5f, maxX = 24.5f;
    [SerializeField] private float minY = -4.1f, maxY = 20.5f;
    //콜라이더 박스 사이즈
    [SerializeField] private Vector2 boxSize = new Vector2(3f, 3f);

    private void Start()
    {
        SpawnChest();
    }

    public void SpawnChest()
    {
        for (int i = 0; i < chestCount; i++)
        {
            //유효한 장소인지 체크
            bool isValid = false;
            int PosAttempt = 0;
            Vector2 randomPos;

            while (PosAttempt <= 50)
            {
                randomPos = GetRandomPosition();
                // Physics2D.OverlapCircle() 를 사용해 일정 거리 이내에 생성 및 겹침 방지
                bool isOverlap = Physics2D.OverlapCircle(randomPos, spawnRadius) != null;
                // Physics2D.OverlapBox() 를 사용해 일정 거리 이내에 오브젝트랑 생성 및 겹침 방지
                bool isOverlapBoxCollider = IsOverlapBoxCollider(randomPos);
                if (!isOverlap && !isOverlapBoxCollider)
                {
                    isValid = true;
                    break;
                }
                PosAttempt++;
            }

            if (isValid)
            {
                randomPos = GetRandomPosition();
                Managers.Resource.Instantiate(CHEST, null, randomPos);
            }
        }
    }

    public bool IsOverlapBoxCollider(Vector2 position)
    {
        return Physics2D.OverlapBox(position, boxSize + new Vector2(spawnRadius, spawnRadius), 0f) != null;
    }

    //랜덤 생산구역
    public Vector2 GetRandomPosition()
    {
        return new Vector2(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY)
        );
    }

    //플레이어가 충돌시 스포너 실행
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        Debug.LogWarning("Player");

    //    }
    //}
}