using Cinematheque.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Cinematheque.WebSite.Models.InfoContainers
{
    public class GenreInfoContainer
    {
        public Genre Genre { get; set; }

        private IEnumerable<Film> films;

        public IEnumerable<Film> AvailableFilms
        {
            get { return films.OrderBy(f => f.Title); }
            set { films = value; }
        }
    }
}