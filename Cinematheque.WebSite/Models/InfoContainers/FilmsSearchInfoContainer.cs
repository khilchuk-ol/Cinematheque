using System;
using System.Collections.Generic;

namespace Cinematheque.WebSite.Models.InfoContainers
{
    public class FilmsSearchInfoContainer
    {
        public string Query { get; set; }
        public IEnumerable<Guid> IncludeActors { get; set; }

        public IEnumerable<Guid> ExcludeActors { get; set; }

        public IEnumerable<Guid> IncludeGenres { get; set; }

        public IEnumerable<Guid> ExcludeGenres { get; set; }

        public FilmsSearchInfoContainer()
        {
            IncludeActors = new List<Guid>();
            ExcludeActors = new List<Guid>();
            IncludeGenres = new List<Guid>();
            ExcludeGenres = new List<Guid>();
        }
    }
}