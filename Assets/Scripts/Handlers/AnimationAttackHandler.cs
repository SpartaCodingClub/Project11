using System;
using UnityEngine;

public class AnimationAttackHandler : StateMachineBehaviour
{
    public event Action OnEnter;
    public event Action OnExit;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnExit?.Invoke();
}