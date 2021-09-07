using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTowardsNode : Node
{
    AnimalAI AI;
    NavMovement navMovement;
    NavMeshAgent navMeshAgent;
    AnimalNeedsScriptable animalNeeds;

    public MoveTowardsNode(AnimalAI AI, NavMovement navMovement, NavMeshAgent navMeshAgent, AnimalNeedsScriptable animalNeeds){
        this.AI = AI;
        this.navMovement = navMovement;
        this.navMeshAgent = navMeshAgent;
        this.animalNeeds = animalNeeds;
    }

    public override NodeState Evaluate(){
        Vector3 destination = navMovement.GetDestination();
        if(Vector3.Distance(navMovement.transform.position, destination) <= animalNeeds.stoppingDistance){
            navMeshAgent.isStopped = true;
            return NodeState.FAILURE;
        }
        navMovement.MoveTowards(navMovement.GetDestination(), animalNeeds.speed);

        return NodeState.RUNNING;
    }
}
