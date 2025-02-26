using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeController : ObjectController
{
    Animator animator;

    public float random;
    public bool isMove;

    Vector2 direction;

    protected override void Initialize()
    {
        base.Initialize();

        //랜덤으로 흰색 콘인지 검은색 콘인지 결정
        random = Random.Range(0f, 1f);
        Debug.Log($"현재 random 값: {random}");

        //animationHandler를 사용하면 Nullreference가 떠서 새로 받아옴
        animator = GetComponentInChildren<Animator>();
        animator.SetFloat("MotionTime", random);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Bullet"))
        {
            collision.rigidbody.mass = 10;


            direction = (collision.transform.position - transform.position).normalized;

            animator.SetFloat("X", direction.x);
            animator.SetFloat("Y", direction.y);

            Debug.Log($"콘을 때린 방향: X:{direction.x} Y:{direction.y}");
            Debug.Log("콘 쓰러짐");

            //쓰러지면 충돌처리가 되지 않게끔
            this.GetComponent<Collider2D>().enabled = false;

            Death();
        }
    }










}
