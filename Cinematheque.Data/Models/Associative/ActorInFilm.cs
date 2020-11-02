using System;
using System.Linq;

namespace Cinematheque.Data
{
    public class ActorInFilm : Entity
    {
        public Guid ActorID { get; set; }

        public Actor Actor { get; set; }
 
        public Guid FilmID { get; set; }

        public Film Film { get; set; }
    }
}
