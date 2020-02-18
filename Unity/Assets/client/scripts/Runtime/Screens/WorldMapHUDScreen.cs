using JunkyardDogs;
using PandeaGames;
using PandeaGames.Views.Screens;
using UnityEngine;

public class WorldMapHUDScreen : ScreenView
{
    [SerializeField] 
    private UserExperienceBreakpointsBehaviour _userExperienceBreakpoints;
    
    private WorldMapHudViewModel _vm;
    private JunkyardUserViewModel _userViewModel;
    private JunkyardUser _user;
    private JunkyardUserService _userService;
    
    public override void Setup(WindowView window)
    {
        base.Setup(window);

        _userService = Game.Instance.GetService<JunkyardUserService>();
        _vm = Game.Instance.GetViewModel<WorldMapHudViewModel>(0);
        _userViewModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
        _user = _userViewModel.UserData;
    }

    private void Update()
    {
        if (_userService == null)
        {
            return;
        }
        UserExperienceBreakpoints breakpointProgress = _userService.GetExperienceBreakpoints();
        _userExperienceBreakpoints.Render(breakpointProgress);
    }
}
