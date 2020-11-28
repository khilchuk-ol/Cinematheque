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
            if(string.IsNullOrWhiteSpace(title))
            {
                return Context.Films.ToList();
            }

            return Context.Films
                          .Where(f => f.Title.Contains(title))
                          .ToList();
        }

        public List<Film> SearchFilmsWithSettings(FilmsSearchSettings fss)
        {
            var query = string.IsNullOrWhiteSpace(fss.Query) ?
                           Context.Films.AsQueryable() :
                           Context.Films.Where(f => f.Title.Contains(fss.Query));

            if(fss.IncludeActorsIDs.Count() != 0)
            {
                query = query.Where(f => f.Actors.Any(a => fss.IncludeActorsIDs.Contains(a.ID)));
            }

            if (fss.IncludeGenresIDs.Count() != 0)
            {
                query = query.Where(f => f.Genres.Any(g => fss.IncludeGenresIDs.Contains(g.ID)));
            }

            if (fss.ExcludeActorsIDs.Count() != 0)
            {
                query = query.Where(f => f.Actors.All(a => !fss.ExcludeActorsIDs.Contains(a.ID)));
            }

            if (fss.ExcludeGenresIDs.Count() != 0)
            {
                query = query.Where(f => f.Genres.All(g => !fss.ExcludeGenresIDs.Contains(g.ID)));
            }

            return query.ToList();
        }
    }
}
