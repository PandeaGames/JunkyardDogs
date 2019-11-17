using System;
using JunkyardDogs.Data;
using PandeaGames;

public class JunkyardUserService : AbstractUserService<JunkyardUser>
{
    private GameStaticData _gameStaticData;

    public UserExperienceBreakpoints GetExperienceBreakpoints()
    {
        _gameStaticData = Game.Instance.GetStaticDataPovider<GameStaticDataProvider>().GameDataStaticData;
        UserExperienceBreakpoints breakpoints = new UserExperienceBreakpoints(
            _gameStaticData.ExpBreakpoints, 
            _gameStaticData.NationalExpBreakpoints, 
            User.Experience
        );

        return breakpoints;
    }
}