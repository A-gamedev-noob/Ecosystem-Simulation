using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundFoodNode : Node
{
    AnimalAI AI;
    NavMovement navMovement;
    Transform creature;
    AnimalNeedsScriptable animalNeeds;

    public FoundFoodNode(AnimalAI AI, Transform creature, NavMovement navMovement, AnimalNeedsScriptable animalNeeds){
        this.AI = AI;
        this.creature = creature; 
        this.navMovement = navMovement;
        this.animalNeeds = animalNeeds;
    }

    public override NodeState Evaluate()
    {
        Collider[] colliders = Physics.OverlapSphere(creature.position, animalNeeds.viewRadius);
        foreach(Collider collider in colliders){
            if(collider.CompareTag("Food")){
                navMovement.SetDestination(collider.transform.position);
                return NodeState.SUCCESS;
            }
        }

        return NodeState.FAILURE;
    }
}
