using Cinematheque.Data.DAO;
using Cinematheque.Data.Models;
using System;
using System.Collections.Generic;

namespace Cinematheque.Data.Dao
{
    public interface IDaoDirector : IDao<Director>
    {
        Director GetDirectorAndFilms(Guid id);

        List<Director> SearchDirectorsByName(string fullname);

        List<Director> GetDirectorsWithoutFilm(Guid filmId);
    }
}
