namespace PandeaGames.Entities
{
    public abstract class UintValueContainer<TValueContainer> : ValueContainer<TValueContainer, uint> where TValueContainer:UintValueContainer<TValueContainer>
    {
        public UintValueContainer(uint value) : base(value)
        {
            
        }
        
        public static bool operator >(UintValueContainer<TValueContainer> value1,
            UintValueContainer<TValueContainer> value2)
        {
            return value1.Value > value2.Value;
        }

        public static bool operator <(UintValueContainer<TValueContainer> value1,
            UintValueContainer<TValueContainer> value2)
        {
            return value1.Value < value2.Value;
        }
    }
}