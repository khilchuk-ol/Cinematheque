using System;
using System.Collections.Generic;

namespace Cinematheque.Data.Models
{
    public class FilmsSearchSettings
    {
        public string Query { get; set; }
        public IEnumerable<Guid> IncludeActorsIDs { get; set; }

        public IEnumerable<Guid> ExcludeActorsIDs { get; set; }

        public IEnumerable<Guid> IncludeGenresIDs { get; set; }

        public IEnumerable<Guid> ExcludeGenresIDs { get; set; }

        public FilmsSearchSettings()
        {
            IncludeActorsIDs = new List<Guid>();
            ExcludeActorsIDs = new List<Guid>();
            IncludeGenresIDs = new List<Guid>();
            ExcludeGenresIDs = new List<Guid>();
        }
    }
}
