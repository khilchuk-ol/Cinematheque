using Cinematheque.Data.DAO;
using System;
using System.Collections.Generic;

namespace Cinematheque.Data.Dao
{
    public interface IDaoGenre : IDao<Genre>
    {
        Genre GetGenreWithFilms(Guid id);

        List<Genre> SearchGenresByName(string name);
    }
}
