using UnityEngine;

namespace PandeaGames.Views
{
    public class ContainerView : AbstractUnityView
    {
        private RectTransform _rt;
        private Transform _transform;
        private GameObject _worldObject;
        private GameObject _uiObject;
        
        public ContainerView()
        {
            _worldObject = new GameObject();
            _transform = _worldObject.GetComponent<Transform>();
            
            _uiObject = new GameObject();
            _rt = _uiObject.AddComponent<RectTransform>();
        }

        public override Transform GetTransform()
        {
            return _transform;
        }

        public override RectTransform GetRectTransform()
        {
            return _rt;
        }
    }
}