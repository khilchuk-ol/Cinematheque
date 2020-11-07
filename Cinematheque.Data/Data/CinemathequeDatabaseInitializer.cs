using Cinematheque.Data.Models;
using Cinematheque.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Cinematheque.Data.Data
{
    public class CinemathequeDatabaseInitializer : DropCreateDatabaseIfModelChanges<CinemathequeContext>
    {
        protected override void Seed(CinemathequeContext context)
        {
            foreach (var n in CountryList.EnglishNames)
            {
                context.Countries.Add(new Country { Name = n });
            }
            context.SaveChanges();

            var country = context.Countries.Where(c => c.Name == "France").FirstOrDefault();

            for (var i = 0; i < 10; i++)
            {
                var ch = Convert.ToChar(65 + i);

                var a = new Actor()
                {
                    Name = ch.ToString() + "aaak",
                    Surname = "Actor",
                    Birth = DateTime.Now,
                    CountryId = country.ID,
                    Gender = Gender.NotIdentified,
                    PhotoFileName = $"actor{ch}.jpg",
                    Biography = "aaaaaaaaaaaaaaaaaaaaaaa"
                };

                var d = new Director()
                {
                    Name = ch.ToString(),
                    Surname = "Director",
                    Birth = DateTime.Now,
                    CountryId = country.ID,
                    Gender = Gender.NotIdentified,
                    PhotoFileName = $"director{ch}.jpg",
                    Biography = "ddddddddddddddddddddddddd"
                };

                var f = new Film()
                {
                    Title = $"film{ch}",
                    ReleaseDate = DateTime.Now,
                    Countries = new List<Country>(),
                    IMDbRating = 5,
                    Description = "fffffffffffffffffffff",
                    PosterFileName = $"film{ch}.jpg",
                    Duration = TimeSpan.FromMinutes(90)
                };
                f.Countries.Add(country);

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

                context.Films.Add(f);
                context.Actors.Add(a);
                context.Genres.Add(g);
                context.Directors.Add(d);

            }

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
