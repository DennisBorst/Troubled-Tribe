using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class State
{
    //public
    public FSM fsm;
    public States id;

    //protected
    protected BlackBoard blackBoard;

    public void Init(FSM _owner)
    {
        fsm = _owner;
    }

    public virtual void OnEnter(BlackBoard blackBoard)
    {
        this.blackBoard = blackBoard;
    }
    public abstract void OnUpdate();
    public abstract void OnExit();
}

