using UnityEngine;
using System.Collections;

public class JunkyardUserService : AbstractUserService<JunkyardUser>, ILootCrateConsumer
{
    public void Consume(AbstractLootCrateData crateData)
    {
        throw new System.NotImplementedException();
    }

    public void Consume(ILoot[] crateContents)
    {
        throw new System.NotImplementedException();
    }
}