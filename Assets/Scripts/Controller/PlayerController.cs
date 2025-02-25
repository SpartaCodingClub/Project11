using System;
using UnityEngine;

public class PlayerController : ObjectController
{
    public float attackRange = 5f;
    private AnimationHandler ShadowRenderer;

    private Transform HandLight;
    private Transform HandPivot;

    private Collider2D target;

    private Action<Vector2, Vector2> testFunc;

    protected override void Initialize()
    {
        base.Initialize();

        ShadowRenderer = gameObject.FindComponent<AnimationHandler>(nameof(ShadowRenderer));
        if (ShadowRenderer == null)
        {
            Debug.LogWarning($"Failed to Find({nameof(ShadowRenderer)})\nFrom: {gameObject.name}");
            Destroy(gameObject);
        }

        HandLight = gameObject.FindComponent<Transform>(nameof(HandLight));
        HandPivot = gameObject.FindComponent<Transform>(nameof(HandPivot));

        Managers.Camera.Target = transform;
        Managers.Game.Player = this;
    }

    protected override void Jumping()
    {
        base.Jumping();
        ShadowRenderer.Jump(jumping, Vector2.down);
    }

    public override void Death()
    {
        base.Death();
        ShadowRenderer.Death(lookDirection);
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

        target = monsterCollider;
        Attack();
    }

    private void HandleLighting()
    {
        if (moving)
        {
            lookDirection = moveDirection;
        }

        var z = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90.0f;
        HandLight.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, z);

        HandPivot.localPosition = lookDirection * 0.5f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.collider.
        }
    }
}