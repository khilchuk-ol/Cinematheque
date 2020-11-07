using Cinematheque.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Cinematheque.WebSite.Models.InfoContainers
{
    public class FilmInfoContainer
    {
        public FilmView Film { get; set; }

        private IEnumerable<Actor> actors;

        public IEnumerable<Actor> AvailableActors 
        {
            get { return actors.OrderBy(a => a.GetFullName()); }
            set { actors = value; } 
        }

        private IEnumerable<Director> directors;

        public IEnumerable<Director> AvailableDirectors
        {
            get { return directors.OrderBy(d => d.GetFullName()); }
            set { directors = value; }
        }

        private IEnumerable<Genre> genres;

        public IEnumerable<Genre> AvailableGenres
        {
            get { return genres.OrderBy(g => g.Name); }
            set { genres = value; }
        }
    }
}