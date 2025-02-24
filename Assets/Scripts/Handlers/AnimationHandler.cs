using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public AnimationStateHandler Destroyed { get; private set; }

    private void Awake()
    {
        Animator = gameObject.GetComponent<Animator>();
        Destroyed = Animator.GetBehaviour<AnimationStateHandler>();
    }

    public virtual void Birth() => Animator.Play(Define.Birth);
    public virtual void Stand() => Animator.Play(Define.Stand);
    public virtual void Death() => Animator.Play(Define.Death);
    public virtual void Attack() => Animator.SetTrigger(Define.Attack);

    public virtual void Move(bool value, Vector2 direction)
    {
        Animator.SetBool(Define.Move, value);
    }

    public virtual void Jump(bool value, Vector2 direction)
    {
        Animator.SetBool(Define.Jump, value);
    }
}