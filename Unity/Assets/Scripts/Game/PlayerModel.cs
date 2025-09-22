using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class PlayerModel
{
    public string playerName;
    public int metal;
    public int crystal;
    public int deuteriurm;
    public List<PlanetModel> Planets;

    public PlayerModel(string name)
    {
        this.playerName = name;
        this.metal = 500;
        this.crystal = 300;
        this.deuteriurm = 100;
    }

    public void CollectResources()
    {
        metal += 10;
        crystal += 5;
        deuteriurm += 2;

    }
}
