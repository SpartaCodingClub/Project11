using UnityEngine;
using VInspector;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(StatHandler))]
public class ObjectController : BaseController
{
    #region Inspector
    [ShowInInspector, ReadOnly] protected ActionState actionState;
    #endregion

    public enum ActionState
    {
        Idle,
        Move,
        Jump,
        Attack
    }

    public StatHandler StatHandler { get; private set; }

    protected Vector2 lookDirection = Vector2.down;
    protected Vector2 moveDirection;

    protected Transform MainRenderer;
    protected AnimationHandler animationHandler;

    protected Rigidbody2D _rigidbody;

    protected bool moving;
    protected bool jumping;

    private float attackTimer;

    private void Update() => HandleLogic();
    private void FixedUpdate() => HandleAction();

    protected override void Initialize()
    {
        base.Initialize();

        MainRenderer = transform.Find(nameof(MainRenderer));
        if (MainRenderer == null)
        {
            Debug.LogWarning($"Failed to Find({nameof(MainRenderer)})\nFrom: {gameObject.name}");
            Destroy(gameObject);

            return;
        }

        animationHandler = MainRenderer.GetOrAddComponent<AnimationHandler>();
        StatHandler = gameObject.GetComponent<StatHandler>();

        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    protected virtual void HandleLogic()
    {
        if (StatHandler.CurrentHP <= 0 && !IsDead)
        {
            Death();
            return;
        }

        attackTimer += Time.deltaTime;
    }

    protected virtual void HandleAction()
    {
        if (IsDead)
        {
            return;
        }

        if (actionState == ActionState.Attack)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }
        actionState = ActionState.Idle;

        Moving();
        Jumping();
    }

    public override void Birth()
    {
        base.Birth();

        if (animationHandler.DestroyHandler != null)
        {
            animationHandler.DestroyHandler.OnEnter += Destroy;
        }

        animationHandler.AttackHandler.OnExit += Stand;

        Stand();
    }

    public override void Stand()
    {
        if (IsDead)
        {
            return;
        }

        base.Stand();

        actionState = ActionState.Idle;
        animationHandler.Stand(lookDirection);
    }

    public override void Death()
    {
        base.Death();

        animationHandler.Death(lookDirection);
    }

    public void Attack()
    {
        if (moving || jumping)
        {
            return;
        }

        if (attackTimer < StatHandler.AttackDelay)
        {
            return;
        }

        actionState = ActionState.Attack;
        animationHandler.Attack(lookDirection);
        animationHandler.Animator.SetFloat(Define.AttackSpeed, StatHandler.AttackSpeed);
        attackTimer = 0.0f;
    }

    protected void Moving()
    {
        moving = moveDirection.magnitude > 0.0f;
        if (moving)
        {
            actionState = ActionState.Move;
        }

        animationHandler.Move(moving, moveDirection);
        _rigidbody.velocity = StatHandler.MoveSpeed * moveDirection;
    }

    protected virtual void Jumping()
    {
        if (StatHandler.JumpPower == 0.0f)
        {
            return;
        }

        float z = transform.position.z;
        jumping = z > 0.0f;

        if (jumping || StatHandler.VelocityZ > 0.0f)
        {
            actionState = ActionState.Jump;
            StatHandler.VelocityZ -= StatHandler.Gravity * Time.deltaTime;

            z += StatHandler.VelocityZ * Time.deltaTime;
            if (z > 0.0f)
            {
                transform.SetPositionZ(z);
                MainRenderer.SetPositionZ(z * -2.6f);
                _rigidbody.excludeLayers = LayerMask.GetMask(Define.Obstacle, Define.Monster);
            }
            else
            {
                Landing();
            }
        }

        MainRenderer.SetPositionY(transform.position.y + z);
        animationHandler.Jump(jumping, lookDirection);
    }

    protected virtual void Landing()
    {
        transform.SetPositionZ(0.0f);

        MainRenderer.SetPositionZ(0.0f);
        StatHandler.VelocityZ = 0.0f;
        _rigidbody.excludeLayers = 0;
    }
}