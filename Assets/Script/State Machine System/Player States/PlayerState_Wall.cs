using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/PlayerState/Wall", fileName = "PlayerState_Wall")]
public class PlayerState_Wall : PlayerState
{
    public override void Enter()
    {
        animator.Play("Wall");
        player.allowJumpCount = 1;
    }
    public override void LogicUpdate()
    {
        if (player.playerIsAllowJump())
        {
            player.WallJump = true;
            stateMachine.SwitchState(typeof(PlayerState_JumpOnWall));
        }
        if (!player.playerIsWall())
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
    }
    public override void PhysicUpdate()
    {
        player.setVelocity(new Vector2(0, -3f));
    }
    public override void Exit()
    {
        player.allowJumpCount--;
    }
}
