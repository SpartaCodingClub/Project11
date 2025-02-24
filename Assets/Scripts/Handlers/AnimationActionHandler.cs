using System;
using UnityEngine;

public class AnimationActionHandler : StateMachineBehaviour
{
    public ActionState ActionState;

    public event Action OnEnter;
    public event Action OnExit;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnEnter?.Invoke();
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnExit?.Invoke();
}