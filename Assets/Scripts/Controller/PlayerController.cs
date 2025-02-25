using UnityEngine;

public class PlayerController : ObjectController
{
    public float attackRange = 5f;
    private AnimationHandler shadowHandler;
    private Transform HandLight;

    protected override void Initialize()
    {
        base.Initialize();

        Transform shadowRenderer = transform.Find(Define.ShadowRenderer);
        if (shadowRenderer == null)
        {
            Debug.LogWarning($"Failed to Find({Define.ShadowRenderer})\nFrom: {gameObject.name}");
            Destroy(gameObject);
        }
        else
        {
            shadowHandler = shadowRenderer.GetOrAddComponent<AnimationHandler>();
        }

        HandLight = transform.Find(nameof(HandLight));

        Managers.Camera.Target = transform;
        Managers.Game.Player = this;
    }

    protected override void Jumping()
    {
        base.Jumping();
        shadowHandler.Jump(jumping, Vector2.down);
    }

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
        HandleLighting();
    }

    private void CheckMonstersInRange()
    {
        Collider2D[] monstersInRange = Physics2D.OverlapCircleAll(transform.position, statHandler.AttackRange, LayerMask.GetMask("Monster"));

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

    private void HandleLighting()
    {
        if (moving)
        {
            lookDirection = moveDirection;
        }

        var z = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90.0f;
        HandLight.transform.rotation = Quaternion.Euler(0.0f, 0.0f, z);
    }
}