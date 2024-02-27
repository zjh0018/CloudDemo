using UnityEngine;
[CreateAssetMenu(menuName = "StateMachine/PlayerState/JumpOnWall", fileName = "PlayerState_JumpOnWall")]
public class PlayerState_JumpOnWall : PlayerState_Jump
{
    public float wallJumpTime;
    public override void Enter()
    {
        wallJumpTime = 0.4f;
        animator.Play("JumpOnWall");
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public override void PhysicUpdate()
    {
        wallJumpTime -= Time.fixedDeltaTime;
        if (wallJumpTime <= 0)
        {
            player.WallJump = false;
        }
        base.PhysicUpdate();
    }
    public override void Exit()
    {
        player.WallJump = false;
        base.Exit();
    }
}
