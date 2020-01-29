using Data;
using PandeaGames.Data.WeakReferences;
using UnityEngine;

namespace PandeaGames
{
    public partial class SynchronousStaticData
    {
        [SerializeField, WeakReference(typeof(ComponentArtConfig))] 
        private WeakReference _componentArtConfig;

        public ComponentArtConfig ComponentArtConfig
        {
            get { return _componentArtConfig.Asset as ComponentArtConfig; }
        }
    }
}