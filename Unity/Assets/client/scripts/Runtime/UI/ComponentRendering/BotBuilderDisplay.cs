using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;
using JunkyardDogs.Specifications;
using System;

public class BotBuilderDisplay : MonoBehaviour
{
    public event Action<BotBuilderDisplay> OnSelect;

    [SerializeField]
    private Transform _cameraAgentContainer;

    [SerializeField]
    private CameraAgent _cameraAgent;

    [SerializeField]
    private PrefabFactory _botPrefabFactory;

    [SerializeField]
    private ServiceManager _serviceManager;

    [SerializeField]
    private Collider _collider;

    private bool _isFocused;
    public bool IsFocused { get { return _isFocused; } }

    private CameraService _cameraService;
    private InputService _inputService;
    private BotBuilder _botBuilder;
    private GameObject _bot;

    public void Setup(BotBuilder botBuilder)
    {
        _cameraService = _serviceManager.GetService<CameraService>();
        _inputService = _serviceManager.GetService<InputService>();

        _botBuilder = botBuilder;

        Bot bot = botBuilder.Bot;
        var chassis = bot.Chassis;
        var reference = chassis.SpecificationReference;

        reference.LoadAsync<JunkyardDogs.Specifications.Chassis>(
            (chassisSpec, referenceSpec) =>
            {
                SetupChassis(chassisSpec);
            }, () => { });

        _inputService.OnPointerDown += OnPointerDown;
    }

    private void OnPointerDown(Vector3 cameraPosition, RaycastHit raycast)
    {
        if(raycast.collider == _collider)
        {
            if (OnSelect != null)
                OnSelect(this);
        }
    }

    private void SetupChassis(JunkyardDogs.Specifications.Chassis chassis)
    {
        _bot = Instantiate(_botPrefabFactory.GetAsset(chassis));
        _bot.transform.SetParent(transform, false);
    }

    public void Blur()
    {
        _isFocused = false;
        _bot.transform.rotation.Set(0, 0, 0, 0);
    }

    public void Focus()
    {
        _isFocused = true;
        _cameraService.Focus(_cameraAgent);
        _cameraAgentContainer.transform.rotation.Set(0, 0, 0, 0);
        _bot.transform.rotation.Set(0, 0, 0, 0);
    }
}