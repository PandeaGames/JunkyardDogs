using UnityEngine;

public class ElementiaUserService : UserService<ElementiaUser>
{
    public void Awake()
    {
        _userDataStorageKeyPrefix = "elementia_";
    }
    public override void StartService(ServiceManager serviceManager)
    {
        base.StartService(serviceManager);
    }
}