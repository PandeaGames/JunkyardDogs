using JunkyardDogs;
using JunkyardDogs.Data;
using PandeaGames;
using PandeaGames.Views.Screens;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JunkyardScreen : ScreenView {
    
    [SerializeField]
    private CurrencyDisplay _junkCurrencyDisplay;
    
    [SerializeField, CurrencyStaticDataReference]
    private CurrencyStaticDataReference _junkCurrencyToDisplay;

    public override void Setup(WindowView window)
    {
        base.Setup(window);
        
        _junkCurrencyDisplay.DisplayCurrency(
            Game.Instance.GetViewModel<JunkyardUserViewModel>(0).UserData.Wallet.GetPair(_junkCurrencyToDisplay));
    }
}