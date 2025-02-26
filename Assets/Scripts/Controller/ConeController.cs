using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeController : ObjectController
{
    public float random;
    public bool isMove;

    protected override void Initialize()
    {
        base.Initialize();

        //흰색 콘인지 검은색 콘인지 결정
        random = Random.Range(0f, 1f);
        Debug.Log($"현재 random 값: {random}");
        random = Mathf.Round(random);
        Debug.Log($"반올림 후의 random 값: {random}");

        //animationHandler.Animator.Play(Define.Stand, 0, random);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Bullet"))
        {
            Debug.Log("콘 쓰러짐");
            Death();
            animationHandler.Animator.SetFloat("ConeType", random);
        }
    }










}
