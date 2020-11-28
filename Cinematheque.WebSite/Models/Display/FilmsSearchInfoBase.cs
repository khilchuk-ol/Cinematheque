using Cinematheque.Data.Models;
using System.Collections.Generic;

namespace Cinematheque.WebSite.Models.Display
{
    public class FilmsSearchInfoBase
    {
        public IEnumerable<ActorView> Actors { get; set; }

        public IEnumerable<Genre> Genres { get; set; }


        public FilmsSearchInfoBase()
        {
            Actors = new List<ActorView>();
            Genres = new List<Genre>();
        }
    }
}