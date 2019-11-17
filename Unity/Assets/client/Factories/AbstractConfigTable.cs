using System.Collections.Generic;
using UnityEngine;

namespace JunkyardDogs.Factories
{
    public interface IConfig
    {
        string ID { get; }
    }
    
    public abstract class AbstractConfigTable<TConfig> : ScriptableObject where TConfig:IConfig
    {
        [SerializeField]
        private List<TConfig> _configs;

        public TConfig GetConfig(string ID)
        {
            foreach (TConfig config in _configs)
            {
                if (config.ID.Equals(ID))
                {
                    return config;
                }
            }

            return default(TConfig);
;        }
    }
}