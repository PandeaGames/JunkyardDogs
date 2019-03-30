#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JunkyardDogs.Data.Balance
{
  //  [CreateAssetMenu(NEW_FILE_NAME, MENU_ROOT_PATH + "Balance Manager Data", 0)]
    public class BalanceManagerData : ScriptableObject
    {
        public const string MENU_ROOT_PATH = "Balance/";
        public const string NEW_FILE_NAME = "Balance Manager Data";
        
        [SerializeField] 
        private List<BalanceData> _balanceData;
        public List<BalanceData> BalanceData {
            get { return _balanceData; }
        }
    }
}
#endif