namespace PandeaGames.Views.ViewControllers
{
    public class ContainerViewController : AbstractViewController
    {
        private IViewController _child;
        private IViewController _parent;
        
        public ContainerViewController(IViewController parent, IViewController child)
        {
            _parent = parent;
            _child = child;
        }
        
        public override void Initialize(IViewController parent)
        {
            base.Initialize(parent);
            
            _parent.Initialize(this);
            _child.Initialize(_parent);
        }

        protected override IView CreateView()
        {
            return _parent.GetView();
        }
        
        protected override void OnViewLoaded()
        {
            base.OnViewLoaded();
            _child.ShowView();
        }
        
        public override void Update()
        {
            base.Update();
            _parent.Update();
            _child.Update();
        }
    }
}