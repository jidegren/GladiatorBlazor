﻿using System;
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
                Surrender(_gladiator, _monster);

                MainLoopCompleted?.Invoke(this, EventArgs.Empty);
                await Task.Delay(20);

                //TODO skicka in rätt attacker/defender i attackklassen
            }
        }

        public void StartGame()
        {
            if (!IsRunning)
            {
                _gladiator = new Gladiator("Forsete", 100, 100, 20);
                _monster = new Monster("Troll", 50, 50, 21);
                MainLoop(_gladiator, _monster);
            }
        }

        public void Round()
        {
            //TODO ska inte vara hårdkodat
            var gladiatorDamage = Attack(_gladiator, _monster);
            var monsterDamage = Attack(_monster, _gladiator);

                string roundInfo = $"{_gladiator.Name} darrar på läppen och ser vettskrämd ut när {_monster.Name} initierar striden." +
                $"{_monster.Name} gör sig nu redo för närstrid." +
                $"{_monster.Name} frustar till och löper fram mot {_gladiator.Name}. {_monster.Name} svingar Fullmånespjut mot {_gladiator.Name} som blir skadad {monsterDamage}." +
                $"{_gladiator.Name} börjar göra sig redo för en attack. {_gladiator.Name} utnyttjar att {_monster.Name} bländas av solen, backar undan " +
                $" {_gladiator.Name} gör sig nu redo för närstrid. {_gladiator.Name} svingar Fullmånespjut mot {_monster.Name} som blir skadad {gladiatorDamage}.";

            _monster.Health -= gladiatorDamage;
            _gladiator.Health -= monsterDamage;

            Rounds.Add(roundInfo);
        }

        public double Attack(Character attacker, Character defender)
        {
            
            defender.SuccessEvasion = false; //TODO : make this a check, now all attacks hits.

            double totalDamage;
            if (!defender.SuccessEvasion)
            {
                totalDamage = (attacker.Strength * 0.2); //+ attacker.Weapon.Damage;
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
