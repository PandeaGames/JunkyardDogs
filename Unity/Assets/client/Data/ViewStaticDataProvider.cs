using System.Collections.Specialized;
using AssetBundles;
using PandeaGames.Data.Static;
using Data;
using UnityEngine;

public class ViewStaticDataProvider : AbstractStaticDataProvider
{
    private const string ScriptableObjectPath = "AssetBundles/Data/StaticData/ViewStaticData";

    private AssetLoader _viewAssetLoader;
    private ViewStaticData _viewStaticDataCache;
    public ViewStaticData ViewStaticData
    {
        get
        {
            if (_viewStaticDataCache == null)
            {
                _viewStaticDataCache = _viewAssetLoader.Asset as ViewStaticData;
            }

            return _viewStaticDataCache;
        }
    }

    protected override void InternalLoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
    {
        _viewAssetLoader = new AssetLoader(JunkyardAssetBundles.data.ToString(), ScriptableObjectPath);
        _viewAssetLoader.LoadAsync(onLoadSuccess, onLoadFailed);
    }
}
