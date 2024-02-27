using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/FrogState/Idle", fileName = "FrogState_Idle")]
public class FrogState_Idle : FrogState
{
    public override void Enter()
    {
        animator.Play("Idle");
    }
    public override void LogicUpdate()
    {
        if (!frog.isFalling && !frog.isGround)
        {
            stateMachine.SwitchState(typeof(FrogState_Jump));
        }
        
        
    }
    public override void PhysicUpdate()
    {

    }
}
