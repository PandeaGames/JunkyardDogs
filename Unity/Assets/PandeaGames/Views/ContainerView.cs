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
            
        }

        public override Transform GetTransform()
        {
            return _transform;
        }

        public override RectTransform GetRectTransform()
        {
            return _rt;
        }

        public override void Show()
        {
            _worldObject = new GameObject();
            _worldObject.name = "ContainerView(" + _worldObject.GetInstanceID()+")";
            _transform = _worldObject.GetComponent<Transform>();
            _transform.SetParent(FindParentTransform());
            
            _uiObject = new GameObject();
            _rt = _uiObject.AddComponent<RectTransform>();
        }
    }
}