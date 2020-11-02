using Cinematheque.Data;
using System;

namespace Cinematheque.WebSite.Models
{
    public class EntityView
    {
        public Guid ID { get; set; }

        public EntityView(Entity data)
        {
            ID = data.ID;
        }

        public EntityView() {}
    }
}