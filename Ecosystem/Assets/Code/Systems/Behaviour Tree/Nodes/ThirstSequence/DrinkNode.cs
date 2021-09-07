using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DrinkNode : Node
{
    HealthAttributes healthAttributes;
    AnimalNeedsScriptable animalNeeds;
    NavMeshAgent navAgent;

    public DrinkNode(HealthAttributes healthAttributes, AnimalNeedsScriptable animalNeeds, NavMeshAgent navAgent){
        this.healthAttributes = healthAttributes;
        this.animalNeeds = animalNeeds;
        this.navAgent = navAgent;
    }

    public override NodeState Evaluate(){
        if(healthAttributes.thirst < animalNeeds.thirst){
            healthAttributes.Drink();
            navAgent.autoBraking = true;
            return NodeState.RUNNING; 
        }

        healthAttributes.Full();
        Debug.Log("f");
        return NodeState.FAILURE;
    }
}
