using GladiatorBlazor.Models.Enum;

namespace GladiatorBlazor.Models
{
    public class Armor : Gear
    {
        public double Protection { get; set; }
        public ArmorType Type { get; set; }

        //TODO Add health, strength.. for example
    }
}
