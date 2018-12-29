using PandeaGames.Views.Screens;
using System.Collections.Generic;

namespace PandeaGames.Views.ViewControllers
{
    public class AbstractViewController : IViewController
    {
        public IViewController parent;
        private List<IViewController> _children;
        protected IView _view;

        public virtual void Initialize(IViewController parent)
        {
            this.parent = parent;
            _children = new List<IViewController>();
        }

        public IViewController GetParent()
        {
            return parent;
        }

        public void DisplayChild(IViewController child)
        {
            _children.Add(child);
            child.Initialize(this);
            child.ShowView();
        }
        
        public void RemoveChild(IViewController child)
        {
            
        }
        
        public IView GetView()
        {
            if (_view == null)
            {
                return CreateView();
            }
            else
            {
                return _view;
            }
        }
        
        protected virtual IView CreateView()
        {
            return new ContainerView();
        }

        public WindowView GetParentWindow()
        {
            return GetParentWindow(this);
        }
        
        private WindowView GetParentWindow(IViewController viewController)
        {
            if (viewController == null)
                return null;
            
            WindowView window = viewController.GetView().GetWindow();
            
            if (window != null)
            {
                return window;
            }
            else
            {
                return GetParentWindow(viewController.GetParent());
            }
        }

        public virtual void Update()
        {
            
        }

        protected virtual void OnBeforeShow()
        {
            
        }
        
        protected virtual void OnAfterShow()
        {
            
        }

        public virtual void ShowView()
        {
            if (_view == null)
            {
                OnBeforeShow();
                _view = GetView();
                _view.InitializeView(this);
                _view.LoadAsync(OnViewLoaded, OnViewLoadError);
            }
        }

        public void RemoveView()
        {
            if (_view != null)
            {
                _view.Destroy();
            }
            
            _view = null;
            
        }

        protected virtual void OnViewLoaded()
        {
            _view.Show();
        }
        
        protected virtual void  OnViewLoadError(LoadException error)
        {
            
        }
    }
}