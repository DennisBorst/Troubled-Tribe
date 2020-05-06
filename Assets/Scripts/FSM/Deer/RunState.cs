using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    private float distanceFromDestination = 2f;
    private float distanceToLocation;
    private Transform targetPoint;

    public RunState(States id)
    {
        this.id = id;
    }
    public override void OnEnter(BlackBoard blackBoard)
    {
        base.OnEnter(blackBoard);
        blackBoard.navMeshAgent.speed = blackBoard.deer.runSpeed;
        blackBoard.deer.SetAnimation("isRunning", true);
        GetRandomWanderPoint();
    }
    public override void OnExit()
    {
        Debug.Log("Exit state");
        blackBoard.deer.SetAnimation("isRunning", false);
        blackBoard.navMeshAgent.destination = blackBoard.deer.transform.position;
    }
    public override void OnUpdate()
    {
        //blackBoard.deer.SetAnimation("isWalking", true);
        distanceToLocation = Mathf.Abs(Vector3.Distance(blackBoard.deer.transform.position, targetPoint.transform.position));

        if (distanceFromDestination >= distanceToLocation)
        {
            fsm.SwitchState(States.Idle);
        }
        else
        {
            blackBoard.navMeshAgent.destination = targetPoint.transform.position;
        }
    }
    private Vector3 GetRandomWanderPoint()
    {
        System.Random random = new System.Random();
        int wanderNumberPoint = random.Next(0, blackBoard.wanderPoints.Length);
        targetPoint = blackBoard.wanderPoints[wanderNumberPoint];
        return targetPoint.transform.position;
    }
}
