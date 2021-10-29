using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DrinkNode : Node
{
    HealthAttributes healthAttributes;
    AnimalNeedsScriptable animalNeeds;
    NavMovement navAgent;

    public DrinkNode(HealthAttributes healthAttributes, AnimalNeedsScriptable animalNeeds, NavMovement navAgent){
        this.healthAttributes = healthAttributes;
        this.animalNeeds = animalNeeds;
        this.navAgent = navAgent;
    }

    public override NodeState Evaluate(){
        if(healthAttributes.thirst < animalNeeds.thirst){
            healthAttributes.Drink();
            return NodeState.RUNNING; 
        }

        navAgent.MovetoPreviousPoint(true);
        healthAttributes.Full();
        return NodeState.FAILURE;
    }
}
