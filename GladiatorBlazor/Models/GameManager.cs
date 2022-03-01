using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GladiatorBlazor.Models
{
    public class GameManager
    {
        public bool IsRunning { get; set; } = false;
        public int RoundCount { get; set; }
        public event EventHandler MainLoopCompleted;
        public Gladiator _gladiator;
        public Monster _monster;
        public List<string> Rounds { get; set; } = new List<string>();


        public async void MainLoop(Gladiator gladiator, Monster monster)
        {
            _gladiator = gladiator;
            _monster = monster;

            IsRunning = true;
            Rounds.Add("Match Starting");
            while (IsRunning)
            {
                Round();

                MainLoopCompleted?.Invoke(this, EventArgs.Empty);
                await Task.Delay(20);

                //TODO skicka in rätt attacker/defender i attackklassen
            }
        }

        public void StartGame()
        {
            if (!IsRunning)
            {
                _gladiator = new Gladiator("Forsete", 100, 100, 20, 20);
                _monster = new Monster("Troll", 100, 100, 21, 15);
                MainLoop(_gladiator, _monster);
            }
        }

        public void Round()
        {
            //TODO ska inte vara hårdkodat
            var gladiatorDamage = Attack(_gladiator, _monster);
            var monsterDamage = Attack(_monster, _gladiator);
            var gladiatorIniCheck = CheckIni(_gladiator, _monster);

            //TODO göra så att samma spelare inte startar varje runda
            //TODO Lägga till undvika
            //TODO Skapa olika rundor så det inte alltid är samma text
            //TODO Ini ska beräknas med en randomsiffra liknande det som sker i attack

            
            if (gladiatorIniCheck)
            {
                if (IsRunning)
                {
                    string gladiatorAttack = $"{_gladiator.Name} börjar göra sig redo för en attack. {_gladiator.Name} utnyttjar att {_monster.Name} bländas av solen, backar undan " +
                    $" {_gladiator.Name} gör sig nu redo för närstrid. {_gladiator.Name} svingar Bastardsvärd mot {_monster.Name} som blir skadad {gladiatorDamage}.";
                    _monster.Health -= gladiatorDamage;
                    Rounds.Add(gladiatorAttack);
                    Surrender(_gladiator, _monster);
                }

                if (IsRunning)
                {
                    string monsterAttack = $"{_monster.Name} vrålar och rusar mot {_gladiator.Name} som darrar på läppen och ser vettskrämd ut när {_monster.Name} initierar striden." +
                    $"{_monster.Name} gör sig nu redo för närstrid." +
                    $"{_monster.Name} svingar Trollhammare mot {_gladiator.Name} som blir skadad {monsterDamage}.";
                    _gladiator.Health -= monsterDamage;
                    Rounds.Add(monsterAttack);
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
                    Rounds.Add(monsterAttack);
                    Surrender(_monster, _gladiator);
                }

                if (IsRunning)
                {
                    string gladiatorAttack = $"{_gladiator.Name} börjar göra sig redo för en attack. {_gladiator.Name} utnyttjar att {_monster.Name} bländas av solen, backar undan " +
                    $" {_gladiator.Name} gör sig nu redo för närstrid. {_gladiator.Name} svingar Bastardsvärd mot {_monster.Name} som blir skadad {gladiatorDamage}.";
                    _monster.Health -= gladiatorDamage;
                    Rounds.Add(gladiatorAttack);
                    Surrender(_gladiator, _monster);
                }
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
                    Rounds.Add("The Gladiator has won the match!! The monster cant take more damage and falls down to the ground.");
                    WinGame();
                }

                if (_gladiator.Endurance <= RoundCount)
                {
                    Rounds.Add("The gladiator is to tired to continue fighting so he falls to the ground and loses the match");
                    GameOver();
                }
            }

            if (attacker is Monster)
            {
                if (_gladiator.Health <= 0) //TODO gör så man kan ställa in taktik
                {
                    Rounds.Add("The Monster has won the match!! The Gladiator cant take more damage and falls down to the ground.");
                    WinGame();
                }

                if (_monster.Endurance <= RoundCount)
                {
                    Rounds.Add("The Monster is to tired to continue fighting so he falls to the ground and loses the match");
                    GameOver();
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
