using System;
using JunkyardDogs.Components;
using PandeaGames.ViewModels;

namespace JunkyardDogs.scripts.Runtime.Dialogs
{
    public class ChooseFromInventoryViewModel:AbstractDialogViewModel<ChooseFromInventoryViewModel>
    {
        public struct Data
        {
            public readonly Type Type;
            public readonly Inventory Inventory;

            public Data(Type type, Inventory inventory)
            {
                Type = type;
                Inventory = inventory;
            }
        }

        public Data modelData;
        public IComponent Selected;

        public void SetData(Data data)
        {
            modelData = data;
        }
    }
}