using System.Data.Entity.ModelConfiguration;

namespace Cinematheque.Data.Mapings
{
    public class ActorConfiguration : EntityTypeConfiguration<Actor>
    {
        public ActorConfiguration()
        {
            ToTable("Actor");
            HasKey(a => a.ID);

            Property(a => a.Name).IsRequired();
            Property(a => a.Surname).IsRequired();
        }
    }
}
