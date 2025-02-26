using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    [SerializeField] private GameObject chestPrefabs;
    [SerializeField] private int chestCount = 10;
    // 중복방지 거리
    [SerializeField] private float spawnRadius = 1f;

    //로비맵 좌표    
    [SerializeField] private float minX = -20.5f, maxX = 24.5f;
    [SerializeField] private float minY = -4.1f, maxY = 20.5f;
    //생성 제한구역 좌표
    [SerializeField] private float restrictMinX = -12f, restrictMaxX = 14.5f;
    [SerializeField] private float restrictMinY = 3.0f, restrictMaxY = 15.8f;


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

            while (PosAttempt <= 10)
            {   
                randomPos = GetRandomPosition();
                bool isRestrict = RestrictArea(randomPos);
                // Physics2D.OverlapCircle(point,radius) 를 사용해 일정 거리 내 중복 생성 방지
                bool isOverlap = Physics2D.OverlapCircle(randomPos, spawnRadius) != null;
                if (!isRestrict && !isOverlap)
                {
                    isValid = true;
                    break;
                }
                PosAttempt++;
            }

            if (isValid)
            {
                randomPos = GetRandomPosition();
                Instantiate(chestPrefabs, randomPos, Quaternion.identity);
            }
            
        }
    }

    //생성 제한구역 , 생각한대로 작동은 안하는것 같음 , 박스 콜라이더가 있는 부분에는 생성 안되는 기능으로 변경하고 싶음
    public bool RestrictArea(Vector2 position)
    {
        return position.x > restrictMinX && position.x < restrictMaxX &&
               position.y > restrictMinY && position.y < restrictMaxY;
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