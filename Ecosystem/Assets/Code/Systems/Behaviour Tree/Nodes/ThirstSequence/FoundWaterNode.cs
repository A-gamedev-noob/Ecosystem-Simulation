using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundWaterNode : Node
{
    AnimalAI AI;
    NavMovement navMovement;
    Transform creature;
    AnimalNeedsScriptable animalNeeds;
    HealthAttributes healthAttributes;

    public FoundWaterNode(AnimalAI AI, Transform creature, NavMovement navMovement, HealthAttributes healthAttributes, AnimalNeedsScriptable animalNeeds){
        this.AI = AI;
        this.creature = creature; 
        this.navMovement = navMovement;
        this.animalNeeds = animalNeeds;
        this.healthAttributes = healthAttributes;
    }

    public override NodeState Evaluate()
    {
        Collider[] colliders = Physics.OverlapSphere(creature.position, animalNeeds.viewRadius);
        foreach(Collider collider in colliders){
            if(collider.CompareTag("Water")){
                if(!healthAttributes.isFoundWater){
                    navMovement.SetDestination(collider.transform.position);
                    healthAttributes.isFoundWater = true; 
                }
                return NodeState.SUCCESS;
            }
        }

        return NodeState.FAILURE;
    }
    
}
