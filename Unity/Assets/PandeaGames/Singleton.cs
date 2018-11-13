namespace PandeaGames
{
    public abstract class Singleton<T> where T:new()
    {
        private static readonly T instance = new T();

        public static T Instance
        {
            get
            {
                return instance;
            }
        }
    }
}