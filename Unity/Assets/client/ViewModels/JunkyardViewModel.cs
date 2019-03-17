using System;
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.Specifications;
using PandeaGames.ViewModels;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace JunkyardDogs
{
    public class JunkyardViewModel : AbstractViewModel
    {
        public event Action<Component> OnTakeJunk;
        
        public JunkyardUser User;
        

        public void TakeJunk(ManufacturerStaticDataReference manufacturer, SpecificationCatalogue.Product product)
        {
            OnTakeJunk(ManufacturerUtils.BuildComponent(manufacturer, product.Specification, product.Material));
        }
    }
}