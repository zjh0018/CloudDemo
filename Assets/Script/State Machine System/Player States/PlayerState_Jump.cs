using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/PlayerState/Jump", fileName = "PlayerState_Jump")]
public class PlayerState_Jump : PlayerState
{
    public override void Enter()
    {
        player.Jump();
        input.JumpInputBuffer = false;
    }
    public override void LogicUpdate()
    {
        if ((player.playerIsFalling() || input.StopJump) && !player.WallJump)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
        if (player.playerIsHurt())
        {
            stateMachine.SwitchState(typeof(PlayerState_Hurt));
        }
        if (player.playerIsWall() && input.AxisX != 0 && input.StopJump)
        {
            stateMachine.SwitchState(typeof(PlayerState_Wall));
        }
        if (player.playerIsAllowDash())
        {
            stateMachine.SwitchState(typeof(PlayerState_Dash));
        }
    }
    public override void PhysicUpdate()
    {
        if (!player.WallJump)
        {
            player.PlayerMove();
        }
    }
}
