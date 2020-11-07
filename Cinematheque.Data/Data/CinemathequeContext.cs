using Cinematheque.Data.Mapings;
using Cinematheque.Data.Models;
using System.Data.Entity;

namespace Cinematheque.Data.Data
{
    public class CinemathequeContext : DbContext
    {
        public CinemathequeContext() : base("Cinematheque")
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CinemathequeContext>());
            Database.SetInitializer(new CinemathequeDatabaseInitializer());
        }

        public DbSet<Film> Films { get; set; }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<Genre> Genres { get; set; } //=> Set<Genre>();

        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FilmConfiguration());
            modelBuilder.Configurations.Add(new ActorConfiguration());
            modelBuilder.Configurations.Add(new DirectorConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
            modelBuilder.Configurations.Add(new CountryConfiguration());
        }
    }
}
