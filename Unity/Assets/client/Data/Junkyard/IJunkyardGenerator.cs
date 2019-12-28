public interface IJunkyardGenerator
{
    byte[,] Generate(byte[,] input, int seed);
    byte[,] Generate(int seed, int width, int height);
}
