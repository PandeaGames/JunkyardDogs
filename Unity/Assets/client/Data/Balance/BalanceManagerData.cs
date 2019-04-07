#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JunkyardDogs.Data.Balance
{
    [CreateAssetMenu(menuName = MENU_NAME)]
    public class BalanceManagerData : ScriptableObject
    {
        private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "Balance Manager Data";
        
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