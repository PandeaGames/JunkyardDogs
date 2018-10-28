using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;
using JunkyardDogs.Specifications;
using System;

public class BotBuilderDisplay : MonoBehaviour 
{
    public event Action<BotBuilderDisplay> OnSelect;

    [SerializeField] 
    private BotRenderConfiguration _botRenderConfiguration;
    
    [SerializeField]
    private CameraAgent _cameraAgent;

    [SerializeField]
    private PrefabFactory _botPrefabFactory;
    
    [SerializeField]
    private ServiceManager _serviceManager;

    [SerializeField]
    private Collider _collider;

    private bool _isFocused;
    private CameraService _cameraService;
    private InputService _inputService;
    private BotBuilder _botBuilder;
    private GameObject _bot;
    private Vector3 _pointerPosition;
    private JunkyardUserService _junkardUserService;
    private DialogService _dialogService;
    private JunkyardUser _user;

    public bool IsFocused { get { return _isFocused; } }
    public CameraAgent CameraAgent { get { return _cameraAgent; } }
    public BotBuilder BotBuilder { get { return _botBuilder; } }

    public void Setup(BotBuilder botBuilder)
    {
        _cameraService = _serviceManager.GetService<CameraService>();
        _inputService = _serviceManager.GetService<InputService>();
        _junkardUserService = _serviceManager.GetService<JunkyardUserService>();
        _dialogService = _serviceManager.GetService<DialogService>();

        _user = _junkardUserService.Load();

        _botBuilder = botBuilder;

        Bot bot = botBuilder.Bot;
        var chassis = bot.Chassis;

        bot.LoadAsync(
            () =>
            {
                SetupChassis(( JunkyardDogs.Specifications.Chassis)chassis.Specification);
            }, () => { });

        _inputService.OnPointerClick += OnPointerClick;
        _inputService.OnPointerDown += OnPointerDown;
    }

    private void OnPointerDown(Vector3 cameraPosition, RaycastHit raycast)
    {
        _pointerPosition = cameraPosition;
    }

    private void OnPointerClick(Vector3 cameraPosition, RaycastHit raycast)
    {
        if(!_isFocused)
        {
            if (raycast.collider == _collider)
            {
                if (OnSelect != null)
                    OnSelect(this);
            }
        }
        else
        {
            if (raycast.collider != null)
            {
                BotRenderer botRenderer = raycast.collider.transform.parent.GetComponent<BotRenderer>();
                Debug.Log("Check collider "+ raycast.collider + " botRenderer "+ botRenderer);
                if(botRenderer != null)
                {

                    HandleComponentTouch(botRenderer, raycast.collider.gameObject);
                }
            }
        }
    }

    private void HandleComponentTouch(BotRenderer botRenderer, GameObject collider)
    {
        Bot bot = _botBuilder.Bot;
        var chassis = bot.Chassis;

        BotAvatar botAvatar = _botRenderConfiguration.AvatarFactory.GetAsset(chassis.Specification) as BotAvatar;

        BotAvatar.AvatarComponent avatarComponent = botAvatar.GetAvatarComponent(collider);

        if (avatarComponent == BotAvatar.AvatarComponent.Plate)
        {
            ReplacePlate(botAvatar, collider, botRenderer);
        }
        else if(avatarComponent == BotAvatar.AvatarComponent.Armament)
        {
            ReplaceArmament(botAvatar, collider, botRenderer);
        }
    }

    private void ReplaceArmament(BotAvatar botAvatar, GameObject armamentCollider, BotRenderer botRenderer)
    {
        var location = botAvatar.GetArmamentLocation(armamentCollider);
        WeaponProcessorBuilder builder = _botBuilder.GetWeaponProcessorBuilder(location);

        if(builder == null)
        {
            var dialogConfig = new ChooseFromInventoryDialog.ChooseFromInventoryDialogConfig<WeaponProcessor>(_user.Competitor.Inventory);
            _dialogService.DisplayDialog<ChooseFromInventoryDialog>(dialogConfig, (Dialog.Response response) =>
            {
                var choice = response as ChooseFromInventoryDialog.ChooseFromInventoryDialogResponse;
                _botBuilder.SetWeaponProcessor(choice.Component as WeaponProcessor, location);
                _junkardUserService.Save();
                botRenderer.Render(_botBuilder.Bot, _botRenderConfiguration);
            });
        }
        else
        {
            var dialogConfig = new ChooseFromInventoryDialog.ChooseFromInventoryDialogConfig<JunkyardDogs.Components.Weapon>(_user.Competitor.Inventory);
            _dialogService.DisplayDialog<ChooseFromInventoryDialog>(dialogConfig, (Dialog.Response response) =>
            {
                var choice = response as ChooseFromInventoryDialog.ChooseFromInventoryDialogResponse;
                builder.SetWeapon(choice.Component as JunkyardDogs.Components.Weapon);
                _junkardUserService.Save();
                botRenderer.Render(_botBuilder.Bot, _botRenderConfiguration);
            });
        }
    }

    private void ReplacePlate(BotAvatar botAvatar, GameObject plateObject, BotRenderer botRenderer)
    {
        var dialogConfig = new ChooseFromInventoryDialog.ChooseFromInventoryDialogConfig<JunkyardDogs.Components.Plate>(_user.Competitor.Inventory);
        _dialogService.DisplayDialog<ChooseFromInventoryDialog>(dialogConfig, (Dialog.Response response) =>
        {
            var choice = response as ChooseFromInventoryDialog.ChooseFromInventoryDialogResponse;
            if (choice == null)
                return;
            _botBuilder.SetPlate(choice.Component as JunkyardDogs.Components.Plate, botAvatar.GetPlateLocation(plateObject), botAvatar.GetPlateIndex(plateObject));
            _junkardUserService.Save();
            botRenderer.Render(_botBuilder.Bot, _botRenderConfiguration);
        });
    }

    private void SelectedNewPlate(Dialog.Response response)
    {
        
    }

    private void SetupChassis(JunkyardDogs.Specifications.Chassis chassis)
    {
        _bot = _botPrefabFactory.InstantiateAsset(chassis);
        _bot.transform.SetParent(transform, false);
        BotRenderer renderer = _bot.AddComponent<BotRenderer>();
        renderer.Render(_botBuilder.Bot, _botRenderConfiguration);
    }

    public void Blur()
    {
        _isFocused = false;
        _bot.transform.rotation.Set(0, 0, 0, 0);
        _collider.enabled = true;
    }

    public void Focus()
    {
        _isFocused = true;
        _bot.transform.rotation.Set(0, 0, 0, 0);
        _inputService.OnPointerMove += OnPointerMove;
        _collider.enabled = false;
    }

    private void OnPointerMove(Vector3 cameraPosition, RaycastHit raycast)
    {
        if(raycast.collider != null && raycast.collider.transform.parent == _bot.transform)
        {
            float delta = cameraPosition.z - _pointerPosition.z;
            //_bot.transform.rotation.SetAxisAngle(Vector3.down, _bot.transform.rotation.eulerAngles.y + delta.x);
            _bot.transform.rotation = Quaternion.AngleAxis(_bot.transform.rotation.eulerAngles.y + delta * 10, Vector3.up);
            _pointerPosition = cameraPosition;
            //Debug.Log("_bot.transform.rotation.eulerAngles.y [" + _bot.transform.rotation.eulerAngles.y + "] delta.x[" + delta.x + "]");
        }
    }
}