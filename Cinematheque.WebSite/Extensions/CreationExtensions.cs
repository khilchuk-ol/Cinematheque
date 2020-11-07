using Cinematheque.Data.Dao;
using Cinematheque.Data.Models;
using Cinematheque.Utils;
using Cinematheque.WebSite.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Cinematheque.WebSite.Extensions
{
    public static class CreationExtensions
    {
        public static Film CreateFilm(this FilmInput input, HttpPostedFileBase poster, 
                                      IDaoCountry daoCountry, IDaoActor daoActor, IDaoDirector daoDirector, IDaoGenre daoGenre)
        {
            var film = new Film
            {
                Title = input.Title,
                ReleaseDate = input.ReleaseDate,
                IMDbRating = input.IMDbRating,
                Duration = input.Duration,
                Description = input.Description,
                Countries = new List<Country>()
            };

            foreach (var name in input.Countries)
            {
                film.Countries.Add(daoCountry.GetCountryByEnglishName(name));
            }

            if (poster != null && poster.ContentLength > 0)
            {
                try
                {
                    if (poster.ContentType.Contains("image"))
                    {
                        var filename = Guid.NewGuid().ToString() + Path.GetExtension(poster.FileName);

                        var path = Path.Combine(PathUtils.GetProjectDirectory(),
                                                "Cinematheque.WebSite\\images\\films\\",
                                                filename);
                        poster.SaveAs(path);

                        film.PosterFileName = filename;
                    }
                    else
                    {
                        throw new Exception("ERROR: Uploaded file is not image");
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("ERROR:" + ex.Message.ToString());
                }
            }
            else
            {
                film.PosterFileName = "default.jpg";
            }

            if (input.Genres != null)
            {
                foreach (var id in input.Genres)
                {
                    var genre = daoGenre.Find(id);
                    film.AddGenre(genre);
                }

            }

            film.Director = daoDirector.Find(input.DirectorID);

            if (input.Actors != null)
            {
                foreach (var id in input.Actors)
                {
                    var actor = daoActor.Find(id);

                    film.AddActor(actor);
                }
            }

            return film;
        }

        public static Actor CreateActor(this ActorInput input, HttpPostedFileBase photo, IDaoCountry daoCountry, IDaoFilm daoFilm)

        {
            var actor = new Actor
            {
                Name = input.Name,
                Surname = input.Surname,
                Birth = input.Birth,
                Death = input.Death,
                Country = daoCountry.GetCountryByEnglishName(input.Country),
                Gender = (Data.Models.Gender)input.Gender
            };

            if (photo != null && photo.ContentLength > 0)
            {
                try
                {
                    if (photo.ContentType.Contains("image"))
                    {
                        var filename = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);

                        var path = Path.Combine(PathUtils.GetProjectDirectory(),
                                                "Cinematheque.WebSite\\images\\actors\\",
                                                filename);
                        photo.SaveAs(path);

                        actor.PhotoFileName = filename;
                    }
                    else
                    {
                        throw new Exception("ERROR: Uploaded file is not image");
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("ERROR:" + ex.Message.ToString());
                }
            }
            else
            {
                actor.PhotoFileName = "default.jpg";
            }

            if (input.FilmsStared != null)
            {
                foreach (var id in input.FilmsStared)
                {
                    var film = daoFilm.Find(id);

                    actor.AddFilm(film);
                }
            }

            return actor;
        }

        public static Director CreateDirector(this DirectorInput input, HttpPostedFileBase photo, IDaoCountry daoCountry, IDaoFilm daoFilm)
        {
            var director = new Director
            {
                Name = input.Name,
                Surname = input.Surname,
                Birth = input.Birth,
                Death = input.Death,
                Country = daoCountry.GetCountryByEnglishName(input.Country),
                Gender = (Data.Models.Gender)input.Gender
            };

            if (photo != null && photo.ContentLength > 0)
            {
                try
                {
                    if (photo.ContentType.Contains("image"))
                    {
                        var filename = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);

                        var path = Path.Combine(PathUtils.GetProjectDirectory(),
                                                "Cinematheque.WebSite\\images\\directors\\",
                                                filename);
                        photo.SaveAs(path);

                        director.PhotoFileName = filename;
                    }
                    else
                    {
                        throw new Exception("ERROR: Uploaded file is not image");
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("ERROR:" + ex.Message.ToString());
                }
            }
            else
            {
                director.PhotoFileName = "default.jpg";
            }

            if (input.FilmsDirected != null)
            {
                foreach (var id in input.FilmsDirected)
                {
                    var film = daoFilm.Find(id);

                    director.AddFilm(film);
                }
            }

            return director;
        }

        public static Genre CreateGenre(this GenreInput input, IDaoFilm daoFilm)
        {
            var genre = new Genre()
            {
                Name = input.Name
            };

            if (input.FilmsOfGenre != null)
            {
                foreach (var id in input.FilmsOfGenre)
                {
                    var film = daoFilm.Find(id);

                    genre.AddFilm(film);
                }
            }

            return genre;
        }
    }
}