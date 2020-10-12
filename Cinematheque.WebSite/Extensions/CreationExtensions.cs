﻿using Cinematheque.Data;
using Cinematheque.Data.Utils;
using Cinematheque.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace Cinematheque.WebSite.Extensions
{
    public static class CreationExtensions
    {
        public static Film CreateFilm(this FilmInput input, HttpPostedFileBase poster)
        {
            var film = new Film();

            film.Title = input.Title;
            film.ReleaseDate = input.ReleaseDate;
            film.IMDbRating = input.IMDbRating;
            film.Duration = input.Duration;
            film.Description = input.Description;
            film.Country = new List<RegionInfo>();

            foreach (var name in input.Country)
            {
                film.Country.Add(CountryList.GetRegionByEnglishName(name));
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
                    var genre = DataHolder.GetGenreById(id);
                    film.AddGenre(genre);
                }

            }

            film.Director = DataHolder.GetDirectorById(input.DirectorID);

            if (input.Actors != null)
            {
                foreach (var id in input.Actors)
                {
                    var actor = DataHolder.GetActorById(id);

                    film.AddActor(actor);
                }
            }

            return film;
        }

        public static Actor CreateActor(this ActorInput input, HttpPostedFileBase photo)
        {
            var actor = new Actor();

            actor.Name = input.Name;
            actor.Surname = input.Surname;
            actor.Birth = input.Birth;
            actor.Death = input.Death;
            actor.Country = CountryList.GetRegionByEnglishName(input.Country);
            actor.Gender = (Data.Gender)input.Gender;

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
                    var film = DataHolder.GetFilmById(id);

                    actor.AddFilm(film);
                }
            }

            return actor;
        }

        public static Director CreateDirector(this DirectorInput input, HttpPostedFileBase photo)
        {
            var director = new Director();

            director.Name = input.Name;
            director.Surname = input.Surname;
            director.Birth = input.Birth;
            director.Death = input.Death;
            director.Country = CountryList.GetRegionByEnglishName(input.Country);
            director.Gender = (Data.Gender)input.Gender;

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
                    var film = DataHolder.GetFilmById(id);

                    director.AddFilm(film);
                }
            }

            return director;
        }

        public static Genre CreateGenre(this GenreInput input, HttpPostedFileBase photo)
        {
            var genre = new Genre(input.Name);

            if (input.FilmsOfGenre != null)
            {
                foreach (var id in input.FilmsOfGenre)
                {
                    var film = DataHolder.GetFilmById(id);

                    genre.AddFilm(film);
                }
            }

            return genre;
        }
    }
}