using Cinematheque.Data.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinematheque.Data.Dao.Impl
{
    public class ActorDao : AbstractDao<Actor>, IDaoActor
    {
        public Actor GetActorWithFilms(Guid id)
        {
            using (var context = new CinemathequeContext())
            {
                return context.Actors
                              .Include(a => a.Films)
                              .FirstOrDefault(a => a.ID == id);
            }
        }

        public List<Actor> SearchActorsByName(string fullname)
        {
            using (var context = new CinemathequeContext())
            {
                return context.Actors
                              .Where(a => a.GetFullName().Contains(fullname))
                              .ToList();
            }
        }
    }
}
