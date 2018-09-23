using JunkyardDogs.Components;
using JunkyardDogs.Simulation;
using UnityEngine;
using WeakReference = Data.WeakReference;

public class MatchTest : MonoBehaviour
{
    [SerializeField] 
    private ServiceManager _serviceManager;

    [SerializeField] 
    private int _userBotIndex;

    [SerializeField]
    public CompetitorBlueprintData _competitor;
    
    [SerializeField, WeakReference(typeof(JunkyardDogs.Simulation.Knowledge.State))]
    private WeakReference _state;

    private JunkyardUserService _userService;
    private SimulationService _simulationService;

    private void Start()
    {
        _userService = _serviceManager.GetService<JunkyardUserService>();
        _simulationService = _serviceManager.GetService<SimulationService>();

        JunkyardUser user = _userService.User;

         (_competitor.GetBlueprint() as CompetitorBlueprint).Generate((opponent) =>
        {
            Engagement engagement = new Engagement();

            engagement.BlueCombatent = user.Competitor.Inventory.Bots[_userBotIndex];
            engagement.RedCombatent = opponent.Inventory.Bots[0];

            PrepareForBattle(engagement.BlueCombatent);
            
            engagement.BlueCombatent.LoadAsync(() => engagement.RedCombatent.LoadAsync(() =>
            {
                _simulationService.SetEngagement(engagement);
                _simulationService.StartSimulation();
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
}
