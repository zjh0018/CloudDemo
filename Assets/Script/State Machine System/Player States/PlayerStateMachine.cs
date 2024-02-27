using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    Animator animator;
    PlayerInput input;
    PlayerController pc;
    [SerializeField] PlayerState[] states;
    private void Awake()//�൱�ڹ��캯�����ڽű�����ʵ����ʱ����
    {
        animator = GetComponent<Animator>();
        pc = gameObject.transform.parent.GetComponent<PlayerController>();
        input = gameObject.transform.parent.GetComponent<PlayerInput>();
        stateTable = new Dictionary<System.Type, IState>(states.Length);
        foreach(PlayerState state in states)
        {
            state.Init(animator, this, pc, input);
            stateTable.Add(state.GetType(), state);
        }
    }
    private void Start()
    {
        SwitchOn(stateTable[typeof(PlayerState_Idle)]);
    }
}

