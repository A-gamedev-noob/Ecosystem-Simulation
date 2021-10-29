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

        ///////////////Thirst Branch//////////////////
        
        DrinkNode drinkNode = new DrinkNode(healthAttributes, animalNeeds, navMovement);
        MoveTowardsNode moveTowardsWaterNode = new MoveTowardsNode(this, navMovement, navAgent, animalNeeds, healthAttributes, InteractionType.Water);
        Selector consumeWaterSelector = new Selector(new List<Node> {moveTowardsWaterNode, drinkNode});
        FoundWaterNode foundWaterNode = new FoundWaterNode(this, this.transform, navMovement, healthAttributes, animalNeeds);
        Sequence findWaterSequence = new Sequence(new List<Node> {foundWaterNode, consumeWaterSelector});
        IsThirstyNode isThirstyNode = new IsThirstyNode(healthAttributes, this);
        Sequence thirstSequence = new Sequence(new List<Node> {isThirstyNode, findWaterSequence});

        ///////////////Hunger Branch//////////////////

        EatNode eatNode = new EatNode(healthAttributes, animalNeeds);
        MoveTowardsNode moveTowardsFoodNode = new MoveTowardsNode(this, navMovement, navAgent, animalNeeds, healthAttributes, InteractionType.Food);
        Selector consumeFoodSelector = new Selector( new List<Node> {moveTowardsFoodNode, eatNode});
        FoundFoodNode foundFoodNode = new FoundFoodNode(this, this.transform, navMovement, animalNeeds);
        Sequence findFoodSequence = new Sequence(new List<Node> {foundFoodNode, consumeFoodSelector});
        IsHungryNode isHungryNode = new IsHungryNode(healthAttributes, this);
        Sequence hungerSequence = new Sequence(new List<Node> {isHungryNode, findFoodSequence});
        
        ///////////////Wander Branch//////////////////

        MoveTowardsNode moveTowardsWanderNode = new MoveTowardsNode(this, navMovement, navAgent, animalNeeds, healthAttributes, InteractionType.Wander);
        SetDestinationNode setDestinationNode = new SetDestinationNode(navMovement, animalNeeds);
        Sequence wanderSequence = new Sequence(new List<Node> {setDestinationNode, moveTowardsWanderNode});




        topNode = new Selector(new List<Node> {thirstSequence, hungerSequence, wanderSequence}); 

        // IndicatorColor(Color.black);

    }

    // Update is called once per frame
    void Update()
    {
        topNode.Evaluate();
    }


    public void IndicatorColor(Color color){
        indicator.material.color = color;
    }

    public NavMeshAgent GetNavMeshAgent(){
        return navAgent;
    }
}
