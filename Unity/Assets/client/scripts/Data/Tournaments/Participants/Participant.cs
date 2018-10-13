using JunkyardDogs.Components;
using System;

public struct ParticipantTeam
{
        private Competitor _competitor;
        private Bot _bot;
        
        public Competitor Competitor
        {
                get { return _competitor; }
        }
        
        public Bot Bot
        {
                get { return _bot; }
        }
        
        public ParticipantTeam(Competitor competitor, Bot bot)
        {
                _competitor = competitor;
                _bot = bot;
        }
}

public abstract class Participant
{
        public abstract void GetTeam(JunkyardUser user,  Action<ParticipantTeam> onComplete, Action onError);
}