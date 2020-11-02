using Cinematheque.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Cinematheque.Data
{
    public class Film : Entity
    {
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public List<RegionInfo> Countries { get; set; }
        
        public List<Actor> Actors { get; set; }

        public Guid DirectorID { get; set; }

        public Director Director { get; set; }

        public List<Genre> Genres { get; set; }

        public TimeSpan Duration { get; set; }

        public float IMDbRating { get; set; }

        public string PosterFileName { get; set; }

        public string Description { get; set; }

        public Film() : base()
        {
            Countries = new List<RegionInfo>();
            Actors = new List<Actor>();
            Genres = new List<Genre>();

            //Validate(this);
        }

        public void RemoveActor(Actor a)
        {
            Validator.RequireNotNull(a);
            Actors.Remove(a);
        }

        public void RemoveGenre(Genre g)
        {
            Validator.RequireNotNull(g);
            Genres.Remove(g);
        }

        public void RemoveAllActors()
        {
            Actors.Clear();
        }

        public void RemoveAllGenres()
        {
            Genres.Clear();
        }

        public void AddGenre(Genre g)
        {
            Validator.RequireNotNull(g);
            Genres.Add(g);
        }

        public void AddActor(Actor a)
        {
            Validator.RequireNotNull(a);
            Actors.Add(a);
        }


        public void RemoveDirector()
        {
            DirectorID = Guid.Empty;
        }

        private static void Validate(Film f)
        {
            Validator.RequireNotNull(f);

            if (!Validator.IsDatePast(f.ReleaseDate))
            {
                throw new Exception("Release date is not valid");
            }

            if (f.IMDbRating <= 0 || f.IMDbRating > 10)
            {
                throw new Exception("Rating is not valid");
            }

            Validator.CheckFileExist(PathUtils.GetProjectDirectory() + "Cinematheque.WebSite\\images\\films\\" + f.PosterFileName);
        }
    }
}
