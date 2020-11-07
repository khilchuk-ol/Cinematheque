using Cinematheque.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Cinematheque.WebSite.Models
{
    public class FilmView : EntityView
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(160)]
        public string Title { get; set; }

        [Display(Name = "Release date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public List<string> Countries { get; set; }

        public string CountryString
        {
            get { return string.Join(", ", Countries); }
        }

        public Dictionary<Guid, string> Actors { get; set; }

        public Guid? DirectorID { get; set; }

        [Display(Name = "Director")]
        public string DirectorName { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public string GenresString
        {
            get { return string.Join(", ", Genres); }
        }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage ="Rating must be between 0 nad 10.")]
        public float IMDbRating { get; set; }

        [Display(Name = "Poster")]
        public string PosterFileName { get; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        public FilmView(Film data) : base(data)
        {
            Title = data.Title;
            ReleaseDate = data.ReleaseDate;
            Countries = data.Countries?.Select(ri => ri.Name).ToList();
            Actors = data.Actors.ToDictionary(a => a.ID, a => a.GetFullName());
            DirectorID = data.Director?.ID;
            DirectorName = data.Director?.GetFullName();
            Genres = data.Genres;
            Duration = data.Duration;
            IMDbRating = data.IMDbRating;
            PosterFileName = data.PosterFileName;
            Description = data.Description;
        }

        public FilmView() : base() { }

        public static List<FilmView> GetFilmViews(List<Film> data)
        {
            return data.Select(f => new FilmView(f)).ToList();
        }
    }
}