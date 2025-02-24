using System;
using UnityEngine;

public class OnAttackExit : StateMachineBehaviour
{
    public event Action OnExit;
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnExit?.Invoke();
}