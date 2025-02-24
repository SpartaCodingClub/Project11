using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator Animator { get; private set; }

    public AnimationActionHandler Attacked { get; private set; }
    public AnimationStateHandler Destroyed { get; private set; }

    private void Awake()
    {
        Animator = gameObject.GetComponent<Animator>();

        foreach (var stateHandler in Animator.GetBehaviours<AnimationStateHandler>())
        {
            switch (stateHandler.State)
            {
                case State.Destroyed:
                    Destroyed = stateHandler;
                    break;
                case State.Birth:
                    break;
                case State.Stand:
                    break;
                case State.Death:
                    break;
            }
        }

        foreach (var actionHanlder in Animator.GetBehaviours<AnimationActionHandler>())
        {
            switch (actionHanlder.ActionState)
            {
                case ActionState.Idle:
                    break;
                case ActionState.Move:
                    break;
                case ActionState.Jump:
                    break;
                case ActionState.Attack:
                    Attacked = actionHanlder;
                    break;
            }
        }

        SetDirection(Vector2.down);
    }

    public virtual void Stand(Vector2 direction)
    {
        SetDirection(direction);
        Animator.Play(Define.Stand);
    }

    public virtual void Death(Vector2 direction)
    {
        SetDirection(direction);
        Animator.Play(Define.Death);
    }

    public virtual void Attack(Vector2 direction)
    {
        SetDirection(direction);
        Animator.SetTrigger(Define.Attack);
    }

    public virtual void Move(bool value, Vector2 direction)
    {
        if (value)
        {
            SetDirection(direction);
        }

        Animator.SetBool(Define.Move, value);
    }

    public virtual void Jump(bool value, Vector2 direction)
    {
        if (value)
        {
            SetDirection(direction);
        }

        Animator.SetBool(Define.Jump, value);
    }

    private void SetDirection(Vector2 direction)
    {
        Animator.SetFloat(Define.PosX, direction.x);
        Animator.SetFloat(Define.PosY, direction.y);
    }
}