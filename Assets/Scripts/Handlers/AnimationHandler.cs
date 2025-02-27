using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public bool HasSlow { get; set; }

    public Animator Animator { get; private set; }

    public AnimationAttackHandler AttackHandler { get; private set; }
    public AnimationDestroyHandler DestroyHandler { get; private set; }

    private void OnEnable()
    {
        Animator = gameObject.GetComponent<Animator>();

        AttackHandler = Animator.GetBehaviour<AnimationAttackHandler>();
        DestroyHandler = Animator.GetBehaviour<AnimationDestroyHandler>();

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

        Animator.SetBool(Define.HasSlow, HasSlow);
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