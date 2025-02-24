using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0.5f; // 이동 속도 설정
    private Rigidbody2D rb; // Rigidbody2D 컴포넌트
    private Vector2 input; // 입력 값 저장

    Animator anim; // 애니메이터 컴포넌트
    private Vector2 lastMoveDirection; // 마지막 이동 방향 저장

    // Start는 게임이 시작될 때 한 번 호출됨
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        anim = GetComponentInChildren<Animator>(); // Animator 컴포넌트 가져오기
    }

    // Update는 매 프레임마다 호출됨
    void Update()
    {
        ProccessInputs(); // 입력 처리
        animate(); // 애니메이션 처리

    }

    private void FixedUpdate()
    {
        rb.velocity = input * speed; // 입력 값에 따라 속도 적용
    }

    // 플레이어 입력을 받아 처리하는 함수
    void ProccessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // 수평 입력 값
        float moveY = Input.GetAxisRaw("Vertical"); // 수직 입력 값

        // 입력이 멈췄을 때 마지막 이동 방향 저장
        if ((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0))
        {
            lastMoveDirection = input;
        }

        // 입력 값을 저장
        input.x = moveX;
        input.y = moveY;

        input.Normalize(); // 대각선 이동 시 속도 보정
    }

    // 애니메이션을 처리하는 함수
    void animate()
    {
        anim.SetFloat("MoveX", input.x); // 현재 X축 이동 값 전달
        anim.SetFloat("MoveY", input.y); // 현재 Y축 이동 값 전달
        anim.SetFloat("MoveMagnitude", input.magnitude); // 이동 여부 전달
        anim.SetFloat("LastMoveX", lastMoveDirection.x); // 마지막 X축 이동 값 전달
        anim.SetFloat("LastMoveY", lastMoveDirection.y); // 마지막 Y축 이동 값 전달
    }

}

