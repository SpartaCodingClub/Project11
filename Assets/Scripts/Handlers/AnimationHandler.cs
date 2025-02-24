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

    public void Birth() => Animator.Play(Define.Birth);
    public void Stand() => Animator.Play(Define.Stand);
    public void Death() => Animator.Play(Define.Death);
    public void Attack() => Animator.SetTrigger(Define.Attack);
    public void Move(bool value) => Animator.SetBool(Define.Move, value);
    public void Jump(bool value) => Animator.SetBool(Define.Jump, value);
}