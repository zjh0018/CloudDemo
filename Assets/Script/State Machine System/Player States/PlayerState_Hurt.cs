using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/PlayerState/Hurt", fileName = "PlayerState_Hurt")]
public class PlayerState_Hurt : PlayerState
{
    [SerializeField] AnimationCurve speedCurve;
    public override void Enter()
    {
        player.setVelocity(new Vector2(0, 0));
        animator.Play("Hurt");
        stateStartTime = Time.time;
    }
    public override void LogicUpdate()
    {
        if (!player.playerIsHurt())
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
    }
    public override void PhysicUpdate()
    {
        player.setVelocityY(speedCurve.Evaluate(StateDuration));
    }
}