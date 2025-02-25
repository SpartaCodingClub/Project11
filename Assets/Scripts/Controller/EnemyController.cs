using UnityEngine;

public enum EnemyType
{
    Zombie,
    Seeder
}

public class EnemyController : ObjectController
{
    PlayerController player;

    private bool onTriggerStay;

    protected override void Initialize()
    {
        base.Initialize();

        player = FindObjectOfType<PlayerController>();

        //좀비
        //나중에 시트와 연결할 수 있으면 더 좋을 듯
        statHandler.AttackRange = 3.0f;
        statHandler.AttackDelay = 1.0f;
        statHandler.HP = 10.0f;
        statHandler.Damage = 1.0f;

    }

    public override void Attack()
    {
        base.Attack();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerStay = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.Player))
        {
            //공격 (근접 몹 한정)
            Attack();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onTriggerStay = false;
    }

    protected override void HandleLogic()
    {
        base.HandleLogic();
        if (onTriggerStay)
        {
            //만약 공격중인 상태라면 이동을 멈춰라
            moveDirection = Vector2.zero;
        }
        else
        {
            moveDirection = lookDirection = (player.transform.position - transform.position).normalized;
        }

        //플레이어를 향해 이동하려면 or 플레이어를 향해 발사체를 날리려면
        //플레이어의 위치를 알 수 있어야 함
    }
}
