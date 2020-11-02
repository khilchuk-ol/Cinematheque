using Cinematheque.Utils;
using System.Collections.Generic;

namespace Cinematheque.Data
{
    public class Director : Person
    {
        public List<Film> Films { get; set; }

        public string PhotoFileName { get; set; }

        public string Biography { get; set; }

        public Director() : base()
        {
            Films = new List<Film>();

            //Validate(this);
        }

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
            foreach(var f in Films)
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
