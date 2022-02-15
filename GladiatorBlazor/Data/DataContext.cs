using GladiatorBlazor.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladiatorBlazor.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Gladiator> Gladiators { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Armor> Armors { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        //TODO ska man istället ta med Charecter istället för monster och gladiator?
    }
}
