using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using JunkyardDogs.Components;
using UnityEngine.UI.Extensions;

public class GarageScreen : ScreenController
{
    public class GarageScreenConfig:Config
    {
        public Garage Garage;
    }

    [SerializeField]
    private ServiceManager _serviceManager;

    [SerializeField]
    private Button _newBotButton;

    private JunkyardUserService _junkardUserService;
    private DialogService _dialogService;
    private JunkyardUser _user;
    private GarageScreenConfig _garageScreenConfig;
    private Garage _garage;

    public override void Setup(WindowController window, Config config)
    {
        base.Setup(window, config);

        _garageScreenConfig = config as GarageScreenConfig;
        _garage = _garageScreenConfig.Garage;
        _junkardUserService = _serviceManager.GetService<JunkyardUserService>();
        _dialogService = _serviceManager.GetService<DialogService>();

        _user = _junkardUserService.Load();

        _newBotButton.onClick.AddListener(OnNewBotClicked);
    }

    private void OnNewBotClicked()
    {
        var dialogConfig = new ChooseFromInventoryDialog.ChooseFromInventoryDialogConfig<Chassis>(_user.Competitor.Inventory);
        _dialogService.DisplayDialog<ChooseFromInventoryDialog>(dialogConfig, SelectedNewChassis);
    }

    private void SelectedNewChassis(Dialog.Response response)
    {
        var choice = response as ChooseFromInventoryDialog.ChooseFromInventoryDialogResponse;

        if(choice.Component != null)
        {
            BotBuilder builder = BotBuilder.CreateNewBot(_user.Competitor.Inventory, choice.Component as Chassis);
            _junkardUserService.Save();
            _garage.AddBuilder(builder);
        }
    }
}
