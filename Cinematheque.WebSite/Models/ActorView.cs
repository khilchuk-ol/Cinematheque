﻿using Cinematheque.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Cinematheque.WebSite.Models
{
    public class ActorView : PersonView
    {
        [Display(Name = "Stared in films ")]
        public Dictionary<Guid, string> FilmsStared { get; set; }

        public IEnumerable<string> PhotoFileNames { get; }

        [Required(AllowEmptyStrings = false)]
        public string Biography { get; set; }

        public ActorView(Actor data) : base(data)
        {
            FilmsStared = data.FilmsStared.ToDictionary(f => f.ID, f => f.Title);
            PhotoFileNames = data.PhotoFileNames;
            Biography = data.Biography;
        }

        public ActorView() : base()
        {

        }

        public static List<ActorView> GetActorViews(List<Actor> data)
        {
            return data.Select(a => new ActorView(a)).ToList();
        }
    }
}