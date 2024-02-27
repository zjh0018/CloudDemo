using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogStateMachine : StateMachine
{
    Animator animator;
    Frog frog;
    [SerializeField] FrogState[] states;
    private void Awake()//�൱�ڹ��캯�����ڽű�����ʵ����ʱ����
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
