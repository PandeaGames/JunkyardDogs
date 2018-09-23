using System;
using WeakReference = Data.WeakReference;

public class DataCompetitorProvider : CompetitorProvider
{
    public WeakReference CompetitorBlueprint { get; set; }
    
    protected override void DoFetchCompetitor(Action<Competitor> onComplete, Action onError)
    {
       // CompetitorBlueprint.LoadAsync(());
    }
}