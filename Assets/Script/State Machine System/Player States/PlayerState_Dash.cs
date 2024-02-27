using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/PlayerState/Dash", fileName = "PlayerState_Dash")]
public class PlayerState_Dash : PlayerState
{
    public override void Enter()
    {
        animator.Play("Dash");
        player.ReadyToDash();
    }
    public override void LogicUpdate()
    {
        if (!player.playerIsDashing())
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        if (player.playerIsHurt())
        {
            stateMachine.SwitchState(typeof(PlayerState_Hurt));
        }
    }
    public override void PhysicUpdate()
    {
        player.PlayerDash();
    }
}
