using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node{

    protected List<Node> nodes = new List<Node>();

    public Selector(List<Node> nodes){
        this.nodes = nodes;
    }

    public override NodeState Evaluate(){

        foreach(var node in nodes){
            switch(node.Evaluate()){
                case NodeState.RUNNING : {
                    _nodesState = NodeState.RUNNING;
                    return _nodesState;
                }
                case NodeState.SUCCESS : {
                    _nodesState = NodeState.SUCCESS;
                    return _nodesState;
                }
                case NodeState.FAILURE : {
                    break;
                }
            }
        }

        _nodesState = NodeState.FAILURE;
        return _nodesState;
    }
}
