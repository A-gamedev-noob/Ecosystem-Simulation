using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundFoodNode : Node
{
    AnimalAI AI;
    NavMovement navMovement;
    Transform animal;
    AnimalNeedsScriptable animalNeeds;

    public FoundFoodNode(AnimalAI AI, Transform animal, NavMovement navMovement, AnimalNeedsScriptable animalNeeds){
        this.AI = AI;
        this.animal = animal; 
        this.navMovement = navMovement;
        this.animalNeeds = animalNeeds;
    }

    public override NodeState Evaluate()
    {
        Collider[] colliders = Physics.OverlapSphere(animal.position, animalNeeds.viewRadius);
        foreach(Collider collider in colliders){
            if(collider.CompareTag("Food")){
                navMovement.SetDestination(collider.transform.position);
                return NodeState.SUCCESS;
            }
        }

        return NodeState.FAILURE;
    }
}
