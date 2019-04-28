using PandeaGames.Views;

namespace JunkyardDogs.Views
{
    public class PreloaderView : AbstractUnityView
    {
        public override void Show()
        {
            FindWindow().LaunchScreen("preloader");
        }
    }
}