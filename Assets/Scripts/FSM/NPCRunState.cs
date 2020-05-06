using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRunState : State
{
    private float distanceFromDestination = 2f;
    private float distanceToLocation;

    private float waitTime = 3f;
    private float currentWaitTime;
    private Transform targetPoint;

    public NPCRunState(States id)
    {
        this.id = id;
    }
    public override void OnEnter(BlackBoard blackBoard)
    {
        base.OnEnter(blackBoard);
        currentWaitTime = waitTime;
        blackBoard.npc.SetAnimation("isRunning", true);
        blackBoard.navMeshAgent.speed = blackBoard.npc.runSpeed;
        GetRandomWanderPoint();
    }
    public override void OnExit()
    {
        currentWaitTime = waitTime;
        blackBoard.npc.SetAnimation("isRunning", false);
        blackBoard.navMeshAgent.destination = blackBoard.npc.transform.position;
    }
    public override void OnUpdate()
    {
        distanceToLocation = Mathf.Abs(Vector3.Distance(blackBoard.npc.transform.position, targetPoint.transform.position));

        if (distanceFromDestination >= distanceToLocation)
        {
            GetRandomWanderPoint();
        }
        else
        {
            blackBoard.navMeshAgent.destination = targetPoint.transform.position;
        }

        if (currentWaitTime <= 0)
        {
            fsm.SwitchState(States.Wander);
        }
        else
        {
            currentWaitTime = Timer(currentWaitTime);
        }
    }
    private Vector3 GetRandomWanderPoint()
    {
        System.Random random = new System.Random();
        int wanderNumberPoint = random.Next(0, blackBoard.wanderPoints.Length);
        targetPoint = blackBoard.wanderPoints[wanderNumberPoint];
        return targetPoint.transform.position;
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
