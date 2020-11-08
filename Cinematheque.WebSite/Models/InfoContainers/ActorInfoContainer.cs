using Cinematheque.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Cinematheque.WebSite.Models.InfoContainers
{
    public class ActorInfoContainer
    {
        public ActorView Actor { get; set; }

        private IEnumerable<Film> films;

        public IEnumerable<Country> Countries { get; set; }

        public IEnumerable<Film> AvailableFilms
        {
            get { return films.OrderBy(f => f.Title); }
            set { films = value; }
        }
    }
}