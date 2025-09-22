using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlanetModel 
{
    public int id;
    public string name;
    public int metal;
    public int crystal;
    public int deuterium;

    public PlanetModel(int id, string name)
    {
        this.id = id;
        this.name = name;
        this.metal = 500;
        this.crystal = 300;
        this.deuterium = 100;
    }
}
