using Cinematheque.Data.Dao;
using Cinematheque.Data.Dao.Impl;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Cinematheque.Data
{
    public static class DataHolder
    {
        public static IDaoDirector DaoDirector { get; private set; }

        public static IDaoActor DaoActor { get; private set; }

        public static IDaoFilm DaoFilm { get; private set; }

        public static IDaoGenre DaoGenre { get; private set; }


        static DataHolder()
        {
            DaoDirector = new DirectorDao();
            DaoActor = new ActorDao();
            DaoFilm = new FilmDao();
            DaoGenre = new GenreDao();

            for (var i = 0; i < 10; i++)
            {
                var ch = Convert.ToChar(65 + i);

                var a = new Actor()
                {
                    Name = ch.ToString(),
                    Surname = "Actor",
                    Birth = DateTime.Now,
                    Country = RegionInfo.CurrentRegion,
                    Gender = Gender.NotIdentified,
                    PhotoFileName = $"actor{ch}.jpg",
                    Biography = "aaaaaaaaaaaaaaaaaaaaaaa"
                };

                var d = new Director()
                {
                    Name = ch.ToString(),
                    Surname = "Director",
                    Birth = DateTime.Now,
                    Country = RegionInfo.CurrentRegion,
                    Gender = Gender.NotIdentified,
                    PhotoFileName = $"director{ch}.jpg",
                    Biography = "ddddddddddddddddddddddddd"
                };

                var f = new Film()
                {
                    Title = $"film{ch}",
                    ReleaseDate = DateTime.Now,
                    Country = new List<RegionInfo>(),
                    IMDbRating = 5,
                    Description = "fffffffffffffffffffff",
                    PosterFileName = $"film{ch}.jpg",
                    Duration = TimeSpan.FromMinutes(90)
                };
                f.Country.Add(RegionInfo.CurrentRegion);

                var g = new Genre()
                {
                    Name = $"Genre {ch}"
                };

                a.AddFilm(f);
                f.AddActor(a);
                f.AddGenre(g);
                g.AddFilm(f);
                d.AddFilm(f);
                f.DirectorID = d.ID;
                f.Director = d;

                DaoFilm.Add(f);
                DaoActor.Add(a);
                DaoGenre.Add(g);
                DaoDirector.Add(d);
            }
        }

        
    }
}
