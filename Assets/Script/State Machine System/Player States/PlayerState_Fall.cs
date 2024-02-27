using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/PlayerState/Fall", fileName = "PlayerState_Fall")]
public class PlayerState_Fall : PlayerState
{
    [SerializeField] AnimationCurve speedCurve;
    public override void Enter()
    {
        animator.Play("Fall");
        stateStartTime = Time.time;
        if (player.allowJumpCount == 2)
        {
            player.allowJumpCount = 1;
        }
    }
    public override void LogicUpdate()
    {
        if (player.allowJumpCount <= 0 && input.Jump)
        {
            input.SetJumpInputBufferTimer();
        }
        if ((player.playerIsAllowJump() || player.getJumpInHead()) && !player.WallJump)
        {
            stateMachine.SwitchState(typeof(PlayerState_JumpInSky));
        }
        if (player.playerIsOnGround())
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        if (player.playerIsAllowDash())
        {
            stateMachine.SwitchState(typeof(PlayerState_Dash));
        }
        if (player.playerIsHurt())
        {
            stateMachine.SwitchState(typeof(PlayerState_Hurt));
        }
        if (player.playerIsWall())
        {
            stateMachine.SwitchState(typeof(PlayerState_Wall));
        }
    }
    public override void PhysicUpdate()
    {
        player.PlayerMove();
        player.setVelocityY(speedCurve.Evaluate(StateDuration));
    }
}
