using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GladiatorBlazor.Models
{
    public class GameManager
    {
        public bool IsRunning { get; set; } = false;
        
        public event EventHandler MainLoopCompleted;
        public Gladiator _gladiator;
        public Monster _monster;
        public int _roundCount = 0;

        public List<string> RoundDescriptions { get; set; } = new List<string>();


        public async void MainLoop(Gladiator gladiator, Monster monster)
        {
            _gladiator = gladiator;
            _monster = monster;

            IsRunning = true;
            RoundDescriptions.Clear();
            RoundDescriptions.Add("Match Starting");
            while (IsRunning)
            {
                Round();
                MainLoopCompleted?.Invoke(this, EventArgs.Empty);
                await Task.Delay(20);
            }
        }

        public void StartGame()
        {
            if (!IsRunning)
            {
                _gladiator = new Gladiator("Forsete", 100, 100, 10, 20, 50, 100);
                _monster = new Monster("Troll", 100, 100, 20, 10, 20, 50);
                MainLoop(_gladiator, _monster);
            }
        }

        public void Round()
        {
            var gladiatorDamage = Attack(_gladiator, _monster);
            var monsterDamage = Attack(_monster, _gladiator);
            var gladiatorIniCheck = CheckIni(_gladiator, _monster);
            var successEvasionGladiator = CheckEvasion(_gladiator, _monster);
            var successEvasionMonster = CheckEvasion(_monster, _gladiator);
            CheckEndurance(_gladiator, _monster);
            _roundCount++;

            //TODO vapnen är hårdkodade in i texten nu.
            //TODO Refaktorera så att man inte upprepar sig med iniative och vem som startar
            //TODO Skapa olika rundor så det inte alltid är samma text
            //TODO ta bort IsRunning från ifsatsen nedan, man kollar det i mainloopen. Kan finnas på fler ställen.

            if (gladiatorIniCheck)
            {
                if (IsRunning && !successEvasionMonster)
                {
                    string gladiatorAttack = $"{_gladiator.Name} is getting ready for a fight. {_gladiator.Name} uses the moment when {_monster.Name} is getting the sun in the eyes" +
                    $" {_gladiator.Name} is ready for closecombat. {_gladiator.Name} swings his longsword against {_monster.Name} and {_monster.Name} takes {gladiatorDamage} damage.";
                    _monster.Health -= gladiatorDamage;
                    RoundDescriptions.Add(gladiatorAttack);
                    Surrender(_gladiator, _monster);
                }
                else if (IsRunning)
                {
                    RoundDescriptions.Add($"{_monster.Name} dodge the attack.");
                    Surrender(_gladiator, _monster);
                }

                if (IsRunning && !successEvasionGladiator)
                {
                    string monsterAttack = $"{_monster.Name} roars and runs against {_gladiator.Name} is shaking and looking terrified when {_monster.Name} starts the combat." +
                    $"{_monster.Name} is ready for closecombat." +
                    $"{_monster.Name} swings his trollhammer against {_gladiator.Name} and {_gladiator.Name} takes {monsterDamage} damage.";
                    _gladiator.Health -= monsterDamage;
                    RoundDescriptions.Add(monsterAttack);
                    Surrender(_monster, _gladiator);
                }
                else if (IsRunning)
                {
                    RoundDescriptions.Add($"{_gladiator.Name} dodge the attack.");
                    Surrender(_monster, _gladiator);
                }
            }
            else
            {
                if (IsRunning && !successEvasionGladiator)
                {
                    string monsterAttack = $"{_monster.Name} roars and runs against {_gladiator.Name} is shaking and looking terrified when {_monster.Name} starts the combat." +
                    $"{_monster.Name} is ready for closecombat." +
                    $"{_monster.Name} swings his trollhammer against {_gladiator.Name} and {_gladiator.Name} takes {monsterDamage} damage.";
                    _gladiator.Health -= monsterDamage;
                    RoundDescriptions.Add(monsterAttack);
                    Surrender(_monster, _gladiator);
                }
                else if (IsRunning)
                {
                    RoundDescriptions.Add($"{_gladiator.Name} dodge the attack.");
                    Surrender(_monster, _gladiator);
                }

                if (IsRunning && !successEvasionMonster)
                {
                    string gladiatorAttack = $"{_gladiator.Name} is getting ready for a fight. {_gladiator.Name} uses the moment when {_monster.Name} is getting the sun in the eyes " +
                    $" {_gladiator.Name} is ready for closecombat. {_gladiator.Name} swings his longsword against {_monster.Name} and {_monster.Name} takes {gladiatorDamage} damage.";
                    _monster.Health -= gladiatorDamage;
                    RoundDescriptions.Add(gladiatorAttack);
                    Surrender(_gladiator, _monster);
                }
                else if (IsRunning)
                {
                    RoundDescriptions.Add($"{_monster.Name} dodge the attack.");
                    Surrender(_gladiator, _monster);
                }
            }
            
        }

        public void CheckEndurance(Character gladiator, Character monster)
        {
            double enduranceBalance = 0.2;
            var gladiatorEndurance = gladiator.Endurance * enduranceBalance;
            var monsterEndurance = monster.Endurance * enduranceBalance;

            if (_roundCount > monsterEndurance)
            {
                RoundDescriptions.Add($"{_monster.Name} is to tired to coninue fighting and falls to the ground.");
                RoundDescriptions.Add($"{_gladiator.Name} wins the fight!!!");
                GameOver();
            }

            if (_roundCount > gladiatorEndurance)
            {
                RoundDescriptions.Add($"{_gladiator.Name} is to tired to coninue fighting and falls to the ground.");
                RoundDescriptions.Add($"{_monster.Name} wins the fight!!!");
                GameOver();
            }
        }
        public bool CheckIni(Character gladiator, Character monster)
        {
            var rndGladiator = new Random();
            double randomGladiatorIni = rndGladiator.Next(1, 20);

            var rndMonster = new Random();
            double randomMonsterIni = rndMonster.Next(1, 20);

            var gladiatorTotalIni = gladiator.Initiative + randomGladiatorIni;

            var monsterTotalIni = monster.Initiative + randomMonsterIni;


            if (gladiatorTotalIni >= monsterTotalIni)
            {
                return true;
            }
            else
            return false;
        }
        public double Attack(Character attacker, Character defender)
        {


            defender.SuccessEvasion = false; //TODO : make this a check, now all attacks hits.

            double totalDamage;
            if (!defender.SuccessEvasion)
            {
                var rnd = new Random();
                double randomAttackDmg = rnd.Next(1, 20);
                totalDamage = (attacker.Strength * 0.2) + randomAttackDmg; //+ attacker.Weapon.Damage;
            }
            else
            {
                totalDamage = 0;
            }

            return totalDamage;
        }
        //TODO kolla formel för UA
        public bool CheckEvasion(Character attacker, Character defender)
        {
            var rnd = new Random();
            double randomEvasion = rnd.Next(1, 20);
            var successEvasion = defender.Evasion + randomEvasion;

            if (attacker.WeaponSkill < successEvasion)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Surrender(Character attacker, Character defender)
        {


            if (attacker is Gladiator)
            {
                if (_monster.Health <= 0) //TODO gör så man kan ställa in taktik
                {
                    RoundDescriptions.Add($"{_gladiator.Name} has won the match!! {_monster.Name} cant take more damage and falls down to the ground.");
                    WinGame();
                }
            }

            if (attacker is Monster)
            {
                if (_gladiator.Health <= 0) //TODO gör så man kan ställa in taktik
                {
                    RoundDescriptions.Add($"{_monster.Name} has won the match!! {_gladiator.Name} cant take more damage and falls down to the ground.");
                    WinGame();
                }
            }

        }

        public void WinGame()
        {
            IsRunning = false;
            _roundCount = 0;
            
        }
        public void GameOver()
        {
            IsRunning = false;
            _roundCount = 0;
           
        }

    }
}
