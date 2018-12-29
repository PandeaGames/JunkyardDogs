using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class TournamentStateRenderer : MonoBehaviour
{
    [SerializeField]
    private GameObject _matchObjectSource;

    private RectTransform _rt;

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
    }
    
    public void Render(TournamentState state)
    {
        TournamentState.TournamentStatus status = state.GetStatus();
        
        int stageCount = 0;
        foreach (StageState stage in state.StageStates)
        {
            int roundCount = 0;
            foreach (RoundState round in stage.Rounds)
            {
                int matchCount = 0;
                foreach (MatchState match in round.Matches)
                {
                    GameObject matchViewInstance = Instantiate(_matchObjectSource, _rt, false);
                    MatchStateRenderer matchStateRenderer = matchViewInstance.GetComponent<MatchStateRenderer>();
                    bool isActiveMatch = status.Match == match;
                    matchStateRenderer.Render(match, isActiveMatch);

                    RectTransform rt = matchViewInstance.GetComponent<RectTransform>();
                    rt.anchorMax = new Vector2(0.5f, 0.5f);
                    rt.anchorMin = new Vector2(0.5f, 0.5f);

                    rt.anchoredPosition = new Vector2(roundCount * 50, matchCount * 30);
                    
                    matchCount++;
                }
                
                roundCount++;
            }
            
            stageCount++;
        }
        
        _matchObjectSource.SetActive(false);
    }
}
