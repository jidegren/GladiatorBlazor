namespace GladiatorBlazor.Models
{
    public class Monster : Character
    {
        public Monster()
        {

        }
        public Monster(string name, double health, double strength, double endurance, double iniative, double evasion, double weaponSkill)
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
