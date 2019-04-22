using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;
using JunkyardDogs.Specifications;
using System;
using JunkyardDogs;
using JunkyardDogs.Data;
using JunkyardDogs.scripts.Runtime.Dialogs;
using PandeaGames;

public class BotBuilderDisplay : MonoBehaviour 
{
    public event Action<BotBuilderDisplay> OnSelect;

    [SerializeField] 
    private BotRenderConfiguration _botRenderConfiguration;
    
    [SerializeField]
    private CameraAgent _cameraAgent;

    [SerializeField]
    private Collider _collider;

    private bool _isFocused;
    private CameraViewModel _cameraViewModel;
    private GarageViewModel _garageViewModel;
    private InputService _inputService;
    private BotBuilder _botBuilder;
    private GameObject _bot;
    private Vector3 _pointerPosition;
    private DialogService _dialogService;
    private JunkyardUser _user;

    public bool IsFocused { get { return _isFocused; } }
    public CameraAgent CameraAgent { get { return _cameraAgent; } }
    public BotBuilder BotBuilder { get { return _botBuilder; } }

    public void Setup(BotBuilder botBuilder)
    {
        _cameraViewModel = Game.Instance.GetViewModel<CameraViewModel>(0);
        _inputService = InputService.Instance;

        _user = Game.Instance.GetViewModel<JunkyardUserViewModel>(0).UserData;
        _garageViewModel = Game.Instance.GetViewModel<GarageViewModel>(0);

        _botBuilder = botBuilder;

        Bot bot = botBuilder.Bot;
        var chassis = bot.Chassis;
        
        _inputService.OnPointerClick += OnPointerClick;
        _inputService.OnPointerDown += OnPointerDown;
        
        SetupChassis(( JunkyardDogs.Specifications.Chassis)chassis.Specification);
    }

    private void OnDestroy()
    {
        if (_inputService != null)
        {
            _inputService.OnPointerMove -= OnPointerMove;
            _inputService = null;
        }
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
            ChooseFromInventoryViewModel vm = Game.Instance.GetViewModel<ChooseFromInventoryViewModel>();
            vm.SetData(new ChooseFromInventoryViewModel.Data(typeof(WeaponProcessor), _user.Competitor.Inventory));

            vm.OnClose += (closedModel) =>
            {
                _botBuilder.SetWeaponProcessor(vm.Selected as WeaponProcessor, location);
                botRenderer.Render(_botBuilder.Bot, _botRenderConfiguration);
            };
            
            _garageViewModel.ChooseFromInventory(vm);
        }
        else
        {
            ChooseFromInventoryViewModel vm = Game.Instance.GetViewModel<ChooseFromInventoryViewModel>();
            vm.SetData(new ChooseFromInventoryViewModel.Data(typeof(JunkyardDogs.Components.Weapon), _user.Competitor.Inventory));

            vm.OnClose += (closedModel) =>
            {
                builder.SetWeapon(closedModel.Selected as JunkyardDogs.Components.Weapon);
                botRenderer.Render(_botBuilder.Bot, _botRenderConfiguration);
            };
            
            _garageViewModel.ChooseFromInventory(vm);
        }
    }

    private void ReplacePlate(BotAvatar botAvatar, GameObject plateObject, BotRenderer botRenderer)
    {
        ChooseFromInventoryViewModel vm = Game.Instance.GetViewModel<ChooseFromInventoryViewModel>();
        vm.SetData(new ChooseFromInventoryViewModel.Data(typeof(JunkyardDogs.Components.Plate), _user.Competitor.Inventory));

        vm.OnClose += (closedModel) =>
        {
            if (closedModel.Selected == null)
                return;
            _botBuilder.SetPlate(closedModel.Selected as JunkyardDogs.Components.Plate, botAvatar.GetPlateLocation(plateObject), botAvatar.GetPlateIndex(plateObject));
            botRenderer.Render(_botBuilder.Bot, _botRenderConfiguration);
        };
        
        _garageViewModel.ChooseFromInventory(vm);
    }

    private void SetupChassis(JunkyardDogs.Specifications.Chassis chassis)
    {
        _bot = Game.Instance.GetStaticDataPovider<GameStaticDataProvider>().GameDataStaticData.BotPrefabFactory.InstantiateAsset(chassis);
        _bot.transform.SetParent(transform, false);
        BotRenderer renderer = _bot.AddComponent<BotRenderer>();
        renderer.Render(_botBuilder.Bot, _botRenderConfiguration);
    }

    public void Blur()
    {
        _isFocused = false;
        _bot.transform.rotation.Set(0, 0, 0, 0);
        _inputService.OnPointerMove -= OnPointerMove;
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
        }
    }
}