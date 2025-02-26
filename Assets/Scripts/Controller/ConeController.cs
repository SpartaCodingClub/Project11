using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeController : ObjectController
{
    float random;

    protected override void Initialize()
    {
        base.Initialize();
        random = Random.Range(0f, 1f);
        animationHandler.Animator.Play(Define.Stand, 0, random);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Bullet"))
        {
            Debug.Log("ÄÜ ¾²·¯Áü");
            Death();
            animationHandler.Animator.SetFloat("Type", random);
        }
    }










}
