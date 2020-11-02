using Cinematheque.Data.Mapings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinematheque.Data.Data
{
    public class CinemathequeContext : DbContext
    {
        public CinemathequeContext() : base("Cinematheque")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CinemathequeContext>());
        }

        public DbSet<Film> Films { get; set; }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<Genre> Genres { get; set; } //=> Set<Genre>();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FilmConfiguration());
            modelBuilder.Configurations.Add(new ActorConfiguration());
            modelBuilder.Configurations.Add(new DirectorConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
        }
    }
}
