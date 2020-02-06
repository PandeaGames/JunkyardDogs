using Data;
using PandeaGames.Data.WeakReferences;
using UnityEngine;

namespace PandeaGames
{
    public partial class SynchronousStaticData
    {
        [SerializeField, WeakReference(typeof(ComponentArtConfig))] 
        private WeakReference _componentArtConfig;
        
        [SerializeField, WeakReference(typeof(ComponentArtConfig))] 
        private WeakReference _hitscanStreamArtConfig;
        
        [SerializeField, WeakReference(typeof(ComponentArtConfig))] 
        private WeakReference _projectileArtConfig;
        
        [SerializeField, WeakReference(typeof(ComponentArtConfig))] 
        private WeakReference _projectileImpactConfig;
        
        [SerializeField, WeakReference(typeof(ComponentArtConfig))] 
        private WeakReference _pulseArtConfig;
        
        [SerializeField, WeakReference(typeof(ComponentArtConfig))] 
        private WeakReference _pulseImpactConfg;

        public ComponentArtConfig ComponentArtConfig
        {
            get { return _componentArtConfig.Asset as ComponentArtConfig; }
        }
    }
}