using System.Collections;
using JunkyardDogs.Components;
using JunkyardDogs.Simulation;
using UnityEngine;
using UnityEngine.Assertions.Must;
using WeakReference = Data.WeakReference;

public class MatchTest : MonoBehaviour
{
    [SerializeField] 
    private ServiceManager _serviceManager;

    [SerializeField] 
    private int _userBotIndex;

    [SerializeField]
    public CompetitorBlueprintData _competitor;

    [SerializeField] 
    public bool _realTime;
    
    [SerializeField, WeakReference(typeof(JunkyardDogs.Simulation.Knowledge.State))]
    private WeakReference _state;

    private JunkyardUserService _userService;
    private SimulationService _simulationService;
    private Engagement _engagement;

    private void Start()
    {
        _userService = _serviceManager.GetService<JunkyardUserService>();
        _simulationService = _serviceManager.GetService<SimulationService>();

        JunkyardUser user = _userService.User;

         (_competitor.GetBlueprint() as CompetitorBlueprint).Generate((opponent) =>
        {
            _engagement = new Engagement();

            _engagement.BlueCombatent = user.Competitor.Inventory.Bots[_userBotIndex];
            _engagement.RedCombatent = opponent.Inventory.Bots[0];
            _engagement.SetTimeLimit(180);//3 minutes

            PrepareForBattle(_engagement.BlueCombatent);
            
            _engagement.BlueCombatent.LoadAsync(() => _engagement.RedCombatent.LoadAsync(() =>
            {
                _simulationService.SetEngagement(_engagement);
                _simulationService.StartSimulation(_realTime);

                StartCoroutine(EndOfBattleCoroutine());

            }, OnError), OnError);
            
            
        }, () => {
});
    }

    private void OnError()
    {
        
    }

    private void PrepareForBattle(Bot bot)
    {
        bot.Agent.States.ForEach((state) => { state.StateWeakReference = _state; });
    }

    private IEnumerator EndOfBattleCoroutine()
    {
        while (_engagement.Outcome == null)
        {
            yield return 0;
        }
        
        if (_engagement.Outcome.Winner == _engagement.RedCombatent)
        {
            Debug.Log("WINNER: RED");
        }
        else
        {
            Debug.Log("WINNER: Blue");
        }
    }
    
}
