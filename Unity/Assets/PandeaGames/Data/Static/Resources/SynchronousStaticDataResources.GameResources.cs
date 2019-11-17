using Data;
using PandeaGames.Data.WeakReferences;
using UnityEngine;

namespace PandeaGames.Data
{
    public partial class GameResources
    {
        [SerializeField, WeakReference(typeof(SynchronousStaticData))] 
        private WeakReference _synchronousStaticData;

        public SynchronousStaticData SynchronousStaticData
        {
            get { return _synchronousStaticData.Asset as SynchronousStaticData; }
        }

        partial void PopulatedLoadGroup(LoaderGroup loadGroup)
        {
            loadGroup.AppendProvider(_synchronousStaticData);
        }
    }
}