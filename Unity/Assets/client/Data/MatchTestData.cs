using System;
using UnityEngine;
using System.Collections.Generic;
using Data;
using UnityStandardAssets.Utility;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace JunkyardDogs.Data
{
    [CreateAssetMenu(menuName = "TestData/MatchTestData")]
    public class MatchTestData : ScriptableObject
    {
        [SerializeField, StaticDataReference(path:ParticipantDataProvider.FULL_PATH)]
        private ParticipantStaticDataReference _participantBlue;
        
        [SerializeField, StaticDataReference(path:ParticipantDataProvider.FULL_PATH)]
        private ParticipantStaticDataReference _participantRed;

        private bool _isLoaded;

        public bool IsLoaded()
        {
            return _isLoaded;
        }
        
        public List<ParticipantStaticDataReference> GetParicipants()
        {
            return new List<ParticipantStaticDataReference>(){_participantBlue, _participantRed};
        }

        public List<ParticipantTeam> GetParticipants(JunkyardUser user)
        {
            var participants = ParticipantDataUtils.GenerateParticipants(
                new List<ParticipantStaticDataReference>() {_participantBlue, _participantRed});

            List<ParticipantTeam> teams = Participant.GetTeam(participants, user);

            return teams;
        }
    }
}