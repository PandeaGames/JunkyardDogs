using PandeaGames.ViewModels;

namespace JunkyardDogs
{
    public class LoadingViewModel : AbstractViewModel
    {
        public delegate void LoadUpdateDelegate(float percentage);
        public event LoadUpdateDelegate OnLoadUpdate;

        private float _percentage;
        
        public void SetLoadingPercentage(float percentage)
        {
            _percentage = percentage;

            if (OnLoadUpdate != null)
            {
                OnLoadUpdate(_percentage);
            }
        }
    }
}