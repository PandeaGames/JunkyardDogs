using JunkyardDogs.Data;
using PandeaGames;
using UnityEngine;

namespace PandeaGames
{
    public partial class SynchronousStaticDataProvider
    {
        public enum NationalityImageTypes
        {
            Flag
        }

        public Sprite GetData(NationalityImageTypes type, NationalityStaticDataReference nationality)
        {
            return GetData(type, nationality.Data);
        }

        public Sprite GetData(NationalityImageTypes type, Nationality nationality)
        {
            SpriteFactory spriteFactory = staticData.NaitonalitySpriteFactory;
            return spriteFactory.GetAsset(nationality);
        }
    }
}
