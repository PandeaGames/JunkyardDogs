using System;
using WeakReference = Data.WeakReference;

public class SingleSourceParticipant : Participant
{
    public WeakReference Source { get; set; }
    public int BotIndex { get; set; }
    
    public override void GetTeam(JunkyardUser user, Action<ParticipantTeam> onComplete, Action onError)
    {
        Source.LoadAsync<CompetitorBlueprintData>((data, refernce) =>
        {
            data.GetBlueprint().GenerateObject((generatedObject) =>
            {
                
                Competitor competitor = generatedObject as Competitor;
                ParticipantTeam team = new ParticipantTeam(competitor, competitor.Inventory.Bots[BotIndex]);

                onComplete(team);

            }, onError);
        }, onError);
    }
}
