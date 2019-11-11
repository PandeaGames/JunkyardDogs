public interface ILootDataModel
{
    JunkyardUser User { get; }
    int Seed { get; }
}

public class LootDataModel : ILootDataModel
{
    public JunkyardUser User { get; private set; }
    public int Seed { get; private set;}

    public LootDataModel(JunkyardUser User, int Seed)
    {
        this.User = User;
        this.Seed = Seed;
    }
}
