using System;
using PandeaGames.Data.Static;
using Data;
using PandeaGames;

namespace JunkyardDogs.Data
{
    public class GameStaticDataProvider : AbstractStaticDataProvider<GameStaticDataProvider>
    {
        private const string ScriptableObjectPath = "AssetBundles/Data/StaticData/GameStaticData";

        private AssetLoader _gameDataLoader;
        private GameStaticData _gameDataLoaderCache;
        public GameStaticData GameDataStaticData
        {
            get
            {
                if (_gameDataLoaderCache == null)
                {
                    _gameDataLoaderCache = _gameDataLoader.Asset as GameStaticData;
                }

                return _gameDataLoaderCache;
            }
        }

        public RarityArtConfig GetRarityArtConfig(int rarity)
        {
            return GameDataStaticData.RarityArtConfig[Math.Min(GameDataStaticData.RarityArtConfig.Length - 1, rarity)];
        }
        
        protected override void InternalLoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
        {
            _gameDataLoader = new AssetLoader(JunkyardAssetBundles.data.ToString(), ScriptableObjectPath);
            _gameDataLoader.LoadAsync(onLoadSuccess, onLoadFailed);
        }
    }
}