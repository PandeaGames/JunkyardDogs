using System;
using JunkyardDogs.Components;
using JunkyardDogs.Specifications;
using PandeaGames.ViewModels;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace JunkyardDogs
{
    public class JunkyardViewModel : AbstractViewModel
    {
        public event Action<Component> OnTakeJunk;
        
        public JunkyardUser User;
        

        public void TakeJunk(WeakReference manufacturer, SpecificationCatalogue.Product product)
        {
            ManufacturerUtils.BuildComponent(manufacturer, product, (component) =>
            {
                if (OnTakeJunk != null)
                {
                    OnTakeJunk(component);
                }
            });
        }
    }
}