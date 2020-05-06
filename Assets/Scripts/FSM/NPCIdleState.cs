using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleState : State
{
    private float waitTime = 3f;
    private float randomTime;

    private float currentWaitTime;

    public NPCIdleState(States id)
    {
        this.id = id;
    }
    public override void OnEnter(BlackBoard blackBoard)
    {
        base.OnEnter(blackBoard);
        randomTime = Random.Range(waitTime * 0.75f, waitTime);
        currentWaitTime = randomTime;
    }
    public override void OnExit()
    {
        currentWaitTime = randomTime;
    }
    public override void OnUpdate()
    {
        if(currentWaitTime <= 0)
        {
            fsm.SwitchState(States.Wander);
        }
        else
        {
            currentWaitTime = Timer(currentWaitTime);
        }
    }
    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
