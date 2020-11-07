using Cinematheque.Data.Data;
using Cinematheque.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Cinematheque.Data.Dao.Impl
{
    public class FilmDao : AbstractDao<Film>, IDaoFilm
    {
        public FilmDao(CinemathequeContext context) : base(context) { }

        public List<Film> GetFilmsWithoutActor(Guid actorId)
        {
            return Context.Films
                          .Where(f => !f.Actors.Select(a => a.ID).Contains(actorId))
                          .ToList();
        }

        public List<Film> GetFilmsWithoutDirector(Guid diectorId)
        {
            return Context.Films
                          .Where(f => f.DirectorID != diectorId)
                          .ToList();
        }

        public Film GetFilmWithFullInfo(Guid id)
        {
            return Context.Films
                          .Include(f => f.Actors)
                          .Include(f => f.Genres)
                          .Include(f => f.Director)
                          .Include(f => f.Countries)
                          .FirstOrDefault(f => f.ID == id);
        }

        public List<Film> FindAllWithGenres()
        {
            return Context.Films
                          .Include(f => f.Genres)
                          .ToList();
        }

        public List<Film> SearchFilmsByTitle(string title)
        {
            return Context.Films
                          .Where(f => f.Title.Contains(title))
                          .ToList();
        }
    }
}
