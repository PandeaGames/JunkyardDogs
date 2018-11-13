using UnityEngine;

namespace PandeaGames.Views.ViewControllers
{
    public class AbstractViewController : IViewController
    {
        protected readonly AbstractViewController _parent;
        private IView _view;
        
        public AbstractViewController() : this(null)
        {
            
        }
        
        public AbstractViewController(AbstractViewController parent)
        {
            _parent = parent;
            _view = new ContainerView();
        }

        public IView GetView()
        {
            return _view;
        }

        public virtual void Update()
        {
            
        }
    }
}