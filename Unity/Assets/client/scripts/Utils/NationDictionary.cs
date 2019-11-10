using System;
using JunkyardDogs.Data;
using PandeaGames.Utils;

[Serializable]
public class NationDictionaryKvP : PandeaGames.Utils.KeyValuePair<NationalityStaticDataReference, NationalExperience>
{
    
}


[Serializable]
public class NationDictionary : 
    DataReferenceDictionary<
        NationalityStaticDataReference, 
        Nationality, 
        Nationality, 
        NationalityDataProvider, 
        NationalExperience, 
        NationDictionaryKvP>
{
    
}
