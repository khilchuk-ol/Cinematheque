using System.Collections.Generic;

namespace Cinematheque.Data.Models
{
    public class User : Entity
    {
        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<Film> FavFilms { get; set; }
    }
}
