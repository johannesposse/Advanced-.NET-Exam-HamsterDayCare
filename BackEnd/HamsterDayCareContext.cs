using Microsoft.EntityFrameworkCore;
using System;

namespace BackEnd
{
    public class HamsterDayCareContext : DbContext
    {

        public virtual DbSet<Hamster> Hamsters { get; set; }
        public virtual DbSet<Cage> Cages { get; set; }
        public virtual DbSet<ExerciseArea> ExerciseArea { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dBContextbuilder)
        {
            dBContextbuilder.UseSqlServer(@"Server=.\SQLExpress;Database=advJohannesPosse;Trusted_Connection=True;").UseLazyLoadingProxies();
        }


        public void AddHamster()
        {
            var x = new Hamster("Evan", "Lassa", 22, true, DateTime.Now, DateTime.Now);
            Hamsters.Add(x);
            SaveChanges();
        }
    }
}