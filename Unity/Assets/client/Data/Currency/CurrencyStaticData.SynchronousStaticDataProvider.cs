using UnityEngine;

namespace PandeaGames
{
    public partial class SynchronousStaticDataProvider
    {
        public enum CurrencyImageTypes
        {
            SmallIcon
        }

        public Sprite GetData(CurrencyImageTypes type, CurrencyData currency)
        {
            CurrencyDataConfigTable config = staticData.CurrencyDataConfigTable;
            switch (type)
            {
                case CurrencyImageTypes.SmallIcon:
                    return config.GetConfig(currency.ID).Icon;
            }

            return null;
        }
    }
}