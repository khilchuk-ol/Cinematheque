using Cinematheque.Data.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Cinematheque.Data
{
    public class Film : Entity
    {
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public List<RegionInfo> Country { get; set; }
        
        public List<Actor> Actors
        {
            get 
            {
                var aifs = DataHolder.ActorsInFilms.Where(aif => aif.Film.Equals(this)).ToList();
                var result= aifs.Select(aif => aif.Actor)     .ToList();
                return result;
            }
        }

        private Guid _directorID;

        public Director Director
        {
            get { return _directorID.Equals(Guid.Empty) ? 
                    null : DataHolder.Directors.Where(d => d.ID == _directorID).FirstOrDefault(); }

            set { _directorID = value.ID; }
        }

        public List<Genre> Genres
        {
            get { return DataHolder.GenresOfFilms.Where(gof => gof.Film.Equals(this))
                                                 .Select(gof => gof.Genre)
                                                 .ToList(); }
            private set { }
        }

        public TimeSpan Duration { get; set; }

        public float IMDbRating { get; set; }

        public string PosterFileName { get; set; }

        public string Description { get; set; }
        
        public Film(string title, DateTime relese, List<RegionInfo> countries, TimeSpan duration, float rating, 
            string posterFileName, string description) : base()
        {
            Title = title;
            ReleaseDate = relese;
            Country = countries ?? new List<RegionInfo>();
            Duration = duration;
            IMDbRating = rating;
            PosterFileName = posterFileName;
            Description = description;

            Validate(this);
        }

        public override string ToString()
        {
            return Title + " (" + ReleaseDate.Year + ") ";
        }

        public void RemoveActor(Actor a)
        {
            Validator.RequireNotNull(a);
            DataHolder.RemoveActorFromFilm(a, this);
        }

        public void RemoveGenre(Genre g)
        {
            Validator.RequireNotNull(g);
            DataHolder.RemoveGenreFromFilm(g, this);
        }

        public void RemoveAllActors()
        {
            DataHolder.RemoveAllActorsOfFilm(this);
        }

        public void RemoveAllGenres()
        {
            DataHolder.RemoveAllGenresOfFilm(this);
        }

        public void AddGenre(Genre genre)
        {
            Validator.RequireNotNull(genre);
            DataHolder.AddGenreToFilm(genre, this);
        }

        public void AddActor(Actor actor)
        {
            Validator.RequireNotNull(actor);
            DataHolder.AddActorToFilm(actor, this);
        }


        public void RemoveDirector()
        {
            _directorID = Guid.Empty;
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
