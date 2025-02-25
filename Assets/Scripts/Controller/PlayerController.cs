using UnityEngine;

public class PlayerController : ObjectController
{
    public float attackRange = 5f;

    protected override void HandleLogic()
    {
        base.HandleLogic();

        var horizontal = Input.GetAxisRaw(Define.Horizontal);
        var vertical = Input.GetAxisRaw(Define.Vertical);
        moveDirection = new Vector2(horizontal, vertical).normalized;

        if (Input.GetKey(KeyCode.Space) && transform.position.z == 0)
        {
            statHandler.VelocityZ = statHandler.JumpPower;
        }
    }
    protected override void HandleAction()
    {
        base.HandleAction();
        CheckMonstersInRange();
    }

    private void CheckMonstersInRange()
    {
        Collider2D[] monstersInRange = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Monster"));

        foreach (var monster in monstersInRange)
        {
            if (monster != null)
            {
                AttackMonster(monster);
            }
        }
    }
    private void AttackMonster(Collider2D monsterCollider)
    {

        var monster = monsterCollider.GetComponent<MonsterController>(); // 몬스터 컨트롤러가 있는 경우
        if (monster != null)
        {
            monster.TakeDamage(10); // 데미지 처리
        }
        Vector2 monsterPosition = monsterCollider.transform.position;
        Vector2 playerPosition = transform.position;
        lookDirection = (monsterPosition - playerPosition).normalized;

        Attack();

        //Attack(); // 공격 애니메이션 실행
    }
}