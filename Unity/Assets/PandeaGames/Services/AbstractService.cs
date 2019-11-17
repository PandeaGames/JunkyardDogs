namespace PandeaGames.Services
{
    public class AbstractService<TService> : IService where TService:AbstractService<TService>, new()
    {
        private static TService _instance;
        public static TService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Game.Instance.GetService<TService>();
                }

                return _instance;
            }
        }
    }
}