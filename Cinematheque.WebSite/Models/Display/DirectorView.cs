using Cinematheque.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Cinematheque.WebSite.Models
{
    public class DirectorView : PersonView
    {
        [Display(Name = "Directed films")]
        public Dictionary<Guid, string> FilmsDirected { get; set; }

        public string PhotoFileName { get; }

        [Required(AllowEmptyStrings = false)]
        public string Biography { get; set; }

        public DirectorView(Director data) : base(data)
        {
            FilmsDirected = data.Films.ToDictionary(f => f.ID, f => f.Title);
            PhotoFileName = data.PhotoFileName;
            Biography = data.Biography;
        }

        public DirectorView() : base() { }

        public static List<DirectorView> GetDirectorViews(List<Director> data)
        {
            return data.Select(d => new DirectorView(d)).ToList();
        }
    }
}