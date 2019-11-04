using System.Collections.Generic;
using JunkyardDogs.Data;

public interface IExperienceModel
{
    int GetExp(Nationality nationality);
    void AddExp(Nationality nationality, int amount);
    int GetTotalExp();
}

public class Experience : IExperienceModel
{
    public Dictionary<NationalityStaticDataReference, NationalExperience> NationalExperience { get; set; }

    public Experience()
    {
        NationalExperience = new Dictionary<NationalityStaticDataReference, NationalExperience>();
    }
    
    public int GetExp(Nationality nationality)
    {
        foreach (KeyValuePair<NationalityStaticDataReference, NationalExperience> kvp in NationalExperience)
        {
            if (kvp.Key.ID == nationality.name)
            {
                return kvp.Value.Exp;
            }
        }

        return 0;
    }

    public void AddExp(Nationality nationality, int amount)
    {
        foreach (KeyValuePair<NationalityStaticDataReference, NationalExperience> kvp in NationalExperience)
        {
            if (kvp.Key.ID == nationality.name)
            {
                kvp.Value.Exp += amount;
            }
        }
    }
    
    public int GetTotalExp()
    {
        int total = 0;

        foreach (KeyValuePair<NationalityStaticDataReference, NationalExperience> kvp in NationalExperience)
        {
            total += kvp.Value.Exp;
        }

        return total;
    }
}
