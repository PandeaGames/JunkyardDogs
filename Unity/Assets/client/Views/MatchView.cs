using PandeaGames.Views;

namespace JunkyardDogs.Views
{
    public class MatchView : SceneView
    {
        public MatchView() : base("Match")
        {
            
        }

        public override void Show()
        {
            base.Show();
            FindWindow().RemoveCurrentScreen();
        }
    }
}