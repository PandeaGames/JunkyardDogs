public interface ILootCrateConsumer
{
    void Consume(AbstractLootCrateData crateData);
    void Consume(ILoot[] crateContents);
}
