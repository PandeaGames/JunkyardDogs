using System.Collections.Generic;
using JunkyardDogs.Data;
using PandeaGames.Data.Static;

public class CurrencyDataProvider : BundledStaticDataReferenceDirectory<
    CurrencyData, 
    CurrencyData, 
    CurrencyStaticDataReference, 
    CurrencyDataProvider>
{
    public const string FULL_PATH = "Assets/AssetBundles/Data/Currency/CurrencyDataSource.asset";
        
    public CurrencyDataProvider() : base("data", "CurrencyDataSource")
    {
            
    }

    public List<CurrencyData> GetCurrenciesByTag(string tag)
    {
        List<CurrencyData> data = new List<CurrencyData>();

        foreach (CurrencyData currencyData in this)
        {
            if (currencyData.Tags.Contains(tag))
            {
                data.Add(currencyData);
            }
        }

        return data;
    }
}