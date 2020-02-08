using JunkyardDogs.Components;
using JunkyardDogs.Specifications;

namespace PandeaGames
{
    public partial class SynchronousStaticDataProvider
    {
        public ComponentArtConfigData GetComponentArtConfigData(IComponent component)
        {
            return GetComponentArtConfigData(component.Specification);
        }
        
        public ComponentArtConfigData GetComponentArtConfigData(Specification spec)
        {
            return staticData.ComponentArtConfig.GetConfig(spec.ID);
        }
        
        public ComponentArtConfigData GetHitscanStreamConfigData(Specification spec)
        {
            return staticData.HitscanStreamArtConfig.GetConfig(spec.ID);
        }
        
        public ComponentArtConfigData GetProjectileArtConfigData(Specification spec)
        {
            return staticData.ProjectileArtConfig.GetConfig(spec.ID);
        }
        
        public ComponentArtConfigData GetProjectileImpactArtConfigData(Specification spec)
        {
            return staticData.ProjectileImpactConfig.GetConfig(spec.ID);
        }
        
        public ComponentArtConfigData GetPulseArtConfigData(Specification spec)
        {
            return staticData.PulseArtConfig.GetConfig(spec.ID);
        }
        
        public ComponentArtConfigData GetPulseImpactArtConfigData(Specification spec)
        {
            return staticData.PulseImpactConfg.GetConfig(spec.ID);
        }
        
        public ComponentArtConfigData GetMeleeImpactArtConfigData(Specification spec)
        {
            return staticData.PulseImpactConfg.GetConfig(spec.ID);
        }
    }
}