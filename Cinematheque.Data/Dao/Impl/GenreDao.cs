﻿using Cinematheque.Data.Data;
using Cinematheque.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Cinematheque.Data.Dao.Impl
{
    public class GenreDao : AbstractDao<Genre>, IDaoGenre
    {
        public GenreDao(CinemathequeContext context) : base(context) { }

        public List<Genre> GetGenresWithoutFilm(Guid filmId)
        {
            return Context.Genres
                          .Where(g => !g.Films.Select(f => f.ID).Contains(filmId))
                          .ToList();
        }

        public Genre GetGenreWithFilms(Guid id)
        {
            return Context.Genres
                          .Include(g => g.Films)
                          .FirstOrDefault(global => global.ID == id);
        }

        public List<Genre> SearchGenresByName(string name)
        {
            return Context.Genres
                          .Where(g => g.Name.Contains(name))
                          .ToList();
        }
    }
}
