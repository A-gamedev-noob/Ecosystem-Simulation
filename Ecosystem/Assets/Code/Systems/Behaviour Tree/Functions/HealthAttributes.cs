using UnityEngine;
using UnityEngine.UI;

public class HealthAttributes : MonoBehaviour
{

    AnimalNeedsScriptable animalNeeds;
    NavMovement navMovement;
    public float thirst;
    public float hunger;
    public float reProdcutiveUrge;
    float timer, timerConsume;
    [SerializeField] ConsumeState currentConsumeState;
    public string colliderTag = "NULL";
    [HideInInspector]public bool isFoundWater = false;

    [Header("Debug")]
    [SerializeField] Text stateText;


    void Start(){
        animalNeeds = GetComponent<AnimalAI>().animalNeeds;
        navMovement = GetComponent<NavMovement>();
        SetAttributes();
        currentConsumeState = ConsumeState.Wandering;
    }

    // Update is called once per frame
    void Update(){
        timer += Time.deltaTime;
        Metabolism();
        
        // stateText.text = currentConsumeState.ToString();
    }
    

    public void SetAttributes(){
        thirst = animalNeeds.thirst;
        hunger = animalNeeds.hunger;
        reProdcutiveUrge = animalNeeds.reProdcutiveUrge;
    }

    void Metabolism(){
        if(timer>=1f){
            if(currentConsumeState == ConsumeState.Wandering){
                thirst = Mathf.Clamp(thirst - animalNeeds.thirstDeclineRate, 0f, 100f);
                hunger = Mathf.Clamp(hunger - animalNeeds.hungerDeclineRate, 0f, 100f);
                reProdcutiveUrge = Mathf.Clamp(reProdcutiveUrge - animalNeeds.urgeDeclineRate , 0f, 100f);
            }
            timer = 0f;
        }
    }

    public void Drink(){
        currentConsumeState = ConsumeState.Drinking;
        timerConsume += Time.deltaTime;
        if(timerConsume>=1f){
            
            thirst += animalNeeds.thirstRefilRate;
            thirst = Mathf.Clamp(thirst, 1, animalNeeds.thirst);
            timerConsume = 0f;
        }
    }

    public void Eat(){
        currentConsumeState = ConsumeState.Eating;
        timerConsume += Time.deltaTime;
        if(timerConsume>=1f){
            hunger += animalNeeds.hungerRefilRate;
            hunger  = Mathf.Clamp(hunger, 1, animalNeeds.hunger);
            timerConsume = 0;
        }
    }

    public void Full(){
        // navMovement.SetDestination(navMovement.SetDestinationByDirection(navMovement.GetDestination() - transform.position));
        // navMovement.MoveTowards(navMovement.GetDestination(), animalNeeds.speed);

        // navMovement.MovetoPreviousPoint(true);

        isFoundWater = false;
        // print("full");
        currentConsumeState = ConsumeState.Wandering;
        colliderTag = "NULL";
    }

    public ConsumeState CurrentConsumeState(){
        return currentConsumeState;
    }

    public void SetConsumeState(ConsumeState consumeState){
        currentConsumeState = consumeState;
    }

    public string GetTag(){
        return colliderTag;
    }

    private void OnTriggerEnter(Collider other) {
        colliderTag = other.tag;
    }

}
public enum ConsumeState{
    Drinking,
    Eating,
    Mateing,
    Wandering
};
