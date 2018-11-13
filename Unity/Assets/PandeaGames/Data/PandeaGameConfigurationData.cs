using Data;
using UnityEngine;

namespace PandeaGames.Data
{
    [CreateAssetMenu(menuName = "PandeaGames/Data/PandeaGameConfigurationData")]
    public class PandeaGameConfigurationData : LoadableConfigurationData
    {
        [SerializeField, WeakReference(typeof(DialogConfig))] 
        private WeakReference _dialogConfigurationData;

        public DialogConfig DialogConfigurationData
        {
            get { return _dialogConfigurationData.Asset as DialogConfig; }
        }

        protected override WeakReference[] WeakReferences()
        {
            return new WeakReference[]{_dialogConfigurationData};
        }
    }
}