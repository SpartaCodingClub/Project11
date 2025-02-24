using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectController;

public class EnemyController : ObjectController
{
    private void Update()
    {
        //direction에 값 넣어주면 이동

        //만약 플레이어와의 거리가 일정 이상 가까워진다면
        //Attack()으로 넘어감
    }

    public override void Attack()
    {
        base.Attack();


    }


}
