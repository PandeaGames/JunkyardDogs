using System;
using System.Collections.Generic;
using PandeaGames;
using PandeaGames.Data.Static;
using Object = UnityEngine.Object;

public abstract class ChildStaticDataReferenceDirectory<TDataBase, TData, TReferenceBase, TReference, TDirectoryBase, TDirectory>:StaticDataReferenceDirectory<TDataBase, TData, TReference, TDirectory>
    where TDataBase:Object, IStaticData
    where TData:TDataBase
    where TReference:StaticDataReference<TDataBase, TData, TReference, TDirectory>, new()
    where TReferenceBase:StaticDataReference<TDataBase, TDataBase, TReferenceBase, TDirectoryBase>, new()
    where TDirectoryBase:StaticDataReferenceDirectory<TDataBase, TDataBase, TReferenceBase, TDirectoryBase>, new()
    where TDirectory:StaticDataReferenceDirectory<TDataBase, TData, TReference, TDirectory>, new()
{
    private static IStaticDataDirectorySource<TData> cachedDirectoryForEditor;
    protected override void LoadSourceDataAsync(Action<IStaticDataDirectorySource<TData>> onLoadSuccess, LoadError onLoadFailed)
    {
        onLoadSuccess(null);
    }

    protected override void InternalLoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
    {
        onLoadSuccess();
    }

    public override TData FindData(string ID)
    {
        return Game.Instance.GetStaticDataPovider<TDirectoryBase>().FindData(ID) as TData;
    }
    
#if UNITY_EDITOR
    protected override IStaticDataDirectorySource<TData> LoadSimulatedSource()
    {
        if (cachedDirectoryForEditor != null)
        {
            return cachedDirectoryForEditor;
        }

        TDirectoryBase directoryBase = new TDirectoryBase();
        
        return null;
    }
#endif
}