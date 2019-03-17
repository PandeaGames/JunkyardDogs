using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PandeaGames.Data.Static
{
    public abstract class AbstractScriptableObjectStaticData<TData> : ScriptableObject, IStaticDataDirectorySource<TData> where TData:Object
    {
        public List<TData> Data;

        public string[] GetIDs()
        {
            List<string> ids = new List<string>();
            
            foreach (TData data in Data)
            {
                ids.Add(data.name);
            }

            return ids.ToArray();
        }
        
        // Explicit for IEnumerable because weakly typed collections are Bad
        System.Collections.IEnumerator IEnumerable.GetEnumerator()
        {
            // uses the strongly typed IEnumerable<T> implementation
            return this.GetEnumerator();
        }

        // Normal implementation for IEnumerable<T>
        public IEnumerator<StaticDataEntry<TData>> GetEnumerator()
        {
            foreach (TData data in Data)
            {
                yield return new StaticDataEntry<TData>(data, data.name);
            }
        }
    }
}