using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "StateMachine/PlayerState/CoyoteTime", fileName = "PlayerState_CoyoteTime")]
public class PlayerState_CoyoteTime : PlayerState
{
    public float coyoteTime = 0.1f;
    private float g;
    public override void Enter()
    {
        animator.Play("Run");
        g = player.GetGravity();
        player.SetGravity(0);
        stateStartTime = Time.time;
    }
    public override void Exit()
    {
        player.SetGravity(g);
    }
    public override void LogicUpdate()
    {
        if (player.playerIsAllowJump())
        {
            stateMachine.SwitchState(typeof(PlayerState_JumpOnGround));
        }
        if (StateDuration > coyoteTime || !Input.GetButton("Horizontal"))
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
    }
    public override void PhysicUpdate()
    {
        player.PlayerMove();
    }
}
