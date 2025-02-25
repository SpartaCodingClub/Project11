using UnityEngine;

public class EnemyController : ObjectController
{
    PlayerController player;

    private bool onTriggerStay;

    protected override void Initialize()
    {
        base.Initialize();

        //테스트를 위한 임시 코드
        player = FindObjectOfType<PlayerController>();

    }

    public override void Attack()
    {
        base.Attack();

        //투사체를 날리는 애들이라면 투사체 생성 로직 (날아가는건 별개 스크립트로)
        //예시: n초마다 한 번씩 투사체를 날린다.

        //좀비는 근접 공격이니까 충돌한 순간 플레이어의 HP를 깎아주면 될 것임
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        onTriggerStay = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //충돌한 오브젝트가 플레이어라면 (자주 쓸 것 같아서 define에 추가해야 할 듯)
        if (collision.collider.CompareTag("Player"))
        {
            //공격 (근접 몹 한정)
            Attack();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onTriggerStay = false;
    }

    protected override void HandleLogic()
    {
        base.HandleLogic();
        if (onTriggerStay)
        {
            moveDirection = Vector2.zero;
        }
        else
        {
            moveDirection = lookDirection = (player.transform.position - transform.position).normalized;
        }


        //direction에 값 넣어주면 이동

        //플레이어의 위치


        //플레이어를 향해 이동하려면 or 플레이어를 향해 발사체를 날리려면
        //플레이어의 위치를 알 수 있어야 함
    }
}
