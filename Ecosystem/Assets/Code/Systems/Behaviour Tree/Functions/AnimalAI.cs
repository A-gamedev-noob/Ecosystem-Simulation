using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour{
    
    NavMovement navMovement;
    NavMeshAgent navAgent;
    HealthAttributes healthAttributes;
    [SerializeField] MeshRenderer indicator;

    public AnimalNeedsScriptable animalNeeds;

    Node topNode;

    private void Awake() {
        navMovement = GetComponent<NavMovement>();
        navAgent = GetComponent<NavMeshAgent>();
        healthAttributes = GetComponent<HealthAttributes>();
    }

    void Start(){
        ConstructTree();
    }

    void ConstructTree(){

        FoundWaterNode foundWaterNode = new FoundWaterNode(this, this.transform, navMovement, animalNeeds);
        MoveTowardsNode moveTowardsNode = new MoveTowardsNode(this, navMovement, navAgent, animalNeeds);
        DrinkNode drinkNode = new DrinkNode(healthAttributes, animalNeeds, navAgent);
        IsThirstyNode isThirstyNode = new IsThirstyNode(healthAttributes, this);
        SetDestinationNode setDestinationNode = new SetDestinationNode(navMovement, animalNeeds);

        Selector consumeSelector = new Selector(new List<Node> {moveTowardsNode, drinkNode});

        Sequence findSourceSequence = new Sequence(new List<Node> {foundWaterNode, consumeSelector});
        Sequence thirstSequence = new Sequence(new List<Node> {isThirstyNode, findSourceSequence});
        Sequence wanderSequence = new Sequence(new List<Node> {setDestinationNode, moveTowardsNode});

        topNode = new Selector(new List<Node> {thirstSequence, wanderSequence}); 

        // IndicatorColor(Color.black);

    }

    // Update is called once per frame
    void Update()
    {
        // topNode.Evaluate();
    }

    public void IndicatorColor(Color color){
        indicator.material.color = color;
    }

    public NavMeshAgent GetNavMeshAgent(){
        return navAgent;
    }
}
