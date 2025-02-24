using System;
using UnityEngine;

public class OnDestroyEnter : StateMachineBehaviour
{
    public event Action OnEnter;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnEnter?.Invoke();
}