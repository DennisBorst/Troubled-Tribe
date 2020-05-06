using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    public Dictionary<States, State> states = new Dictionary<States, State>();

    private State currentState;
    private BlackBoard blackBoard;

    public FSM(BlackBoard blackBoard, States startState, params State[] statesList)
    {
        this.blackBoard = blackBoard;

        foreach (State state in statesList)
        {
            state.Init(this);
            states.Add(state.id, state);
        }

        SwitchState(startState);
    }

    public void SwitchState(States _newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = states[_newState];

        if (currentState != null)
        {
            currentState.OnEnter(blackBoard);
        }
    }

    public void OnUpdate()
    {
        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }
}
