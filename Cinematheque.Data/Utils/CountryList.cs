using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinematheque.Data.Utils
{
    public static class CountryList
    {
        private static List<RegionInfo> Regions { get; }

        public static HashSet<string> EnglishNames
        {
            get { return Regions.Select(ri => ri.EnglishName)
                                .OrderBy(n => n)
                                .ToHashSet(); }
        }

        private static List<RegionInfo> MainFilmCountries { get; }

        public static HashSet<string> MainFilmCountriesNames
        {
            get { return MainFilmCountries.Select(ri => ri.EnglishName)
                                          .OrderBy(n => n)
                                          .ToHashSet(); }
        }

        static CountryList()
        {
            Regions = new List<RegionInfo>();
            MainFilmCountries = new List<RegionInfo>();

            FillRegions();
            FillMainFilmCountries();
        }

        private static void FillRegions()
        {
            var cultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (var ci in cultureInfo)
            {
                try
                {
                    var regionInfo = new RegionInfo(ci.LCID);
                    Regions.Add(regionInfo);
                }
                catch
                {
                    continue;
                }
            }
        }

        private static void FillMainFilmCountries()
        {
            var names = new string[] { "United States", "China", "Korea", "Netherlands", "Germany", "United Kingdom", "New Zealand",
                                       "Spain", "Italy", "France", "Belgium", "India", "Brazil", "Japan", "Poland",
                                       "Egypt", "Nigeria", "Iran", "Turkey", "Ukraine", "Russia", "Mexico", "Argentina"};

            foreach (var n in names)
            {
                MainFilmCountries.Add(GetRegionByEnglishName(n));
            }
        }

        public static RegionInfo GetRegionByEnglishName(string name)
        {
            return Regions.Where(ri => ri.EnglishName == name).FirstOrDefault();
        }
    }
}
