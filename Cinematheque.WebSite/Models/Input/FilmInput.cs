using Cinematheque.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinematheque.WebSite.Models
{
    public class FilmInput : EntityView
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(160)]
        public string Title { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public List<string> Countries { get; set; }

        public List<Guid> Actors { get; set; }

        public Guid DirectorID { get; set; }

        public List<Guid> Genres { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Rating must be between 0 nad 10.")]
        public float IMDbRating { get; set; }

        public string PosterFileName { get; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        public FilmInput() : base() { }
    }
}