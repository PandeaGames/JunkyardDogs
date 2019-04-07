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
}