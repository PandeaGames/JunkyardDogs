using PandeaGames;

public class JunkyardGeneratorUtilities : Singleton<JunkyardGeneratorUtilities>, IJunkyardGenerator
{
    public byte[,] Generate(byte[,] input, int seed)
    {
        //todo: Generate data
        return input;
    }

    public byte[,] Generate(int seed, int width, int height)
    {
        return Generate(new byte[width, height], seed);
    }
}