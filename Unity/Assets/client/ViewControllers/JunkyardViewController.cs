using JunkyardDogs.Views;
using PandeaGames.Views;
using PandeaGames.Views.ViewControllers;

namespace JunkyardDogs
{
    public class JunkyardViewController: AbstractViewController
    {
        protected override IView CreateView()
        {
            return new Views.JunkyardView();
        }
    }
}