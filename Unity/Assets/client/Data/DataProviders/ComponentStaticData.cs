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
        
        [SerializeField, WeakReference(typeof(ComponentArtConfig))] 
        private WeakReference _meleeImpactConfg;

        public ComponentArtConfig ComponentArtConfig
        {
            get { return _componentArtConfig.Asset as ComponentArtConfig; }
        }
        
        public ComponentArtConfig HitscanStreamArtConfig
        {
            get { return _hitscanStreamArtConfig.Asset as ComponentArtConfig; }
        }
        
        public ComponentArtConfig ProjectileArtConfig
        {
            get { return _projectileArtConfig.Asset as ComponentArtConfig; }
        }
        
        public ComponentArtConfig ProjectileImpactConfig
        {
            get { return _projectileImpactConfig.Asset as ComponentArtConfig; }
        }
        
        public ComponentArtConfig PulseArtConfig
        {
            get { return _pulseArtConfig.Asset as ComponentArtConfig; }
        }
        
        public ComponentArtConfig PulseImpactConfg
        {
            get { return _pulseImpactConfg.Asset as ComponentArtConfig; }
        }
        
        public ComponentArtConfig MeleeImpactConfg
        {
            get { return _meleeImpactConfg.Asset as ComponentArtConfig; }
        }
    }
}