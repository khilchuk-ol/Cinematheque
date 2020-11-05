using Cinematheque.Data.DAO;
using Cinematheque.Data.Models;
using System;
using System.Collections.Generic;

namespace Cinematheque.Data.Dao
{
    public interface IDaoGenre : IDao<Genre>
    {
        Genre GetGenreWithFilms(Guid id);

        List<Genre> SearchGenresByName(string name);

        List<Genre> GetGenresWithoutFilm(Guid filmId);
    }
}
