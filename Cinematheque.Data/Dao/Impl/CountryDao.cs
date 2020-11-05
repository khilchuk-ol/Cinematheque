using Cinematheque.Data.Data;
using Cinematheque.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Cinematheque.Data.Dao.Impl
{
    public class CountryDao : AbstractDao<Country>, IDaoCountry
    {
        public CountryDao(CinemathequeContext context) : base(context) { }

        public Country GetCountryByEnglishName(string name)
        {
            return Context.Countries
                          .Where(c => c.Name.Equals(name))
                          .FirstOrDefault();
        }

        public HashSet<string> GetNames()
        {
            return Context.Countries
                          .Select(c => c.Name)
                          .ToHashSet();
        }
    }
}
