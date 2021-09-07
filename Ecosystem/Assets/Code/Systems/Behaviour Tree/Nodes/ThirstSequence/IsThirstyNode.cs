using UnityEngine;

public class IsThirstyNode : Node {

    HealthAttributes healthAttributes;
    AnimalAI animalAI;

    public IsThirstyNode(HealthAttributes healthAttributes, AnimalAI animalAI){
        this.healthAttributes = healthAttributes;
        this.animalAI = animalAI;
    }

    public override NodeState Evaluate(){
        if(healthAttributes.thirst < healthAttributes.hunger || healthAttributes.IsConsuming()){
            animalAI.IndicatorColor(Color.red);
            return NodeState.SUCCESS;
        }
        // healthAttributes.Full();
        return NodeState.FAILURE;
    }
}