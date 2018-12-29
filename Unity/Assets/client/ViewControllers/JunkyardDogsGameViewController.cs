using PandeaGames.Views.ViewControllers;
using PandeaGames.Views;
using JunkyardDogs.Views;
using PandeaGames;

namespace JunkyardDogs
{
    public class JunkyardDogsGameViewController : AbstractViewController
    {
        protected override IView CreateView()
        {
            return new JunkyardGameContainer();
        }
    }
}