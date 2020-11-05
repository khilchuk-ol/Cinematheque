using Cinematheque.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace Cinematheque.Data.Mapings
{
    public class CountryConfiguration : EntityTypeConfiguration<Country>
    {
        public CountryConfiguration()
        {
            ToTable("Country");
            HasKey(c => c.ID);

            Property(c => c.Name).IsRequired();
        }
    }
}
