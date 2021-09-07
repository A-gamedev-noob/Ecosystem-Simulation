using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimalDatabase{
    List<Animal> Animals  = new List<Animal>();
}

public class Animal{
    public string name;
}
