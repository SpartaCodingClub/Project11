using UnityEngine;

public enum EnemyType
{
    Zombie,
    Seeder
}

public class EnemyController : ObjectController
{
    private bool onTriggerStay;
    private ProjectileHandler projectileHandler;

    protected override void Initialize()
    {
        base.Initialize();

        //AttackRange에 따라 Collider의 크기를 변경
        CircleCollider2D collider = GetComponentInChildren<CircleCollider2D>();
        collider.isTrigger = true;
        collider.radius = StatHandler.AttackRange;

        //this.gameObject.AddComponent<CircleCollider2D>();
        projectileHandler = gameObject.GetComponent<ProjectileHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.Player) == false)
        {
            return;
        }

        onTriggerStay = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsDead)
        {
            return;
        }

        if (collision.CompareTag(Define.Player) == false)
        {
            return;
        }

        Attack();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.Player) == false)
        {
            return;
        }

        onTriggerStay = false;
    }

    public override void Birth()
    {
        base.Birth();
        animationHandler.AttackHandler.OnEnter += () => projectileHandler.RangeAttack(transform.position, lookDirection);
    }

    public override void Death()
    {
        actionState = ActionState.Idle;

        moving = false;
        jumping = false;
        onTriggerStay = false;

        moveDirection = Vector2.zero;
        _rigidbody.velocity = Vector2.zero;

        animationHandler.Animator.SetBool(Define.Move, false);
        animationHandler.Animator.SetBool(Define.Jump, false);

        base.Death();
    }

    protected override void HandleLogic()
    {
        if (IsDead)
        {
            return;
        }

        base.HandleLogic();

        Vector3 distance = Managers.Game.Player.transform.position - transform.position;
        lookDirection = distance.normalized;

        //만약 공격중이거나 최소 사거리에서 벗어나면 이동을 멈춘다.
        if (onTriggerStay || distance.magnitude > 8.0f)
        {
            moveDirection = Vector2.zero;
            return;
        }

        moveDirection = lookDirection;
    }
}