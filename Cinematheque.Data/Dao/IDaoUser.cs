using Cinematheque.Data.DAO;
using Cinematheque.Data.Models;
using System;
using System.Collections.Generic;

namespace Cinematheque.Data.Dao
{
    public interface IDaoUser : IDao<User>
    {
        List<User> FindAllWithFilms();

        User GetUserWithFilms(Guid id);
    }
}
