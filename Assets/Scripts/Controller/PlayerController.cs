using UnityEngine;

public class PlayerController : ObjectController
{
    private Transform HandLight;
    private Transform HandPivot;

    private AnimationHandler ShadowRenderer;
    private ProjectileHandler projectileHandler;

    private Transform target;

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
        projectileHandler = gameObject.GetComponent<ProjectileHandler>();

        Managers.Camera.Target = transform;
        Managers.Game.Player = this;
    }

    protected override void Start()
    {
        base.Start();

        animationHandler.AttackHandler.OnEnter += () =>
        {
            projectileHandler.RangeAttack(ProjectilePattern.Default, HandPivot.position, target.position);
            Managers.Skill.ApplySkill(2, this);
        };
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

        if (Input.GetKey(KeyCode.Space))
        {
            if (transform.position.z == 0)
            {
                statHandler.VelocityZ = statHandler.JumpPower;
            }

            return;
        }

        Collider2D closestMonster = GetClosestMonster();
        if (closestMonster == null)
        {
            return;
        }

        AttackTargetMonster(closestMonster);
    }
    protected override void HandleAction()
    {
        base.HandleAction();
        HandleLighting();
    }

    private Collider2D GetClosestMonster()
    {
        Collider2D[] monsters = Physics2D.OverlapCircleAll(transform.position, statHandler.AttackRange, LayerMask.GetMask(Define.Monster));

        Collider2D closestMonster = null;
        float closestDistance = Mathf.Infinity;

        foreach (var monster in monsters)
        {
            if (monster == null)
            {
                continue;
            }

            float distance = Vector2.Distance(transform.position, monster.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestMonster = monster;
            }
        }

        return closestMonster;
    }

    private void AttackTargetMonster(Collider2D closestMonster)
    {
        // 캐릭터 방향 설정 후
        Vector3 targetPosition = closestMonster.transform.position;
        lookDirection = (targetPosition - transform.position).normalized;

        // 공격
        Attack();

        // AnimationHandler.AttackHandler의 OnEnter 이벤트를 처리하기 위함
        target = closestMonster.transform;
    }

    private void HandleLighting()
    {
        if (moving)
        {
            lookDirection = moveDirection;
        }

        var z = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90.0f;
        HandLight.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, z);

        HandPivot.localPosition = lookDirection;
    }
}