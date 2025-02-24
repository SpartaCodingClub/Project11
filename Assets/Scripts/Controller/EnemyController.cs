using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectController;

public class EnemyController : ObjectController
{
    PlayerTest player;

    //플레이어의 위치
    Vector2 playerDirection = Vector2.zero;

    protected override void Initialize()
    {
        base.Initialize();

        //테스트를 위한 임시 코드
        player = Object.FindObjectOfType<PlayerTest>();

    }


    public override void Attack()
    {
        base.Attack();

        Debug.Log("Zombie Attack");

        //투사체를 날리는 애들이라면 투사체 생성 로직 (날아가는건 별개 스크립트로)
        //예시: n초마다 한 번씩 투사체를 날린다.

        //좀비는 근접 공격이니까 충돌한 순간 플레이어의 HP를 깎아주면 될 것임


    }

    public void OnDamage()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //충돌한 오브젝트가 플레이어라면 (자주 쓸 것 같아서 define에 추가해야 할 듯)
        if (collision.collider.CompareTag("Player"))
        {
            //공격 (근접 몹 한정)
            
            Attack();
        }
    }

    private void Update()
    {
        //direction에 값 넣어주면 이동

        //플레이어의 위치
        direction = (player.transform.position - this.transform.position).normalized;
        Debug.Log(direction);

        //if(ActionState == ActionState.Attack)

        //플레이어를 향해 이동하려면 or 플레이어를 향해 발사체를 날리려면
        //플레이어의 위치를 알 수 있어야 함
    }


}
