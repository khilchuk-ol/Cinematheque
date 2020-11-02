using Cinematheque.Data.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinematheque.Data.Dao.Impl
{
    public class GenreDao : AbstractDao<Genre>, IDaoGenre
    {
        public Genre GetGenreWithFilms(Guid id)
        {
            using(var context = new CinemathequeContext())
            {
                return context.Genres
                              .Include(g => g.Films)
                              .FirstOrDefault(global => global.ID == id);
            }
        }

        public List<Genre> SearchGenresByName(string name)
        {
            using (var context = new CinemathequeContext())
            {
                return context.Genres
                              .Where(g => g.Name.Contains(name))
                              .ToList();
            }
        }
    }
}
