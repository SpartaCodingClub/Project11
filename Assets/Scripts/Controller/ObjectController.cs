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
    protected Vector2 direction;

    protected StatHandler statHandler;
    private AnimationHandler animationHandler;

    private Rigidbody2D _rigidbody;

    protected override void Initialize()
    {
        base.Initialize();

        statHandler = gameObject.GetComponent<StatHandler>();
        animationHandler = transform.Find(Define.MainRenderer).GetOrAddComponent<AnimationHandler>();

        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (actionState == ActionState.Attack)
        {
            return;
        }
        actionState = ActionState.Idle;

        Moving();
        Jumping();
    }

    public override void Birth()
    {
        base.Birth();
        animationHandler.Birth();
    }

    public override void Stand()
    {
        base.Stand();
        animationHandler.Stand();
    }

    public override void Death()
    {
        base.Death();
        animationHandler.Death();
    }

    public virtual void Attack()
    {
        actionState = ActionState.Attack;
        animationHandler.Attack();
    }

    private void Moving()
    {
        bool moving = direction.magnitude > 0.0f;
        if (moving)
        {
            actionState = ActionState.Move;
        }

        animationHandler.Move(moving);
        _rigidbody.velocity = statHandler.MoveSpeed * direction;
    }

    private void Jumping()
    {
        float z = transform.position.z;
        bool jumping = z > 0.0f;
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

        animationHandler.Jump(jumping);
        animationHandler.transform.SetPositionY(transform.position.y + transform.position.z);
    }
}