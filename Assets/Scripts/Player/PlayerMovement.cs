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

    private bool isDead = false; // 캐릭터 사망 상태

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        anim = GetComponentInChildren<Animator>(); // Animator 컴포넌트 가져오기
    }

    void Update()
    {
        if (isDead) return; // 죽었으면 입력을 막음

        ProccessInputs(); // 입력 처리
        animate(); // 애니메이션 처리
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            rb.velocity = Vector2.zero; // 죽었으면 이동 멈춤
            return;
        }

        rb.velocity = input * speed; // 입력 값에 따라 속도 적용
    }

    void ProccessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // 수평 입력 값
        float moveY = Input.GetAxisRaw("Vertical"); // 수직 입력 값

        Vector2 moveInput = new Vector2(moveX, moveY);

        if (moveInput != Vector2.zero) // 플레이어가 움직일 때만 lastMoveDirection 업데이트
        {
            lastMoveDirection = moveInput;
        }

        input = moveInput.normalized; // 대각선 이동 속도 보정
    }

    void animate()
    {
        anim.SetFloat("MoveX", input.x); // 현재 X축 이동 값 전달
        anim.SetFloat("MoveY", input.y); // 현재 Y축 이동 값 전달
        anim.SetFloat("MoveMagnitude", input.magnitude); // 이동 여부 전달
        anim.SetFloat("LastMoveX", lastMoveDirection.x); // 마지막 X축 이동 값 전달
        anim.SetFloat("LastMoveY", lastMoveDirection.y); // 마지막 Y축 이동 값 전달
    }

    public void Die()
    {
        isDead = true; // 사망 상태 설정
        anim.SetBool("IsDeath", true); // Death 애니메이션 실행

        // 마지막 방향을 기반으로 죽는 애니메이션 설정
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveY", lastMoveDirection.y);

        rb.velocity = Vector2.zero; // 이동 정지
    }
}

