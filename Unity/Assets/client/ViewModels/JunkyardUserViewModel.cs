using PandeaGames.ViewModels;
using UnityEngine;

namespace JunkyardDogs
{
    public class JunkyardUserViewModel : AbstractUserViewModel<JunkyardUser>, ILootCrateConsumer
    {
        public delegate void LootDelegate(IConsumable[] crateContents);
        public delegate void ScreenSpaceLootDelegate(IConsumable[] crateContents, Vector3 screenSpaceCollectioPoint);
        public LootDelegate OnLootConsumed;
        public ScreenSpaceLootDelegate OnScreenSpaceLootConsumed;
        
        public IConsumable[] Consume(AbstractLootCrateData crateData, int seed)
        {
            IConsumable[] loot = UserData.Consume(crateData, seed);
            OnLootConsumed?.Invoke(loot);
            return loot;
        }
        
        public void Consume(IConsumable[] consumables)
        {
            OnLootConsumed?.Invoke(consumables);
            UserData.Consume(consumables);
        }

        public IConsumable[] Consume(ILoot[] crateContents, int seed)
        {
            IConsumable[] consumables = UserData.Consume(crateContents, seed);
            OnLootConsumed?.Invoke(consumables);
            return consumables;
        }
        
        public IConsumable[] Consume(AbstractLootCrateData crateContents, int seed, Vector3 screenSpaceCollectionPoint)
        {
            IConsumable[] consumables = Consume(crateContents, seed);
            OnScreenSpaceLootConsumed?.Invoke(consumables, screenSpaceCollectionPoint);
            return consumables;
        }
        
        public void Consume(ILoot[] crateContents, int seed, Vector3 screenSpaceCollectionPoint)
        {
            IConsumable[] consumables = Consume(crateContents, seed);
            OnScreenSpaceLootConsumed?.Invoke(consumables, screenSpaceCollectionPoint);
        }
    }
}