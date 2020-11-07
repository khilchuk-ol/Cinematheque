using Cinematheque.Data.Data;
using Cinematheque.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Cinematheque.Data.Dao.Impl
{
    public class ActorDao : AbstractDao<Actor>, IDaoActor
    {
        public ActorDao(CinemathequeContext context) : base(context) { }

        public List<Actor> GetActorsWithoutFilm(Guid filmId)
        {
            return Context.Actors
                          .Where(a => !a.Films.Select(f => f.ID).Contains(filmId))
                          .ToList();
        }

        public Actor GetActorWithFilms(Guid id)
        {
            return Context.Actors
                          .Include(a => a.Films)
                          .Include(a => a.Country)
                          .FirstOrDefault(a => a.ID == id);
        }

        public List<Actor> SearchActorsByName(string fullname)
        {
            return Context.Actors
                          .Where(a => a.GetFullName().Contains(fullname))
                          .ToList();
        }
    }
}
