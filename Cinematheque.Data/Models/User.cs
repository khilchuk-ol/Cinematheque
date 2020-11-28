using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Cinematheque.Data.Models
{
    [DataContract]
    public class User : Entity
    {
        [DataMember]
        public string Username { get; set; }

        //public string Email { get; set; }

        //public string Password { get; set; }

        [DataMember]
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
