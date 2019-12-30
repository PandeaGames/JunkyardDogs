using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PandeaGames.Data.Static
{
    public abstract class StaticDataReferenceDirectory<TDataBase, TData, TReference, TDirectory> : AbstractStaticDataProvider<TDirectory>, 
        IEnumerable<TReference> 
        where TDataBase:IStaticData
        where TData:TDataBase
        where TReference:StaticDataReference<TDataBase, TData, TReference, TDirectory>, new()
        where TDirectory:StaticDataReferenceDirectory<TDataBase, TData, TReference, TDirectory>, new()
    {
        private Dictionary<string, TData> _dataLookup;

        protected override void InternalLoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
        {
            LoadSourceDataAsync((source) =>
            {
                PopulateData(source, ref _dataLookup);

                onLoadSuccess();

            }, onLoadFailed);
        }

        private void PopulateData(IStaticDataDirectorySource<TData> source, ref Dictionary<string, TData> dataLookup)
        {
            dataLookup = new Dictionary<string, TData>();
            
            foreach (StaticDataEntry<TData> entry in source)
            {
                TData data = entry.Data;

                if (data != null)
                {
                    dataLookup.Add(entry.ID, entry.Data);
                }
            }
        }
        
        // Explicit for IEnumerable because weakly typed collections are Bad
        System.Collections.IEnumerator IEnumerable.GetEnumerator()
        {
            // uses the strongly typed IEnumerable<T> implementation
            return this.GetEnumerator();
        }

        // Normal implementation for IEnumerable<T>
        public IEnumerator<TReference> GetEnumerator()
        {
            foreach (KeyValuePair<string, TData> lookupEntry in _dataLookup)
            {
                TReference reference = new TReference();
                reference.ID = lookupEntry.Key;
                yield return reference;
            }
        }

        protected abstract void LoadSourceDataAsync(Action<IStaticDataDirectorySource<TData>> onLoadSuccess, LoadError onLoadFailed);

        #if UNITY_EDITOR
        protected abstract IStaticDataDirectorySource<TData> LoadSimulatedSource();
        public virtual TData FindData(string ID)
        {
            if (_dataLookup == null) 
            {
                PopulateData(LoadSimulatedSource(), ref _dataLookup);
            }
        #else
        public virtual TData FindData(string ID)
        {
        #endif
            try
            {
                TData data = _dataLookup[ID];
                return data;
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("Could not find data '{0}' in data lookup '{1}'\n{2}", ID, this, e);
            }
            
            return default(TData);
        }
    }
}