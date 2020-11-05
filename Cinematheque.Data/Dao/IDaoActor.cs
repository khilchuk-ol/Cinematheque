using Cinematheque.Data.DAO;
using Cinematheque.Data.Models;
using System;
using System.Collections.Generic;

namespace Cinematheque.Data.Dao
{
    public interface IDaoActor : IDao<Actor>
    {
        Actor GetActorWithFilms(Guid id);

        List<Actor> SearchActorsByName(string fullname);

        List<Actor> GetActorsWithoutFilm(Guid filmId);
    }
}
