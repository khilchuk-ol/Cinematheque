using System;
using System.Linq;

namespace Cinematheque.Data
{
    public class ActorInFilm : Entity
    {
        private Guid _actorID;

        public Actor Actor
        {
            get { return DataHolder.Actors.Where(a => a.ID == _actorID).FirstOrDefault(); }

            set { _actorID = value.ID; }
        }
 
        private Guid _filmID;

        public Film Film
        {
            get { return DataHolder.Films.Where(f => f.ID == _filmID).FirstOrDefault(); }
            
            set { _filmID = value.ID; }
        }
    }
}
