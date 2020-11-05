using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinematheque.WebSite.Models
{
    public class ActorInput : PersonView
    {
        public List<Guid> FilmsStared { get; set; }

        public IEnumerable<string> PhotoFileNames { get; }

        [Required(AllowEmptyStrings = false)]
        public string Biography { get; set; }
        public ActorInput() : base() { }
    }
}