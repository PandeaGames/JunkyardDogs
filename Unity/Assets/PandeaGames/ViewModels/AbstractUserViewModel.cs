namespace PandeaGames.ViewModels
{
    public abstract class AbstractUserViewModel<TUserData> : AbstractViewModel where TUserData:User
    {
        public delegate void UserDataDelegate(TUserData user);
        public event UserDataDelegate OnUserDataLoaded;

        public TUserData UserData;

        public void SetUserData(TUserData data)
        {
            UserData = data;

            if (OnUserDataLoaded != null)
            {
                OnUserDataLoaded(data);
            }
        }
    }
}