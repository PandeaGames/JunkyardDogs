using PandeaGames.Data.WeakReferences;
using UnityEngine;
using System.Collections.Generic;
using Data;

namespace JunkyardDogs.Data
{
    public class MatchTestData : ScriptableObject, ILoadableObject
    {
        [SerializeField, WeakReference(typeof(ParticipantData))]
        private WeakReference _participantBlue;
        
        [SerializeField, WeakReference(typeof(ParticipantData))]
        private WeakReference _participantRed;

        private bool _isLoaded;

        public bool IsLoaded()
        {
            return _isLoaded;
        }
        
        public List<WeakReference> GetParicipants()
        {
            return new List<WeakReference>(){_participantBlue, _participantRed};
        }

        public void LoadAsync(LoadSuccess loadSuccess, LoadError loadError)
        {
            Loader loader = new Loader();
            loader.AppendProvider(_participantRed);
            loader.AppendProvider(_participantBlue);
            loader.LoadAsync(loadSuccess, loadError);
        }
    }
}