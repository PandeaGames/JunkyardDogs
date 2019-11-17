using PandeaGames.ViewModels;

namespace JunkyardDogs.scripts.Runtime.Dialogs
{
    public class TakeJunkDialogViewModel : AbstractDialogViewModel<TakeJunkDialogViewModel>
    {
        public struct Data
        {
            public readonly ILoot[] Loot;

            public Data(ILoot[] loot)
            {
                Loot = loot;
            }
        }
        
        public Data ModelData;
        public bool ShouldTakeLoot;

        public void SetData(Data modelData)
        {
            ModelData = modelData;
        }
    }
}