using UnityEngine;
using PandeaGames.Data.WeakReferences;

namespace PandeaGames.Data
{
    [CreateAssetMenu(menuName = "PandeaGames/Data/PandeaGameConfigurationData")]
    public class PandeaGameConfigurationData : LoadableConfigurationData
    {
        [SerializeField, WeakReference(typeof(DialogConfig))] 
        private WeakReference _dialogConfigurationData;
        
        [SerializeField, WeakReference(typeof(InputConfig))] 
        private WeakReference _inputConfig;

        public DialogConfig DialogConfigurationData
        {
            get { return _dialogConfigurationData.Asset as DialogConfig; }
        }
        
        public InputConfig InputConfig
        {
            get { return _inputConfig.Asset as InputConfig; }
        }

        protected override WeakReference[] WeakReferences()
        {
            return new WeakReference[]{_dialogConfigurationData, _inputConfig};
        }
    }
}