using Cinematheque.Data.DAO;
using Cinematheque.Data.Models;
using System.Collections.Generic;

namespace Cinematheque.Data.Dao
{
    public interface IDaoCountry : IDao<Country>
    {
        Country GetCountryByEnglishName(string name);

        HashSet<string> GetNames();
    }
}
