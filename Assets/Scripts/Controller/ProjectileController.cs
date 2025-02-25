using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : BaseController
{
    ProjectileHandler projectileHandler;
    Rigidbody2D _rigidbody;
    Animator _animator;
    
    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
    }
    public void Init(ProjectileHandler projectileHandler)
    {
        this.projectileHandler = projectileHandler;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        int monsterLayer = LayerMask.NameToLayer("Monster");
        int playerLayer = LayerMask.NameToLayer("Player");

        //if(collision.gameObject.layer != ~(monsterLayer|playerLayer))
        //_animator.SetTrigger("OnEffect");
        //Destroy(gameObject);
    }
    public void TargetToDirection(Vector2 direction)
    {
        _rigidbody.velocity = direction.normalized * 10f;
    }
}

