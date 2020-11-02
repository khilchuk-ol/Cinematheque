using Cinematheque.Data.DAO;
using System;
using System.Collections.Generic;

namespace Cinematheque.Data.Dao
{
    public interface IDaoDirector : IDao<Director>
    {
        Director GetDirectorAndFilms(Guid id);

        List<Director> SearchDirectorsByName(string fullname);
    }
}
