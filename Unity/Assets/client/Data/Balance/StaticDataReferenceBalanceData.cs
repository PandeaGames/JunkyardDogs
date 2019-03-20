#if UNITY_EDITOR
using PandeaGames.Data.Static;
using UnityEngine;

namespace JunkyardDogs.Data.Balance
{
    public abstract class StaticDataReferenceBalanceData<TStaticDataList, TUnityData> : BalanceData where TStaticDataList:AbstractScriptableObjectStaticData<TUnityData> where TUnityData:UnityEngine.Object
    {
        [SerializeField]
        protected TStaticDataList _dataList;
    }
}
#endif