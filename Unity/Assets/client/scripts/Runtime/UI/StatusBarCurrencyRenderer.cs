using System.Collections.Generic;
using JunkyardDogs.Data;
using PandeaGames;
using UnityEngine;

public class StatusBarCurrencyRenderer : MonoBehaviour
{
    [SerializeField] 
    private CurrencyDisplay _currencyDisplay;

    [SerializeField]
    private Transform _currencyContainer;

    private void Start()
    {
        string tag = GameStaticDataProvider.Instance.GameDataStaticData.StatusBarCurrencyTag;
        List<CurrencyData> currencies = CurrencyDataProvider.Instance.GetCurrenciesByTag(tag);

        foreach (CurrencyData currency in currencies)
        {
            GameObject currencyDisplayObj =
                Instantiate(_currencyDisplay.gameObject, _currencyContainer, worldPositionStays:false);
            currencyDisplayObj.SetActive(true);
            CurrencyDisplay currencyDisplay = currencyDisplayObj.GetComponent<CurrencyDisplay>();
            currencyDisplay.DisplayCurrency(JunkyardUserService.Instance.User.Wallet.GetPair(currency));
        }
    }
}
