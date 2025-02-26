using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeController : ObjectController
{
    Animator animator;

    public float random;
    public bool isMove;

    Vector3 direction;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            direction = (collision.transform.position - transform.position).normalized;

            animator.SetFloat("X", direction.x);
            animator.SetFloat("Y", direction.y);

            Rigidbody2D rb = this.GetComponent<Rigidbody2D>();

            //저항 설정
            rb.mass = 1.0f;
            rb.drag = 3.0f;
            rb.angularDrag = 10.0f;

            //플레이어가 있는 반대 방향으로 튕겨나가도록 힘을 가한다.
            rb.AddForce(direction * -10.0f, ForceMode2D.Impulse);
            rb.AddTorque(-10f, ForceMode2D.Impulse);

            //쓰러지면 충돌처리가 되지 않게끔 처리
            this.GetComponent<Collider2D>().enabled = false;

            Death();
        }
    }


    protected override void HandleAction()
    {
        //지우면 안됩니다
    }

}
