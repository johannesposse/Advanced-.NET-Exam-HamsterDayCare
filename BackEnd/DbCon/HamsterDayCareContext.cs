using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class HamsterDayCareContext : DbContext
    {
        public virtual DbSet<Hamster> Hamsters { get; set; }
        public virtual DbSet<Cage> Cages { get; set; }
        public virtual DbSet<ExerciseArea> ExerciseArea { get; set; }
        public virtual DbSet<ActivityLog> ActivityLogs { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder dBContextbuilder)
        {
            dBContextbuilder.UseSqlServer(@"Server=.\SQLExpress;Database=advJohannesPosse;Trusted_Connection=True;MultipleActiveResultSets=True;").UseLazyLoadingProxies();
            
        }

        public HamsterDayCareContext()
        {
            this.Database.EnsureCreated();
        }
    }
}