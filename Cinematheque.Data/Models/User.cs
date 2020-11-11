using System.Collections.Generic;
using System.Linq;

namespace Cinematheque.Data.Models
{
    public class User : Entity
    {
        public string Username { get; set; }

        //public string Email { get; set; }

        //public string Password { get; set; }

        public List<Film> FavFilms { get; set; }

        public User()
        {
            Username = "Not set";
            FavFilms = new List<Film>();
        }

        public bool HasFavourite(Film film)
        {
            return FavFilms.FirstOrDefault(f => f.ID == film.ID) != null;
        }
    }
}
