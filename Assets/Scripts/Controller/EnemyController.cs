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

        //statHandler에 넣어주는게 아니라
        //statHandler에서 받아와야 할 것 같기도.......
        //강의 끝나면 구조 여쭤보기. 일단은 임시 코드!!!

        //Zonbie
        //statHandler.AttackRange = 1.0f;
        //statHandler.AttackDelay = 1.0f;
        //statHandler.HP = 10.0f;
        //statHandler.Damage = 1.0f;
        //statHandler.MoveSpeed = 1f;

        //Seeder
        statHandler.AttackRange = 10.0f;
        statHandler.AttackDelay = 1.0f;
        statHandler.HP = 10.0f;
        statHandler.Damage = 1.0f;
        statHandler.MoveSpeed = 0f;

        //AttackRange에 따라 Collider의 크기를 변경
        CircleCollider2D collider = GetComponentInChildren<CircleCollider2D>();
        collider.isTrigger = true;
        collider.radius = statHandler.AttackRange;

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

    protected override void HandleLogic()
    {
        base.HandleLogic();

        lookDirection = (Managers.Game.Player.transform.position - transform.position).normalized;
        if (onTriggerStay)
        {
            //만약 공격중인 상태라면 이동을 멈춰라
            moveDirection = Vector2.zero;
        }
        else
        {
            moveDirection = lookDirection;
        }
    }
}