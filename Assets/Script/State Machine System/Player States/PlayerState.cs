using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : ScriptableObject,IState
{
    protected float currentSpeed;
    protected Animator animator;
    protected PlayerInput input;
    protected PlayerStateMachine stateMachine;
    protected PlayerController player;
    protected float stateStartTime;
    protected float StateDuration => Time.time - stateStartTime;
    protected bool IsAnimatorFinished => StateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;
    public void Init(Animator a, PlayerStateMachine psm, PlayerController pc, PlayerInput i)
    {
        animator = a;
        stateMachine = psm;
        player = pc;
        input = i;
    }
    public virtual void Enter() { }      
           
    public virtual void Exit() { }
     
           
    public virtual void LogicUpdate() { }


    public virtual void PhysicUpdate() { }

}
