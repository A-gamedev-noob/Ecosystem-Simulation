using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MateNode : Node
{
    HealthAttributes healthAttributes;
    AnimalNeedsScriptable animalNeeds;

    public MateNode(HealthAttributes healthAttributes, AnimalNeedsScriptable animalNeeds){
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
