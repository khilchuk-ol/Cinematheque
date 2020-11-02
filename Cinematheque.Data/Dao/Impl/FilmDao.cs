using Cinematheque.Data.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinematheque.Data.Dao.Impl
{
    public class FilmDao : AbstractDao<Film>, IDaoFilm
    {
        public Film GetFilmWithActorsAndGenres(Guid id)
        {
            using(var context = new CinemathequeContext())
            {
                return context.Films
                              .Include(f => f.Actors)
                              .Include(f => f.Genres)
                              .FirstOrDefault(f => f.ID == id);
            }
        }

        public List<Film> SearchFilmsByTitle(string title)
        {
           using(var context = new CinemathequeContext())
            {
                return context.Films
                              .Where(f => f.Title.Contains(title))
                              .ToList();
            }
        }
    }
}
