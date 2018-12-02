using System;
using System.ComponentModel;
using PandeaGames.ViewModels;
using Component = JunkyardDogs.Components.Component;

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
        public Component Selected;

        public void SetData(Data data)
        {
            modelData = data;
        }
    }
}