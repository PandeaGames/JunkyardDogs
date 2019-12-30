using PandeaGames;

public class JunkyardGeneratorUtilities : Singleton<JunkyardGeneratorUtilities>, IJunkyardGenerator
{
    public SerializedJunkyard Generate(byte[,] input, int seed)
    {
        //todo: Generate data
        return null;
    }

    public SerializedJunkyard Generate(int seed, int width, int height)
    {
        return Generate(new byte[width, height], seed);
    }
}