using Cinematheque.Utils;
using System.Collections.Generic;

namespace Cinematheque.Data
{
    public class Actor : Person
    {
        public List<Film> Films { get; set; }

        public string PhotoFileName { get; set; }

        public string Biography { get; set; }

        public Actor() : base()
        {
            Films = new List<Film>();

            //Validate(this);
        }

        public void RemoveAllFilms()
        {
            Films.Clear();
        }

        public void RemoveFilm(Film f)
        {
            Films.Remove(f);
        }

        public void AddFilm(Film f)
        {
            Validator.RequireNotNull(f);
            Films.Add(f);
        }

        private static void Validate(Actor a)
        {
            Validator.CheckFileExist(PathUtils.GetProjectDirectory() + "Cinematheque.WebSite\\images\\actors\\" + a.PhotoFileName);
        }
    }
}
