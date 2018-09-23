using System;
using System.Collections;

public abstract class CompetitorProvider
{
    public void FetchCompetitor(Action<Competitor> onComplete, Action onError)
    {
        TaskProvider.Instance.DelayedAction(() => DoFetchCompetitor(onComplete, onError));
    }

    protected abstract void DoFetchCompetitor(Action<Competitor> onComplete, Action onError);
}