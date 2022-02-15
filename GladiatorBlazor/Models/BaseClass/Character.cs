namespace GladiatorBlazor.Models
{
    public class Character
    {
        public string Name { get; set; }
        public double Health { get; set; }
        public double Strength { get; set; }
        public double Endurance { get; set; }
        public double Evasion { get; set; }
        public bool SuccessEvasion { get; set; }
        public Weapon Weapon { get; set; }
        public Armor Armor { get; set; } //TODO hur gör man i character klassen om man ska kunna ha olika typer av armor?
        public Item Item { get; set; } // TODO samma som ovan
        

        //TODO ställa in taktik? Var lägger man det
        //TODO om man vill kunna välja en vapentyp och lägga poäng där, HOW
        
    }
}
