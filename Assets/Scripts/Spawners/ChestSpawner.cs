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
    [SerializeField] private float spawnRadius = 2f;

    //로비맵 좌표
    [SerializeField] private float minX = -25f, maxX = 29f;
    [SerializeField] private float minY = -8f, maxY = 25f;
    //생성 제한구역 좌표
    [SerializeField] private float restrictMinX = -12f, restrictMaxX = -4.5f;
    [SerializeField] private float restrictMinY = 14.5f, restrictMaxY = 15.8f;

    public void SpawnChest()
    {
        int checkAttempt = 0;

        for (int i = 0; i < chestCount; i++)
        {
            //유효한 장소인지 체크
            bool isValid = false;
            int PosAttempt = 0;
            Vector2 randomPos;
            do
            {
                randomPos = GetRandomPosition();
                bool isRestrict = RestrictArea(randomPos);
                // Physics2D.OverlapCircle(point,radius) 를 사용해 일정 거리 내 중복 생성 방지
                bool isOverlap = Physics2D.OverlapCircle(randomPos, spawnRadius) != null;
                if (!isRestrict && !isOverlap)
                {
                    isValid = true;
                }
                PosAttempt++;
            }
            while (!isValid && PosAttempt <= 10);

            if (isValid)
            {
                Instantiate(chestPrefabs, randomPos, Quaternion.identity);
            }
            else
            {
                checkAttempt++;
                if (checkAttempt >= 30)
                {
                    return;
                }
            }
        }
    }

    //생성 제한구역
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
}