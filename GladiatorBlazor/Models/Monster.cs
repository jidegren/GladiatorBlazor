namespace GladiatorBlazor.Models
{
    public class Monster : Character
    {
        public Monster()
        {

        }
        public Monster(string name, double health, double strength, double endurance)
        {
            Name = name;
            Health = health;
            Strength = strength;
            Endurance = endurance;
        }
    }

}
