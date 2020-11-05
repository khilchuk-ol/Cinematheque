using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinematheque.WebSite.Models
{
    public class GenreInput : EntityView
    {
        [Required()]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", 
            ErrorMessage = "Name is not suitable for genre. Must contain only alphabetical characters")]
        public string Name { get; set; }

        public List<Guid> FilmsOfGenre { get; set; }

        public GenreInput() : base() { }
    }
}