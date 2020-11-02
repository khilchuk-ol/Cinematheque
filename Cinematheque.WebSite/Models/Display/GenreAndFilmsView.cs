using Cinematheque.Data;
using System.Collections.Generic;

namespace Cinematheque.WebSite.Models
{
    public class GenreAndFilmsView
    {
        public Genre Genre { get; set; }

        public IEnumerable<FilmView> Films { get; set; }
    }
}