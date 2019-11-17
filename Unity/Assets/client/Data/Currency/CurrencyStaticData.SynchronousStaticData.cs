using Data;
using PandeaGames.Data.WeakReferences;
using UnityEngine;

namespace PandeaGames
{
    
    public partial class SynchronousStaticData
    {
        [SerializeField] private CurrencyDataConfigTable _currencyDataConfigTable;
        public CurrencyDataConfigTable CurrencyDataConfigTable
        {
            get { return _currencyDataConfigTable; }
        }
    }
}
