using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogStateMachine : StateMachine
{
    Animator animator;
    Frog frog;
    [SerializeField] FrogState[] states;
    private void Awake()//相当于构造函数，在脚本对象被实例化时调用
    {
        animator = GetComponent<Animator>();
        frog = GetComponent<Frog>();
        stateTable = new Dictionary<System.Type, IState>(states.Length);
        foreach (FrogState state in states)
        {
            state.Init(animator, this, frog);
            stateTable.Add(state.GetType(), state);
        }
    }
    private void Start()
    {
        SwitchOn(stateTable[typeof(FrogState_Idle)]);
    }
}
