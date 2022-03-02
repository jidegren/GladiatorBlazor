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
                _gladiator = new Gladiator("Forsete", 100, 100, 10, 20);
                _monster = new Monster("Troll", 100, 100, 20, 10);
                MainLoop(_gladiator, _monster);
            }
        }

        public void Round()
        {
            //TODO ska inte vara hårdkodat
            var gladiatorDamage = Attack(_gladiator, _monster);
            var monsterDamage = Attack(_monster, _gladiator);
            var gladiatorIniCheck = CheckIni(_gladiator, _monster);


            //TODO Refakturera så att man inte upprepar sig med iniative och vem som startar
            //TODO Lägga till undvika
            //TODO Skapa olika rundor så det inte alltid är samma text
            //TODO Uthållighet och antal rundor


            _roundCount++;
            CheckEndurance(_gladiator, _monster);

            if (gladiatorIniCheck)
            {
                if (IsRunning)
                {
                    string gladiatorAttack = $"{_gladiator.Name} börjar göra sig redo för en attack. {_gladiator.Name} utnyttjar att {_monster.Name} bländas av solen, backar undan " +
                    $" {_gladiator.Name} gör sig nu redo för närstrid. {_gladiator.Name} svingar Bastardsvärd mot {_monster.Name} som blir skadad {gladiatorDamage}.";
                    _monster.Health -= gladiatorDamage;
                    RoundDescriptions.Add(gladiatorAttack);
                    Surrender(_gladiator, _monster);
                }

                if (IsRunning)
                {
                    string monsterAttack = $"{_monster.Name} vrålar och rusar mot {_gladiator.Name} som darrar på läppen och ser vettskrämd ut när {_monster.Name} initierar striden." +
                    $"{_monster.Name} gör sig nu redo för närstrid." +
                    $"{_monster.Name} svingar Trollhammare mot {_gladiator.Name} som blir skadad {monsterDamage}.";
                    _gladiator.Health -= monsterDamage;
                    RoundDescriptions.Add(monsterAttack);
                    Surrender(_monster, _gladiator);
                    
                }
            }
            else
            {
                if (IsRunning)
                {
                    string monsterAttack = $"{_monster.Name} vrålar och rusar mot {_gladiator.Name} som darrar på läppen och ser vettskrämd ut när {_monster.Name} initierar striden." +
                    $"{_monster.Name} gör sig nu redo för närstrid." +
                    $"{_monster.Name} svingar Trollhammare mot {_gladiator.Name} som blir skadad {monsterDamage}.";
                    _gladiator.Health -= monsterDamage;
                    RoundDescriptions.Add(monsterAttack);
                    Surrender(_monster, _gladiator);
                }

                if (IsRunning)
                {
                    string gladiatorAttack = $"{_gladiator.Name} börjar göra sig redo för en attack. {_gladiator.Name} utnyttjar att {_monster.Name} bländas av solen, backar undan " +
                    $" {_gladiator.Name} gör sig nu redo för närstrid. {_gladiator.Name} svingar Bastardsvärd mot {_monster.Name} som blir skadad {gladiatorDamage}.";
                    _monster.Health -= gladiatorDamage;
                    RoundDescriptions.Add(gladiatorAttack);
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
                RoundDescriptions.Add($"{_monster.Name} är för trött för att fortsätta slåss och faller ner till marken.");
                RoundDescriptions.Add($"{_gladiator.Name} vinner striden!!!");
                GameOver();
            }

            if (_roundCount > gladiatorEndurance)
            {
                RoundDescriptions.Add($"{_gladiator.Name} är för trött för att fortsätta slåss och faller ner till marken.");
                RoundDescriptions.Add($"{_monster.Name} vinner striden!!!");
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
        public void Avoid(Character attacker, Character defender)
        {

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
