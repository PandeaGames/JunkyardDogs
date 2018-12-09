using PandeaGames.Views;

namespace JunkyardDogs.Views
{
    public class ChooseBotFromInventoryView : SceneView
    {
        public ChooseBotFromInventoryView() : base("ChooseBot")
        {
            
        }

        public override void Show()
        {
            base.Show();
            FindWindow().LaunchScreen("ChooseBotScreen");
        }
    }
}