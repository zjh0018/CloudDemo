using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/PlayerState/Run", fileName = "PlayerState_Run")]
public class PlayerState_Run : PlayerState
{
    [SerializeField] float acceration = 5f;
    public override void Enter()
    {
        animator.Play("Run");
        currentSpeed = player.MoveSpeed();
    }
    public override void LogicUpdate()
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, player.runMaxSpeed, acceration * Time.deltaTime);
        if (!input.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        if (player.playerIsAllowJump())
        {
            stateMachine.SwitchState(typeof(PlayerState_JumpOnGround));
        }
        if (player.playerIsFalling())
        {
            stateMachine.SwitchState(typeof(PlayerState_CoyoteTime));
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
        player.PlayerMove(currentSpeed);
    }

}
