namespace PandeaGames.Views.ViewControllers
{
    public interface IViewController
    {
        IView GetView();
        void Update();
        void ShowView();
        void RemoveView();
        void Initialize(IViewController parent);
        IViewController GetParent();
        void DisplayChild(IViewController child);
        void RemoveChild(IViewController child);
    }
}