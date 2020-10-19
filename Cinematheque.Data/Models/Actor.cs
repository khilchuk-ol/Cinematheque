using Cinematheque.Data.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Cinematheque.Data
{
    public class Actor : Person
    {
        public List<Film> FilmsStared
        {
            get { return DataHolder.ActorsInFilms.Where(aif => aif.Actor.Equals(this)) 
                                                 .Select(aif => aif.Film)
                                                 .OrderBy(f => f.Title)
                                                 .ToList(); }
            private set { }
        }

        public string PhotoFileName { get; set; }

        public string Biography { get; set; }

        public Actor(string name, string sname, DateTime birth, DateTime? death, Gender gender, RegionInfo country, 
            string photoFileName, string biography) :
            base(name, sname, birth, death, gender, country)
        {
            PhotoFileName = photoFileName;
            Biography = biography;

            Validate(this);
        }

        public Actor() : base()
        { }

        public void RemoveAllFilms()
        {
            DataHolder.RemoveAllFilmsOfActor(this);
        }

        public void RemoveFilm(Film f)
        {
            DataHolder.RemoveFilmFromActor(f, this);
        }

        public void AddFilm(Film f)
        {
            Validator.RequireNotNull(f);
            DataHolder.AddFilmToActor(f, this);
        }

        private static void Validate(Actor a)
        {
            Validator.CheckFileExist(PathUtils.GetProjectDirectory() + "Cinematheque.WebSite\\images\\actors\\" + a.PhotoFileName);
        }
    }
}
