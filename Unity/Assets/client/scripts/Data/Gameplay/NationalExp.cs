
public class NationalExp : IConsumable
{
    public Nationality Nationality { private set; get; }
    public int Exp { private set; get; }
    
    public NationalExp(Nationality nationality, int exp)
    {
        Nationality = nationality;
        Exp = exp;
    }
}
