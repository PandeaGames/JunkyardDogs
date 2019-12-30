public interface IJunkyardGenerator
{
    SerializedJunkyard Generate(byte[,] input, int seed);
    SerializedJunkyard Generate(int seed, int width, int height);
}
