using UnityEngine;

public class PlayerController : ObjectController
{
    private static readonly string JUMP_EFFECT = "JumpEffect";

    private Transform HandLight;
    private Transform HandPivot;

    private AnimationHandler ShadowRenderer;
    private ProjectileHandler projectileHandler;

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

    public override void Birth()
    {
        base.Birth();
        animationHandler.AttackHandler.OnEnter += () => projectileHandler.RangeAttack(HandPivot.position, lookDirection);
    }

    public override void Death()
    {
        base.Death();
        ShadowRenderer.Death(lookDirection);
    }

    protected override void Jumping()
    {
        base.Jumping();
        ShadowRenderer.Jump(jumping, Vector2.down);
    }

    protected override void HandleLogic()
    {
        base.HandleLogic();

        var horizontal = Input.GetAxisRaw(Define.Horizontal);
        var vertical = Input.GetAxisRaw(Define.Vertical);
        moveDirection = new Vector2(horizontal, vertical).normalized;

        if (Input.GetKey(KeyCode.Space))
        {
            if (transform.position.z != 0)
            {
                return;
            }

            statHandler.VelocityZ = statHandler.JumpPower;

            Vector2 newPosition = transform.position;
            newPosition.y -= 0.6f;

            Managers.Resource.Instantiate(JUMP_EFFECT, null, newPosition, Define.EFFECT).GetComponent<ObjectController>().Death();
        }

        Collider2D closestMonster = GetClosestMonster();
        if (closestMonster != null)
        {
            AttackTargetMonster(closestMonster);
        }
    }
    protected override void HandleAction()
    {
        base.HandleAction();
        HandleLighting();
    }

    private Collider2D GetClosestMonster()
    {
        int layerMask = LayerMask.GetMask(Define.Monster, Define.Boss);
        Collider2D[] monsters = Physics2D.OverlapCircleAll(transform.position, statHandler.AttackRange, layerMask);

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
        if (moving || jumping)
        {
            return;
        }

        // 캐릭터 방향 설정 후
        Vector3 targetPosition = closestMonster.transform.position;
        lookDirection = (targetPosition - transform.position).normalized;
        HandPivot.localPosition = lookDirection;

        // 공격
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
    }
}