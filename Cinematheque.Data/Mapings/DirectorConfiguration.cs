using System.Data.Entity.ModelConfiguration;

namespace Cinematheque.Data.Mapings
{
    public class DirectorConfiguration : EntityTypeConfiguration<Director>
    {
        public DirectorConfiguration()
        {
            ToTable("Director");
            HasKey(d => d.ID);

            Property(d => d.Name).IsRequired();
            Property(d => d.Surname).IsRequired();

            HasMany(d => d.Films)
                .WithRequired(f => f.Director)
                .HasForeignKey(f => f.DirectorID);
        }
    }
}
