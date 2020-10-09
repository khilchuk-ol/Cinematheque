using System;
using System.Linq;

namespace Cinematheque.Data
{
    public class GenreOfFilm : Entity
    {
        private Guid _genreID;

        public Genre Genre
        {
            get { return DataHolder.Genres.Where(g => g.ID == _genreID).FirstOrDefault(); }

            set { _genreID = value.ID; }
        }

        private Guid _filmID;

        public Film Film
        {
            get { return DataHolder.Films.Where(f => f.ID == _filmID).FirstOrDefault(); }

            set { _filmID = value.ID; }
        }
    }
}
