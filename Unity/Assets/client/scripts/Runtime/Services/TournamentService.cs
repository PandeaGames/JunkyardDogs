using JunkyardDogs;
using JunkyardDogs.Data;
using PandeaGames;
using PandeaGames.Services;

public class TournamentService : AbstractService<TournamentService>
{
    private JunkyardUserService _userService;
    private JunkyardUser _user;
    private GameStaticData _gameStaticData;
    
    public TournamentService() : base()
    {
        _userService = Game.Instance.GetService<JunkyardUserService>();
        _user = _userService.User;
        _gameStaticData = Game.Instance.GetStaticDataPovider<GameStaticDataProvider>().GameDataStaticData;
    }
}
