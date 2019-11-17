using Data;
using PandeaGames.Data.WeakReferences;
using UnityEngine;

namespace PandeaGames
{
    public partial class SynchronousStaticData
    {
        [SerializeField, WeakReference(typeof(SpriteFactory))] 
        private WeakReference _naitonalitySpriteFactory;

        public SpriteFactory NaitonalitySpriteFactory
        {
            get { return _naitonalitySpriteFactory.Asset as SpriteFactory; }
        }

        partial void PopulatedLoadGroup(LoaderGroup loadGroup)
        {
            loadGroup.AppendProvider(_naitonalitySpriteFactory);
        }
    }
}