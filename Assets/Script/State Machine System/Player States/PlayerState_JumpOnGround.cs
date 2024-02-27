using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/PlayerState/JumpOnGround", fileName = "PlayerState_JumpOnGround")]
public class PlayerState_JumpOnGround : PlayerState_Jump
{
    public override void Enter()
    {
        animator.Play("JumpOnGround");
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
