using System;

namespace Cinematheque.Data
{
    public class Entity
    {
        public Guid ID { get;  private set; }

        public Entity()
        {
            ID = Guid.NewGuid();
        }
    }
}
