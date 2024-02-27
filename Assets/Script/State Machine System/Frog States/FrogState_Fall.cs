using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/FrogState/Fall", fileName = "FrogState_Fall")]
public class FrogState_Fall : FrogState
{
    public override void Enter()
    {
        animator.Play("Fall");
    }
    public override void LogicUpdate()
    {
        if (frog.isGround)
        {
            stateMachine.SwitchState(typeof(FrogState_Idle));
        }


    }
    public override void PhysicUpdate()
    {

    }
}
