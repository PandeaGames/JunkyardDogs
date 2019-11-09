using System;
using JunkyardDogs.Data;
using PandeaGames.Utils;

[Serializable]
public class NationDictionaryKvP : PandeaGames.Utils.KeyValuePair<NationalityStaticDataReference, int>
{
    
}


[Serializable]
public class NationDictionary : DataReferenceDictionary<NationalityStaticDataReference, Nationality, Nationality, NationalityDataProvider, int, NationDictionaryKvP>
{
    
}
