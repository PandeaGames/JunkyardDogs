using PandeaGames.Views.Screens;
using PandeaGames.Views.ViewControllers;
using UnityEngine;

namespace PandeaGames.Views
{
    public class AbstractUnityView : IView
    { 
        protected ServiceManager _serviceManager;
        protected WindowView _window;
        protected IViewController _viewController;
        
        public virtual void InitializeView(IViewController controller)
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

        public virtual void Show()
        {
            
        }

        public WindowView FindWindow()
        {
            return FindWindow(_viewController);
        }
        
        public WindowView FindWindow(IViewController viewController)
        {
            WindowView windowView = viewController.GetView().GetWindow();
            IViewController parentViewController = viewController.GetParent();
            
            if (windowView)
            {
                return windowView;
            }
            else if(parentViewController != null)
            {
                return FindWindow(parentViewController);
            }

            return null;
        }
        
        public ServiceManager FindServiceManager()
        {
            return FindServiceManager(_viewController);
        }
        
        public ServiceManager FindServiceManager(IViewController viewController)
        {
            ServiceManager serviceManager = viewController.GetView().GetServiceManager();
            IViewController parentViewController = viewController.GetParent();
            
            if (serviceManager)
            {
                return serviceManager;
            }
            else if(parentViewController != null)
            {
                return FindServiceManager(parentViewController);
            }

            return null;
        }

        public WindowView GetWindow()
        {
            return _window;
        }

        public ServiceManager GetServiceManager()
        {
            return _serviceManager;
        }
    }
}