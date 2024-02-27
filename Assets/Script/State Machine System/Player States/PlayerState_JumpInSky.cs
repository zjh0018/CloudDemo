using UnityEngine;
[CreateAssetMenu(menuName = "StateMachine/PlayerState/JumpInSky", fileName = "PlayerState_JumpInSky")]

public class PlayerState_JumpInSky : PlayerState_Jump
{
    public override void Enter()
    {
        base.Enter();
        animator.Play("JumpInSky");
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
