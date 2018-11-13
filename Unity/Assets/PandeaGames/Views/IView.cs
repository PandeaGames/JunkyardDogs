using PandeaGames.Views.ViewControllers;
using UnityEngine;

namespace PandeaGames.Views
{
    public interface IView : ILoadableObject
    {
        void InitializeView(IViewController controller);
        Transform GetTransform();
        RectTransform GetRectTransform();
        void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadError);
        bool IsLoaded();
        void Destroy();
        ServiceManager GetServiceManager();
    }
}