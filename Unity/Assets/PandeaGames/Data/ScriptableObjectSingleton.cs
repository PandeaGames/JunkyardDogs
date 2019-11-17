using System;
using UnityEngine;

namespace PandeaGames.Data
{
    public abstract class ScriptableObjectSingleton<TSingleton> : ScriptableObject where TSingleton : ScriptableObjectSingleton<TSingleton>
    {
        private static TSingleton _instance;
        public static TSingleton Instance
        {
            get
            {
                if(_instance == null)
                {
                    string className = typeof(TSingleton).Name;
                    string path = string.Format("PandeaGames/{0}", className);
                    _instance = Resources.Load<TSingleton>(path);

                    if (_instance == null)
                    {
                        throw new Exception(string.Format("Unable to load Singleton from Resources of type '{0}' at '{1}''", typeof(TSingleton).FullName, path));
                    }
                }

                return _instance;
            }
        }
    }
}