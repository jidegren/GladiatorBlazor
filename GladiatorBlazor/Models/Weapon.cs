namespace GladiatorBlazor.Models
{
    public class Weapon : Gear
    {
        public double Damage { get; set; }
        
        public WeaponType Type { get; set; }
    }
}
