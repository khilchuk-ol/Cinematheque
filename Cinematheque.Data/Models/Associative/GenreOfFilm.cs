using System;
using System.Linq;

namespace Cinematheque.Data
{
    public class GenreOfFilm : Entity
    {
        public Guid GenreID { get; set; }

        public Genre Genre { get; set; }

        public Guid FilmID { get; set; }
        
        public Film Film { get; set; }
    }
}
