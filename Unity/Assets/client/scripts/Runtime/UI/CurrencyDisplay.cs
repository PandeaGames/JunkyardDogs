using System;
using PandeaGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _quantityText;
    
    [SerializeField]
    private Image _currencyImage;

    [SerializeField] private SynchronousStaticDataProvider.CurrencyImageTypes _imageType;

    private CurrencyDictionaryKvP _currency;
    
    public void DisplayCurrency(CurrencyDictionaryKvP currency)
    {
        _currency = currency;
        _currencyImage.sprite = SynchronousStaticDataProvider.Instance.GetData(_imageType, _currency.Key);
    }

    private void Update()
    {
        if (_currency != null)
        {
            _quantityText.text = _currency.Value.ToString();
        }
    }
}
