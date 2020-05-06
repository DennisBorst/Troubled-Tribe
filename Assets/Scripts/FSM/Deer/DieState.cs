using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : State
{
    public DieState(States id)
    {
        this.id = id;
    }
    public override void OnEnter(BlackBoard blackBoard)
    {
        base.OnEnter(blackBoard);
    }
    public override void OnExit()
    {

    }
    public override void OnUpdate()
    {

    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
