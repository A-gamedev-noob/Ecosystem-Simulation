using UnityEngine;

public class IsThirstyNode : Node {

    HealthAttributes healthAttributes;
    AnimalAI animalAI;

    public IsThirstyNode(HealthAttributes healthAttributes, AnimalAI animalAI){
        this.healthAttributes = healthAttributes;
        this.animalAI = animalAI;
    }


    public override NodeState Evaluate(){
        if(healthAttributes.thirst < healthAttributes.hunger && (healthAttributes.CurrentConsumeState() == ConsumeState.Wandering || healthAttributes.CurrentConsumeState() == ConsumeState.Drinking)){
            animalAI.IndicatorColor(Color.blue);
            // if((healthAttributes.CurrentConsumeState() != ConsumeState.Wandering) && (healthAttributes.CurrentConsumeState() != ConsumeState.Drinking))
            //     healthAttributes.SetConsumeState(ConsumeState.Wandering);   
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}