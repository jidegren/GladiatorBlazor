namespace GladiatorBlazor.Models
{
    public class Gladiator : Character
    {
        public Gladiator()
        {

        }
        public Gladiator(string name, double health, double strength, double endurance )
        {
            Name = name;
            Health = health;
            Strength = strength;
            Endurance = endurance;
        }

    }
}
