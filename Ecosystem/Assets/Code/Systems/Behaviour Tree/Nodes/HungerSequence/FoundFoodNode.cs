using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundFoodNode : Node
{
    AnimalAI AI;
    NavMovement navMovement;
    Transform animal;
    AnimalNeedsScriptable animalNeeds;

    Vector3 nearestFood;

    public FoundFoodNode(AnimalAI AI, Transform animal, NavMovement navMovement, AnimalNeedsScriptable animalNeeds){
        this.AI = AI;
        this.animal = animal; 
        this.navMovement = navMovement;
        this.animalNeeds = animalNeeds;
    }

    public override NodeState Evaluate()
    {
        Collider[] colliders = Physics.OverlapSphere(animal.position, animalNeeds.viewRadius);
            if(FindNearestFood(colliders)){
                navMovement.SetDestination(nearestFood);
                return NodeState.SUCCESS;
            }

        return NodeState.FAILURE;
    }

    bool FindNearestFood(Collider[] colliders){
        if(colliders.Length <= 0)
            return false;
        List<Vector3> foodPos = new List<Vector3>();
        // bool foodAvalaible = false;
        foreach(Collider col in colliders){
            if(col.CompareTag("Food")){
                foodPos.Add(col.transform.position);
            }
        }
        if(foodPos.Capacity <=0)
            return false;
        nearestFood = foodPos[0];
        foreach(Vector3 pos in foodPos){
            if(Vector3.Distance(nearestFood, animal.position) > Vector3.Distance(pos, animal.position))
                nearestFood = pos;
        }

        return true;

    }
}
