using UnityEngine;

public class IsHungryNode : Node {

    HealthAttributes healthAttributes;

    public IsHungryNode(HealthAttributes healthAttributes){
        this.healthAttributes = healthAttributes;
    }

    public override NodeState Evaluate(){
        if(healthAttributes.thirst < healthAttributes.hunger)
            return NodeState.SUCCESS;

        healthAttributes.Full();
        return NodeState.FAILURE;
    }
}