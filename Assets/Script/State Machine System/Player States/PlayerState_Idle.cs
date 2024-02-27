using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/PlayerState/Idle", fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayerState
{
    [SerializeField] float acceration = 2000f;
    public override void Enter()
    {
        animator.Play("Idle");
        player.resetExtraJump();
        currentSpeed = player.MoveSpeed();
    }
    public override void LogicUpdate()
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, acceration * Time.deltaTime);
        if (input.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Run));
        }
        if (player.playerIsAllowJump())
        {
            stateMachine.SwitchState(typeof(PlayerState_JumpOnGround));
        }
        if (player.playerIsFalling())
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
        if (player.playerIsAllowDash())
        {
            stateMachine.SwitchState(typeof(PlayerState_Dash));
        }
        if (player.playerIsHurt())
        {
            stateMachine.SwitchState(typeof(PlayerState_Hurt));
        }
    }
    public override void PhysicUpdate()
    {
        player.setVelocityX(currentSpeed * player.transform.localScale.x);
    }
}
