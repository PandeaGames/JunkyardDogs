using PandeaGames.Views.ViewControllers;
using UnityEngine;

namespace PandeaGames.Views
{
    public class AbstractUnityView : IView
    {
        [SerializeField] 
        private ServiceManager _serviceManager;
        
        protected IViewController _viewController;
        
        public void InitializeView(IViewController controller)
        {
            _viewController = controller;
        }

        public virtual Transform GetTransform()
        {
            return null;
        }
        
        public virtual RectTransform GetRectTransform()
        {
            return null;
        }

        public virtual void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadError)
        {
            onLoadSuccess();
        }

        public bool IsLoaded()
        {
            return true;
        }

        public virtual void Destroy()
        {
            
        }

        public ServiceManager GetServiceManager()
        {
            return _serviceManager;
        }
    }
}