using PandeaGames.Views;

namespace JunkyardDogs.Views
{
    public class ChooseNationlaityView : AbstractUnityView
    {
        public override void Show()
        {
            FindWindow().LaunchScreen("chooseCountry");
        }
    }
}