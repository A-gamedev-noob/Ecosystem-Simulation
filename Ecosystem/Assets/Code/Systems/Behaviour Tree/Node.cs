using UnityEngine;

[System.Serializable]
public abstract class Node{
    protected NodeState _nodesState;
    public NodeState nodeState{get{return nodeState;}}

    public abstract NodeState Evaluate();
}

public enum NodeState{
    RUNNING, SUCCESS, FAILURE
}