using System;
using JunkyardDogs.Data;
using JunkyardDogs.Factories;
using UnityEngine;


    [Serializable]
    public class CurrencyDataConfig : IConfig
    {
        [SerializeField, CurrencyStaticDataReference]
        private CurrencyStaticDataReference _currencyDataReference;

        [SerializeField]
        private Sprite _icon;
        public Sprite Icon
        {
            get { return _icon; }
        }

        public string ID
        {
            get { return _currencyDataReference.ID; }
        }
    }
    
    [Serializable, CreateAssetMenu(menuName =  "CurrencyDataConfigTable")]
    public class CurrencyDataConfigTable : AbstractConfigTable<CurrencyDataConfig>
    {
        
    }
