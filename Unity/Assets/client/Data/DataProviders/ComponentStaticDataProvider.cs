using JunkyardDogs.Components;

namespace PandeaGames
{
    public partial class SynchronousStaticDataProvider
    {
        public ComponentArtConfigData GetData(IComponent component)
        {
            return staticData.ComponentArtConfig.GetConfig(component.Specification.ID);
        }
    }
}