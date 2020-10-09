using Cinematheque.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinematheque.WebSite.Models
{
    public class GenreAndFilmsView
    {
        public Genre Genre { get; set; }

        public IEnumerable<FilmView> Films { get; set; }
    }
}