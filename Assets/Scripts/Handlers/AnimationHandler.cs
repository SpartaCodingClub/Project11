using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void Birth() => animator.Play(Define.Birth);
    public void Stand() => animator.Play(Define.Stand);
    public void Death() => animator.Play(Define.Death);
    public void Attack() => animator.SetTrigger(Define.Attack);
    public void Move(bool value) => animator.SetBool(Define.Move, value);
    public void Jump(bool value) => animator.SetBool(Define.Jump, value);
}