using UnityEngine;
using VInspector;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(StatHandler))]
public class ObjectController : BaseController
{
    #region Inspector
    [ShowInInspector, ReadOnly] private ActionState actionState;
    #endregion

    public enum ActionState
    {
        Idle,
        Move,
        Jump,
        Attack
    }

    protected Vector2 lookDirection = Vector2.down;
    protected Vector2 moveDirection = Vector2.down;

    protected AnimationHandler animationHandler;
    protected StatHandler statHandler;

    private Rigidbody2D _rigidbody;

    protected bool moving;
    protected bool jumping;

    private float attackTimer;

    private void Update() => HandleLogic();
    private void FixedUpdate() => HandleAction();

    protected override void Initialize()
    {
        base.Initialize();

        statHandler = gameObject.GetComponent<StatHandler>();

        Transform mainRenderer = transform.Find(Define.MainRenderer);
        if (mainRenderer == null)
        {
            Debug.LogWarning($"Failed to Find({Define.MainRenderer})\nFrom: {gameObject.name}");
            Destroy(gameObject);
        }
        else
        {
            animationHandler = mainRenderer.GetOrAddComponent<AnimationHandler>();
        }

        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        animationHandler.AttackHandler.OnExit += Stand;
        animationHandler.DestroyHandler.OnEnter += Destroy;
    }

    protected virtual void HandleLogic()
    {
        attackTimer += Time.deltaTime;
    }

    protected virtual void HandleAction()
    {
        if (actionState == ActionState.Attack)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }
        actionState = ActionState.Idle;

        Moving();
        Jumping();
    }

    public override void Stand()
    {
        base.Stand();

        actionState = ActionState.Idle;
        animationHandler.Stand(lookDirection);
    }

    public override void Death()
    {
        base.Death();

        animationHandler.Death(lookDirection);
    }

    public virtual void Attack()
    {
        if (moving || jumping)
        {
            return;
        }

        if (attackTimer < statHandler.AttackDelay)
        {
            return;
        }

        actionState = ActionState.Attack;
        animationHandler.Attack(lookDirection);
        animationHandler.Animator.SetFloat(Define.AttackSpeed, statHandler.AttackSpeed);
        attackTimer = 0.0f;
    }

    protected virtual void Moving()
    {
        moving = moveDirection.magnitude > 0.0f; ;
        if (moving)
        {
            actionState = ActionState.Move;
        }

        animationHandler.Move(moving, moveDirection);
        _rigidbody.velocity = statHandler.MoveSpeed * moveDirection;
    }

    protected virtual void Jumping()
    {
        float z = transform.position.z;

        jumping = z > 0.0f;
        if (jumping || statHandler.VelocityZ > 0.0f)
        {
            actionState = ActionState.Jump;
            statHandler.VelocityZ -= statHandler.Gravity * Time.deltaTime;

            z += statHandler.VelocityZ * Time.deltaTime;
            if (z > 0.0f)
            {
                transform.SetPositionZ(z);
            }
            else
            {
                statHandler.VelocityZ = 0.0f;
                transform.SetPositionZ(0.0f);
            }
        }

        animationHandler.Jump(jumping, lookDirection);
        animationHandler.transform.SetPositionY(transform.position.y + transform.position.z);
    }
}