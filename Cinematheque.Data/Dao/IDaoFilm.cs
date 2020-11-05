using Cinematheque.Data.DAO;
using Cinematheque.Data.Models;
using System;
using System.Collections.Generic;

namespace Cinematheque.Data.Dao
{
    public interface IDaoFilm : IDao<Film>
    {
        Film GetFilmWithActorsAndGenres(Guid id);

        List<Film> SearchFilmsByTitle(string title);

        List<Film> GetFilmsWithoutDirector(Guid diectorId);

        List<Film> GetFilmsWithoutActor(Guid actorId);
    }
}
