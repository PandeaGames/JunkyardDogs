using UnityEngine;

namespace PandeaGames.Data
{
    [CreateAssetMenu(fileName = "InputConfig", menuName = "PandeaGaames/Config/InputConfig", order = 1)]
    public class InputConfig : ScriptableObject
    {
        [SerializeField]
        private bool _touchEnabled;
        [SerializeField]
        private bool _providePonterRaycast;
        [SerializeField]
        private bool _useTriggersInRaycast;
        [SerializeField]
        private int _maxRaycastResults = 1;

        public bool TouchEnabled
        {
            get { return _touchEnabled; }
        }
        
        public bool ProvidePonterRaycast
        {
            get { return _providePonterRaycast; }
        }
        
        public bool UseTriggersInRaycast
        {
            get { return _useTriggersInRaycast; }
        }
        
        public int MaxRaycastResults
        {
            get { return _maxRaycastResults; }
        }
    }
}