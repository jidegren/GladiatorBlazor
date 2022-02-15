namespace GladiatorBlazor.Models
{
    public class GameManager
    {
        public bool IsRunning { get; set; } = false;
        public int Round { get; set; }
        public Gladiator _gladiator;
        public Monster _monster;

        public async void MainLoop(Gladiator gladiator, Monster monster)
        {
            _gladiator = gladiator;
            _monster = monster;

            IsRunning = true;

            while (IsRunning)
            {
                //TODO skicka in rätt attacker/defender i attackklassen
            }
        }

        public void StartGame()
        {
            var gladiator = new Gladiator("Forsete", 100, 100, 20);
            var monster = new Monster("Troll", 50, 50, 21);

            if (IsRunning)
            {
                MainLoop(gladiator, monster);
            }
        }

        public void Attack(Character attacker, Character defender)
        {
            
            defender.SuccessEvasion = false; //TODO : make this a check, now all attacks hits.

            if (!defender.SuccessEvasion)
            {
                defender.Health -= (attacker.Strength * 0.2) + attacker.Weapon.Damage;
            }
        }
        public void Avoid(Character attacker, Character defender)
        {
            
        }

        public void Surrender(Character attacker, Character defender)
        {
            var gameManager = new GameManager();

            if (attacker is Gladiator)
            {
                if (_monster.Health <= 0) //TODO gör så man kan ställa in taktik
                {
                    gameManager.WinGame();
                }

                if (_gladiator.Endurance <= gameManager.Round)
                {
                    gameManager.GameOver();
                }
            }

            if (attacker is Monster)
            {
                if (_gladiator.Health <= 0) //TODO gör så man kan ställa in taktik
                {
                    gameManager.WinGame();
                }

                if (_monster.Endurance <= gameManager.Round)
                {
                    gameManager.GameOver();
                }
            }

        }

        public void WinGame()
        {
            IsRunning = false;
        }
        public void GameOver()
        {
            IsRunning = false;
        }

    }
}
