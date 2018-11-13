namespace PandeaGames.Views.ViewControllers
{
    public interface IViewController
    {
        IView GetView();
        void Update();
    }
}