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
    InteractionType interactionType;
    HealthAttributes healthAttributes;

    public MoveTowardsNode(AnimalAI AI, NavMovement navMovement, NavMeshAgent navMeshAgent, AnimalNeedsScriptable animalNeeds, HealthAttributes healthAttributes, InteractionType interactionType){
        this.AI = AI;
        this.navMovement = navMovement;
        this.navMeshAgent = navMeshAgent;
        this.animalNeeds = animalNeeds;
        this.interactionType = interactionType;
        this.healthAttributes = healthAttributes;
    }

    public override NodeState Evaluate(){

        switch (interactionType){
            case InteractionType.Water :{
                return Water();
            }
            case InteractionType.Food :{
                return Food();
            }
            case InteractionType.Wander:{
                return Wander();
            }

        }

        return NodeState.RUNNING;
    }

    NodeState Water(){
        if (healthAttributes.GetTag() == "Water"){
            navMeshAgent.isStopped = true;
            return NodeState.FAILURE;
        }
        navMovement.MoveTowards();
        return NodeState.RUNNING;
    }

    NodeState Food(){
        Vector3 destination = navMovement.GetDestination();
        if (Vector3.Distance(navMovement.transform.position, destination) <= animalNeeds.stoppingDistance){
            navMeshAgent.isStopped = true;
            return NodeState.FAILURE;
        }
        navMovement.MoveTowards();

        return NodeState.RUNNING;
    }

    NodeState Mate(){
        return NodeState.RUNNING;
    }

    NodeState Wander(){
        Vector3 destination = navMovement.GetDestination();
        if (Vector3.Distance(navMovement.transform.position, destination) <= animalNeeds.stoppingDistance){
            navMeshAgent.isStopped = true;
            return NodeState.FAILURE;
        }
        navMovement.MoveTowards();

        return NodeState.RUNNING;
    }

}
