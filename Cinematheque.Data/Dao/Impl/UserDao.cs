using Cinematheque.Data.Data;
using Cinematheque.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Cinematheque.Data.Dao.Impl
{
    public class UserDao : AbstractDao<User>, IDaoUser
    {
        public UserDao(CinemathequeContext context) : base(context) { }

        public List<User> FindAllWithFilms()
        {
            return Context.Users.Include(u => u.FavFilms).ToList();
        }

        public User GetUserWithFilms(Guid id)
        {
            throw new NotImplementedException();
            //return Context.Users.Where(u => u.ID == id).Include(u => u.FavFilms).FirstOrDefault();
        }
    }
}
