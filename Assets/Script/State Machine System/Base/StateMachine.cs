using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected IState currentState;
    protected Dictionary<System.Type, IState> stateTable;
    // Update is called once per frame
    void Update()
    {
        currentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        currentState.PhysicUpdate();
    }
    protected void SwitchOn(IState newState)
    {
        currentState = newState;
        currentState.Enter();
    }
    public void SwitchState(IState newState)
    {
        currentState.Exit();
        SwitchOn(newState);
    }
    public void SwitchState(System.Type newStateType)
    {
        SwitchState(stateTable[newStateType]);
    }
}