using System;
using System.Collections.Generic;
using Data;
using GoogleSheetsForUnity;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[Serializable]
public class ConfigListBalanceDataObj
{
    [NaughtyAttributes.ReadOnly]
    public string name;
}

[Serializable]
public abstract class AbstractConfigListBalanceData<TConfig>
    : BalanceData, ILoadableObject where TConfig:ConfigListBalanceDataObj, new()
{
    [SerializeField]
    private TConfig _defaultConfig;
    
    [SerializeField]
    private TConfig[] _configs;
    
    public override void ImportData(string json)
    {
        ConfigListBalanceDataObj[] dataList = JsonHelper.ArrayFromJson<ConfigListBalanceDataObj>(json);
        Debug.LogFormat("{0} Data found. Parsing.", dataList.Length);
        if (_configs == null) _configs = new TConfig[0];
        List<TConfig> newConfigList = new List<TConfig>(_configs);
        foreach (ConfigListBalanceDataObj data in dataList)
        {
            bool contains = Contains(data);
            if (!contains)
            {
                TConfig newConfig = new TConfig();
                newConfig.name = data.name;
                newConfigList.Add(newConfig);
            }
        }

        _configs = newConfigList.ToArray();
    }

    public TConfig GetConfig(string id)
    {
        foreach (TConfig config in _configs)
        {
            if (config.name == id)
            {
                return config;
            }
        }

        return _defaultConfig;
    }

    private bool Contains(ConfigListBalanceDataObj obj)
    {
        foreach (TConfig config in _configs)
        {
            if (config.name == obj.name)
            {
                return true;
            }
        }
        
        return false;
    }

    public override RowData[] GetData()
    {
        throw new NotImplementedException();
    }

    public override string[] GetFieldNames()
    {
        throw new NotImplementedException();
    }

    public override string GetUIDFieldName()
    {
        ConfigListBalanceDataObj config = new ConfigListBalanceDataObj();
        return nameof(config.name);
    }

    public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
    {
        LoaderGroup loaderGroup = new LoaderGroup();
        
        if (_defaultConfig is ILoadableObject)
        {
            loaderGroup.AppendProvider(_defaultConfig as ILoadableObject);
        }

        foreach (TConfig config in _configs)
        {
            if (config is ILoadableObject)
            {    
                loaderGroup.AppendProvider(config as ILoadableObject);
            }
        }
        
        loaderGroup.LoadAsync(() =>
        {
            IsLoaded = true;
            onLoadSuccess?.Invoke();
        }, onLoadFailed);
    }

    public bool IsLoaded { get; private set; }
}
