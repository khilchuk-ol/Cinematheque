using Cinematheque.Data.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Cinematheque.Data
{
    public class Director : Person<Director>
    {
        public List<Film> FilmsDirected 
        {
            get { 
                return DataHolder.Films.Where(f => f.Director.Equals(this))
                                       .OrderBy(f => f.Title)
                                       .ToList(); 
            }
        }

        public string PhotoFileName { get; set; }

        public string Biography { get; set; }

        public Director(string name, string sname, DateTime birth, DateTime? death, Gender gender, RegionInfo country, 
            string photoFileName, string biography) : 
            base(name, sname, birth, death, gender, country)
        {
            PhotoFileName = photoFileName;
            Biography = biography;

            Validate(this);
        }

        public Director() : base()
        { }

        public void AddFilm(Film f)
        {
            Validator.RequireNotNull(f);

            f.Director = this;
        }

        public void RemoveFilm(Film f)
        {
            if(!IsDirectorOfFilm(f))
            {
                return;
            }

            f.RemoveDirector();
        }

        public void RemoveAllFilms()
        {
            foreach(var f in FilmsDirected)
            {
                f.RemoveDirector();
            }
        }

        private bool IsDirectorOfFilm(Film f)
        {
            return f.Director.Equals(this);
        }

        private static void Validate(Director d)
        {
            Validator.CheckFileExist(PathUtils.GetProjectDirectory() + "Cinematheque.WebSite\\images\\directors\\" + d.PhotoFileName);
        }
    }
}
