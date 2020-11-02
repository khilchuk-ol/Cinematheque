using Cinematheque.Data.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinematheque.Data.Dao.Impl
{
    public class DirectorDao : AbstractDao<Director>, IDaoDirector
    {
        public Director GetDirectorAndFilms(Guid id)
        {
            using(var context = new CinemathequeContext())
            {
                return context.Directors
                              .Include(d => d.Films)
                              .FirstOrDefault(d => d.ID == id);
            }
        }

        public List<Director> SearchDirectorsByName(string fullname)
        {
            using (var context = new CinemathequeContext())
            {
                return context.Directors
                              .Where(d => d.GetFullName().Contains(fullname))
                              .ToList();
            }
        }
    }
}
