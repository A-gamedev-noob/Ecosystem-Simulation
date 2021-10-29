using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animal", menuName = "ScriptableObjects/ Animal", order = 0)]
public class AnimalNeedsScriptable : ScriptableObject
{
    public string animalName;
    [Header("Physical Atributes")]
    public float viewRadius = 10f;
    public float speed;
    public float stoppingDistance = 2f;
    [Header("Thirst")]
    [Range(0, 100)]
    public float thirst;
    [Range(0, 100)]
    public float thirstDeclineRate = 1.6f;
    [Range(0, 100)]
    public float thirstRefilRate = 25f;
    [Header("Hunger")]
    [Range(0, 100)]
    public float hunger;
    [Range(0, 100)]
    public float hungerDeclineRate = 1;
    [Range(0, 100)]
    public float hungerRefilRate = 100;
    [Header("Reproductive Urge")]
    [Range(0, 100)]
    public float reProdcutiveUrge;
    [Range(0, 100)]
    public float urgeDeclineRate = 1.5f;
    [Range(0, 100)]
    public float urgerRefilRate = 15f;

    public List<MonoBehaviour> animalType;
}

public enum AnimalType{
    anim,
    assd,
    asda,
    wed
}
