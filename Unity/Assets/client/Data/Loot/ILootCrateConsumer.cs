public interface ILootCrateConsumer
{
    void Consume(AbstractLootCrateData crateData, int seed);
    void Consume(ILoot[] crateContents, int seed);
}
