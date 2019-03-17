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
        public static List<ParticipantTeam> GetTeam(List<Participant> participants, JunkyardUser user)
        {
                List<ParticipantTeam> output = new List<ParticipantTeam>();
                
                participants.ForEach((participant) =>
                {
                        output.Add(participant.GetTeam(user));
                });

                return output;
        }
        public abstract ParticipantTeam GetTeam(JunkyardUser user);
}