using UnityEngine;
using System.Collections;

//mass is how dense the material is
//volume is how much space it takes up
//weight is mass + volume and gravity shit
//V = (W/G)/D
//W = M & G
//M = D * V

public class SimulationUtils
{
    public static float CalculateMass(float density, float volume)
    {
        return density * volume;
    }

    public static float CalculateWeight(float gravity, float mass)
    {
        return mass * gravity;
    }

    public static float CalculateWeight(float gravity, float mass, float density, float volume)
    {
        return CalculateWeight(gravity, CalculateMass(density, volume));
    }
}
