using Cinematheque.Data.DAO;
using System;
using System.Collections.Generic;

namespace Cinematheque.Data.Dao
{
    public interface IDaoFilm : IDao<Film>
    {
        Film GetFilmWithActorsAndGenres(Guid id);

        List<Film> SearchFilmsByTitle(string title);
    }
}
