using System;
using JunkyardDogs.Data;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

public class SingleSourceParticipant : Participant
{
    public CompetitorBlueprintStaticDataReference Source { get; set; }
    public int BotIndex { get; set; }
    
    public override ParticipantTeam GetTeam(JunkyardUser user)
    {
            
        Competitor competitor = Source.Data.DoGenerate();
        ParticipantTeam team = new ParticipantTeam(competitor, competitor.Inventory.Bots[BotIndex]);
        return team;
    }
}
