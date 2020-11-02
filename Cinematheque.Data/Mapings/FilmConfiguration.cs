using System.Data.Entity.ModelConfiguration;
using System.Security.Cryptography;

namespace Cinematheque.Data.Mapings
{
    public class FilmConfiguration : EntityTypeConfiguration<Film>
    {
        public FilmConfiguration()
        {
            ToTable("Film");
            HasKey(f => f.ID);

            Property(f => f.Title).IsRequired();

            HasMany(f => f.Actors)
                .WithMany(a => a.Films)
                .Map(m =>
                {
                    m.ToTable("FilmActor");
                    m.MapLeftKey("FilmID");
                    m.MapRightKey("ActorID");
                });

            HasMany(f => f.Genres)
                .WithMany(g => g.Films)
                .Map(m =>
                {
                    m.ToTable("FilmGenre");
                    m.MapLeftKey("FilmID");
                    m.MapRightKey("GenreID");
                });
        }
    }
}
