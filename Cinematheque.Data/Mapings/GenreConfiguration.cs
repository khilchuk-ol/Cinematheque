using System.Data.Entity.ModelConfiguration;
using Cinematheque.Data.Models;

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
