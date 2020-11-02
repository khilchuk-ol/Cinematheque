using Cinematheque.Data.DAO;
using System;
using System.Collections.Generic;

namespace Cinematheque.Data.Dao
{
    public interface IDaoActor : IDao<Actor>
    {
        Actor GetActorWithFilms(Guid id);

        List<Actor> SearchActorsByName(string fullname);
    }
}
