using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAttributes : MonoBehaviour
{

    AnimalNeedsScriptable animalNeeds;
    public float thirst;
    public float hunger;
    public float reProdcutiveUrge;
    float timer, timerDrink;
    [SerializeField] bool consuming;

    void Start(){
        animalNeeds = GetComponent<AnimalAI>().animalNeeds;
        SetAttributes();
    }

    // Update is called once per frame
    void Update(){
        timer += Time.deltaTime;
        Metabolism();
    }
    

    public void SetAttributes(){
        thirst = animalNeeds.thirst;
        hunger = animalNeeds.hunger;
        reProdcutiveUrge = animalNeeds.reProdcutiveUrge;
    }

    void Metabolism(){
        if(timer>=1f){
            if(!consuming){
                thirst -= animalNeeds.thirstDeclineRate;
                hunger -= animalNeeds.hungerDeclineRate;
                reProdcutiveUrge -= animalNeeds.urgeDeclineRate;
            }
            timer = 0f;
        }
    }

    public void Drink(){
        consuming = true;
        timerDrink += Time.deltaTime;
        if(timerDrink>=1f){
            
            thirst += animalNeeds.thirstRefilRate;
            thirst = Mathf.Clamp(thirst, 1, animalNeeds.thirst);
            timerDrink = 0f;
        }
    }

    public void Full(){
        consuming = false;
    }

    public bool IsConsuming(){
        return consuming;
    }

}
