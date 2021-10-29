using UnityEngine;

public class IsHungryNode : Node {

    HealthAttributes healthAttributes;
    AnimalAI AI;

    public IsHungryNode(HealthAttributes healthAttributes, AnimalAI AI){
        this.healthAttributes = healthAttributes;
        this.AI = AI;
    }

    public override NodeState Evaluate(){
        if(healthAttributes.thirst > healthAttributes.hunger && (healthAttributes.CurrentConsumeState() == ConsumeState.Wandering || healthAttributes.CurrentConsumeState() == ConsumeState.Eating) ){
            AI.IndicatorColor(Color.magenta);

            // if ((healthAttributes.CurrentConsumeState() != ConsumeState.Wandering) && (healthAttributes.CurrentConsumeState() != ConsumeState.Eating))
            //     healthAttributes.SetConsumeState(ConsumeState.Wandering);
            return NodeState.SUCCESS;
        }
        else if(healthAttributes.CurrentConsumeState() != ConsumeState.Wandering)
            healthAttributes.Full();
        return NodeState.FAILURE;
    }
}