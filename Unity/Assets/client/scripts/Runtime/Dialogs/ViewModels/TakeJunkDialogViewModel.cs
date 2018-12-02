using System.ComponentModel;
using PandeaGames.Data.WeakReferences;
using PandeaGames.ViewModels;
using JunkyardDogs.Components;
using Component = JunkyardDogs.Components.Component;

namespace JunkyardDogs.scripts.Runtime.Dialogs
{
    public class TakeJunkDialogViewModel : AbstractDialogViewModel<TakeJunkDialogViewModel>
    {
        public struct Data
        {
            public readonly Component Component;

            public Data(Component component)
            {
                Component = component;
            }
        }
        
        public Data ModelData;
        public bool ShouldTakeComponent;

        public void SetData(Data modelData)
        {
            ModelData = modelData;
        }
    }
}