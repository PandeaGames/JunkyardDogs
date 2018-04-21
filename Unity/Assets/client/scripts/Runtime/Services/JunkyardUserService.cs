using UnityEngine;
using System.Collections;

public class JunkyardUserService : UserService<JunkyardUser>
{

    public override void StartService(ServiceManager serviceManager)
    {
        base.StartService(serviceManager);
    }

    protected override string SerializeUser(User user)
    {
        return JsonUtility.ToJson(user as JunkyardUser);
    }
}