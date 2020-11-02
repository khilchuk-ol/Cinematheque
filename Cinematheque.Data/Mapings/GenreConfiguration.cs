using System.Data.Entity.ModelConfiguration;

namespace Cinematheque.Data.Mapings
{
    public class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            ToTable("Genre");
            HasKey(g => g.ID);

            Property(g => g.Name).IsRequired();
        }
    }
}
