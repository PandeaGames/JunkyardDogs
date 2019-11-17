using System;
using System.Collections.Generic;
using PandeaGames.Services;
using PandeaGames.Data.Static;
using PandeaGames.ViewModels;


namespace PandeaGames
{
    using TServiceInstances = Dictionary<uint, IService>;
    using TServices = Dictionary<Type, Dictionary<uint, IService>>;
    using TViewModelInstances = Dictionary<uint, IViewModel>;
    using TViewModels = Dictionary<Type, Dictionary<uint, IViewModel>>;
    using TStaticDataProviderInstances = Dictionary<uint, IStaticDataProvider>;
    using TStaticDataProviders = Dictionary<Type, Dictionary<uint, IStaticDataProvider>>;
    
    public class Game : Singleton<Game>
    {
        private readonly TServices _services = new TServices();
        private readonly TViewModels _viewModels = new TViewModels();
        private readonly TStaticDataProviders _staticDataProviders = new TStaticDataProviders();

        private T GetDependancy<T, TInterface>(uint instanceId, Dictionary<Type, Dictionary<uint, TInterface>> lookup)
            where T : class, TInterface, new()
        {
            return GetDependancy<TInterface>(typeof(T), instanceId, lookup) as T;
        }

        private TInterface GetDependancy<TInterface>(Type type, uint instanceId, Dictionary<Type, Dictionary<uint, TInterface>> lookup)
        {
            Dictionary<uint, TInterface> instances = null;
            lookup.TryGetValue(type, out instances);

            if (instances == null)
            {
                instances = new Dictionary<uint, TInterface>();
                lookup.Add(type, instances);
            }

            TInterface service = default(TInterface);
            instances.TryGetValue(instanceId, out service);

            if (service == null)
            {
                service = (TInterface)Activator.CreateInstance(type);
                instances.Add(instanceId, service);
            }

            return service;
        }

        public TService GetService<TService>() where TService : class, IService, new()
        {
            return GetDependancy<TService, IService>(instanceId:0, lookup:_services);
        }

        public TViewModel GetViewModel<TViewModel>(uint instanceId) where TViewModel : class, IViewModel, new()
        {
            return GetDependancy<TViewModel, IViewModel>(instanceId:instanceId, lookup:_viewModels);
        }
        
        public TViewModel GetViewModel<TViewModel>() where TViewModel : class, IViewModel, new()
        {
            return new TViewModel();
        }
        
        public TStaticDataProvider GetStaticDataPovider<TStaticDataProvider>() where TStaticDataProvider : class, IStaticDataProvider, new()
        {
            return GetDependancy<TStaticDataProvider, IStaticDataProvider>(instanceId:0, lookup:_staticDataProviders);
        }
    }
}