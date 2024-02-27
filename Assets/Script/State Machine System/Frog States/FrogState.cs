using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogState : ScriptableObject, IState
{
    protected Animator animator;
    protected Frog frog;
    protected FrogStateMachine stateMachine;
    public void Init(Animator a, FrogStateMachine psm, Frog f)
    {
        animator = a;
        stateMachine = psm;
        frog = f;
    }
    public virtual void Enter() { }

    public virtual void Exit() { }


    public virtual void LogicUpdate() { }


    public virtual void PhysicUpdate() { }
}
