using PandeaGames.Views.ViewControllers;
using PandeaGames.Views;

namespace JunkyardDogs
{
    public class JunkyardViewController : AbstractViewController
    {
        protected override IView CreateView()
        {
            return new JunkyardGameContainer();
        }
    }
}