using PandeaGames.Views;
using PandeaGames.Views.Screens;

namespace JunkyardDogs.Views
{
    public class GarageLineupView : AbstractUnityView
    {
        public override void Show()
        {
            FindWindow().LaunchScreen("garageScreen");
        }
    }
}