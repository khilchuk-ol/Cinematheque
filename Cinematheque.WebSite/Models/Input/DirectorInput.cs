using Cinematheque.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinematheque.WebSite.Models
{
    public class DirectorInput : PersonView
    {
        public List<Guid> FilmsDirected { get; set; }

        public string PhotoFileName { get; }

        [Required(AllowEmptyStrings = false)]
        public string Biography { get; set; }
        public DirectorInput() : base() { }
    }
}