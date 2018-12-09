using JunkyardDogs.Components;
using System;
using System.Collections.Generic;

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
        public static void GetTeam(List<Participant> participants, JunkyardUser user, Action<List<ParticipantTeam>> onComplete, Action onError)
        {
                List<ParticipantTeam> output = new List<ParticipantTeam>();
                
                participants.ForEach((participant) =>
                {
                        participant.GetTeam(user, (team) =>
                        {
                                output.Add(team);
                                if (output.Count == participants.Count)
                                {
                                        onComplete(output);
                                }
                        }, onError);
                });
        }
        public abstract void GetTeam(JunkyardUser user,  Action<ParticipantTeam> onComplete, Action onError);
}