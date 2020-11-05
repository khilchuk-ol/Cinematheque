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
            var film = Context.Films
                              .Where(f => f.ID == filmId)
                              .FirstOrDefault();
            return Context.Actors
                          .Where(a => !a.Films.Contains(film))
                          .ToList();
        }

        public Actor GetActorWithFilms(Guid id)
        {
            return Context.Actors
                          .Include(a => a.Films)
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
