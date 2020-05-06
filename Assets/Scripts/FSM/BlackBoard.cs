using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlackBoard
{
    public Transform[] wanderPoints;
    public PlayerMovement[] playerScripts;
    public List<Transform> players;
    public NavMeshAgent navMeshAgent;

    public Deer deer;
    public NPC npc;
}
