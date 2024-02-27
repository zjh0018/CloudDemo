using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/FrogState/Jump", fileName = "FrogState_Jump")]
public class FrogState_Jump : FrogState
{
    public override void Enter()
    {
        animator.Play("Jump");
    }
    public override void LogicUpdate()
    {
        if (frog.isFalling && !frog.isGround)
        {
            stateMachine.SwitchState(typeof(FrogState_Fall));
        }


    }
    public override void PhysicUpdate()
    {

    }
}
