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
            var actor = Context.Actors
                               .Where(a => a.ID == actorId)
                               .FirstOrDefault();

            return Context.Films
                          .Where(f => !f.Actors.Contains(actor))
                          .ToList();
        }

        public List<Film> GetFilmsWithoutDirector(Guid diectorId)
        {
            return Context.Films
                          .Where(f => f.DirectorID != diectorId)
                          .ToList();
        }

        public Film GetFilmWithActorsAndGenres(Guid id)
        {
            return Context.Films
                          .Include(f => f.Actors)
                          .Include(f => f.Genres)
                          .FirstOrDefault(f => f.ID == id);
        }

        public List<Film> SearchFilmsByTitle(string title)
        {
            return Context.Films
                          .Where(f => f.Title.Contains(title))
                          .ToList();
        }
    }
}
