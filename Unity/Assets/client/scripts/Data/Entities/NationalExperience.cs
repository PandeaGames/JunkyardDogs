using System;
using JunkyardDogs.Components.Gameplay;

[Serializable]
public class NationalExperience : ExpLevel
{
    public NationalExperience()
    {
        
    }
    
    public NationalExperience(int exp) : base(exp)
    {
        
    }
    
    public static implicit operator NationalExperience(int exp)
    {
        return new NationalExperience(exp);
    }
}
