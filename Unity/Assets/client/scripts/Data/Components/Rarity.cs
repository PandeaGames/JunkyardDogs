using System;
using PandeaGames.Entities;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class Rarity : UintValueContainer<Rarity>
    {
        public Rarity(uint value) : base(value)
        {
            
        }

        public override Rarity Add(Rarity other)
        {
            return new Rarity(value + other.value);
        }

        public static Rarity operator +(Rarity rarity, uint bonus)
        {
            return new Rarity(rarity.value + bonus);
        }
    }
}