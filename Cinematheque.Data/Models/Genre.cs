using Cinematheque.Utils;
using System;
using System.Collections.Generic;

namespace Cinematheque.Data.Models
{
    public class Genre : Entity
    {
        public string Name { get; set; }

        public List<Film> Films { get; set; }

        public Genre() : base()
        {
            Films = new List<Film>();
        }

        public void AddFilm(Film f)
        {
            Validator.RequireNotNull(f);
            Films.Add(f);
        }

        public void RemoveFilm(Film f)
        {
            Validator.RequireNotNull(f);
            Films.Remove(f);
        }

        public void RemoveAllFilms()
        {
            Films.Clear();
        }

        /*private static void Validate(Genre g)
        {
            if (!Validator.IsAlphabetic(g.Name))
            {
                throw new Exception("Name is not valid");
            }
        }
        */

        public override string ToString()
        {
            return Name;
        }
    }
}
