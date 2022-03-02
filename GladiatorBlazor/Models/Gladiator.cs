namespace GladiatorBlazor.Models
{
    public class Gladiator : Character
    {
        public Gladiator()
        {

        }
        public Gladiator(string name, double health, double strength, double endurance, double iniative, double evasion, double weaponSkill)
        {
            Name = name;
            Health = health;
            Strength = strength;
            Endurance = endurance;
            Initiative = iniative;
            Evasion = evasion;
            WeaponSkill = weaponSkill;
        }

    }
}
