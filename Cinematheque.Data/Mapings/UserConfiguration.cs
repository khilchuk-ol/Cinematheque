using Cinematheque.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace Cinematheque.Data.Mapings
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("User");
            HasKey(u => u.ID);

            Property(u => u.Login).IsRequired();
            Property(u => u.Email).IsRequired();
            Property(u => u.Password).IsRequired();

            HasMany(u => u.FavFilms)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("UserFilm");
                    m.MapLeftKey("UserID");
                    m.MapRightKey("FilmID");
                });
        }
    }
}
