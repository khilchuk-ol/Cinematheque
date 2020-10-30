using Cinematheque.Data;
using System;

namespace Cinematheque.WebSite.Models
{
    public class EntityView<T> where T : class
    {
        public Guid ID { get; set; }

        public EntityView(Entity<T> data)
        {
            ID = data.ID;
        }

        public EntityView()
        {

        }
    }
}