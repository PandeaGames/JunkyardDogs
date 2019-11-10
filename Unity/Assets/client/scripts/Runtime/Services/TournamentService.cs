using JunkyardDogs;
using JunkyardDogs.Data;
using PandeaGames;
using PandeaGames.Services;

public class TournamentService : AbstractService
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

    public TournamentStatus GetTournamentStatus(TournamentStaticDataReference tournament)
    {
        return GetTournamentStatus(tournament.Data);
    }

    public TournamentStatus GetTournamentStatus(Tournament tournament)
    {
        TournamentMetaState meta;
        _user.Tournaments.TryGetTournamentMeta(tournament, out meta);

        if (meta == null)
        {
            return TournamentStatus.Locked;
        }

        int exp = _user.GetExp(tournament.nation); 
        BreakpointData breakpoint = _gameStaticData.NationalExpBreakpoints.Data;
        int completedBreakpoints = breakpoint.GetCompletedBreakpointIndex(exp);
        bool hasEnoughExperience = completedBreakpoints >= tournament.nationalExpUnlockBreakpoint;

        if (hasEnoughExperience)
        {
            if (meta.Completions > 0)
            {
                return TournamentStatus.Available; 
            }
            else
            {
                return TournamentStatus.New;
            }
        }
        else
        {
            return TournamentStatus.Locked;
        }
    }
}
