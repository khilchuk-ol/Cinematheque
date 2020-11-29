using Cinematheque.Data.Data;
using Cinematheque.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Cinematheque.Data.Dao.Impl
{
    public class DirectorDao : AbstractDao<Director>, IDaoDirector
    {
        public DirectorDao(CinemathequeContext context) : base(context) { }

        public Director GetDirectorAndFilms(Guid id)
        {
            return Context.Directors
                          .Include(d => d.Films)
                          .Include(d => d.Country)
                          .FirstOrDefault(d => d.ID == id);
        }

        public List<Director> GetDirectorsWithoutFilm(Guid filmId)
        {
            return Context.Directors
                          .Where(d => !d.Films.Select(f => f.ID).Contains(filmId))
                          .ToList();
        }

        public List<Director> SearchDirectorsByName(string fullname)
        {
            return Context.Directors
                          .Where(d => (d.Name + d.Surname).Contains(fullname))
                          .ToList();
        }
    }
}
