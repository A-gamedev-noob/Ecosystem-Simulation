using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundMateNode : Node
{
    HealthAttributes healthAttributes;
    AnimalNeedsScriptable animalNeeds;

    public FoundMateNode(HealthAttributes healthAttributes, AnimalNeedsScriptable animalNeeds){
        this.healthAttributes = healthAttributes;
        this.animalNeeds = animalNeeds;
    }

    public override NodeState Evaluate(){
        if(healthAttributes.thirst < animalNeeds.thirst){
            healthAttributes.Drink();
            return NodeState.RUNNING; 
        }
        return NodeState.FAILURE;
    }
}
