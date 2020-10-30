using Cinematheque.Data.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cinematheque.Data
{
    public class Genre : Entity<Genre>, IEquatable<Genre>
    {
        public string Name { get; set; }

        public IEnumerable<Film> FilmsOfGenre
        {
            get { return DataHolder.GenresOfFilms.Where(gof => gof.Genre.Equals(this))
                                                 .Select(gof => gof.Film)
                                                 .ToList(); }

            private set { }
        }

        public Genre(string name) : base()
        {
            Name = name;

            Validate(this);
        }

        public void AddFilm(Film f)
        {
            DataHolder.AddFilmToGenre(f, this);
        }

        public void RemoveFilm(Film f)
        {
            DataHolder.RemoveFilmFromGenre(f, this);
        }

        public void RemoveAllFilms()
        {
            DataHolder.RemoveAllFilmsOfGenre(this);
        }

        private static void Validate(Genre g)
        {
            if (!Validator.IsAlphabetic(g.Name))
            {
                throw new Exception("Name is not valid");
            }
        }

        public bool Equals(Genre other)
        {
            return Name.Equals(other.Name);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
