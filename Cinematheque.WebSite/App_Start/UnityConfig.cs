using Cinematheque.Data.Dao;
using Cinematheque.Data.Dao.Impl;
using Cinematheque.Data.Data;
using Cinematheque.Data.Models;
using Cinematheque.Utils;
using System;
using System.Collections.Generic;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Lifetime;

namespace Cinematheque.WebSite
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<CinemathequeContext>(new PerRequestLifetimeManager());

            container.RegisterType<IDaoActor, ActorDao>();
            container.RegisterType<IDaoCountry, CountryDao>();
            container.RegisterType<IDaoDirector, DirectorDao>();
            container.RegisterType<IDaoFilm, FilmDao>();
            container.RegisterType<IDaoGenre, GenreDao>();

            FillContainer(container);
        }

        private static void FillContainer(IUnityContainer container)
        {
            var daoDirector = container.Resolve<IDaoDirector>();
            var daoActor = container.Resolve<IDaoActor>();
            var daoFilm = container.Resolve<IDaoFilm>();
            var daoGenre = container.Resolve<IDaoGenre>();
            var daoCountry = container.Resolve<IDaoCountry>();


            foreach (var n in CountryList.EnglishNames)
            {
                daoCountry.Add(new Country()
                {
                    Name = n
                });
            }

            var country = daoCountry.GetCountryByEnglishName("France");

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

                daoFilm.Add(f);
                daoActor.Add(a);
                daoGenre.Add(g);
                daoDirector.Add(d);

            }
        }
    }
}