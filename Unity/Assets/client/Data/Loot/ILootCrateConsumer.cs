public interface ILootCrateConsumer
{
    IConsumable[] Consume(AbstractLootCrateData crateData, int seed);
    IConsumable[] Consume(ILoot[] crateContents, int seed);
    void Consume(IConsumable[] crateContents);
}
