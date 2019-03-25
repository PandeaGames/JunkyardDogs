#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace JunkyardDogs.Data.Balance
{
    [CreateAssetMenu]
    public class BalanceManagerData : ScriptableObject
    {
        [SerializeField] 
        private List<BalanceData> _balanceData;
        public List<BalanceData> BalanceData {
            get { return _balanceData; }
        }
    }
}
#endif