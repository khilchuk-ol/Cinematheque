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
                                                 .ToList(); }
            private set { }
        }

        public IEnumerable<string> PhotoFileNames { get; private set; }

        public string Biography { get; set; }

        public Actor(string name, string sname, DateTime birth, DateTime? death, Gender gender, RegionInfo country, 
            IEnumerable<string> photoFileNames, string biography) :
            base(name, sname, birth, death, gender, country)
        {
            PhotoFileNames = photoFileNames;
            Biography = biography;

            Validate(this);
        }

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
            DataHolder.AddFilmToActor(f, this);
        }

        public void AddPhoto(string fileName)
        {
            var path = PathUtils.GetProjectDirectory() + "data\\actors\\photos\\" + fileName;
            Validator.CheckFileExist(path);

            PhotoFileNames.Append(fileName);
        }

        private static void Validate(Actor a)
        {
            foreach (var fn in a.PhotoFileNames)
            {
                Validator.CheckFileExist(PathUtils.GetProjectDirectory() + "Cinematheque.WebSite\\images\\actors\\" + fn);
            }
        }
    }
}
