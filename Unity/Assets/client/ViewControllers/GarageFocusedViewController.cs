using JunkyardDogs.Views;
using PandeaGames.Views;
using PandeaGames.Views.ViewControllers;

namespace JunkyardDogs
{
    public class GarageFocusedViewController : AbstractViewController
    {
        protected override IView CreateView()
        {
            return new GarageFocusedView();
        }
    }
}