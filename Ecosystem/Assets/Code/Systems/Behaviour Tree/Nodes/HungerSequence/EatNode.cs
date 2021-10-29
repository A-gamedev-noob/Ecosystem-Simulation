using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatNode : Node
{
    HealthAttributes healthAttributes;
    AnimalNeedsScriptable animalNeeds;

    public EatNode(HealthAttributes healthAttributes, AnimalNeedsScriptable animalNeeds){
        this.healthAttributes = healthAttributes;
        this.animalNeeds = animalNeeds;
    }

    public override NodeState Evaluate(){
        if(healthAttributes.hunger < animalNeeds.hunger){
            healthAttributes.Eat();
            Debug.Log("Eating");
            return NodeState.RUNNING; 
        }
        return NodeState.FAILURE;
    }
}
