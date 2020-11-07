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
    public static class DataModelExtensions
    {
        public static void CopyToData(this FilmInput input, Film data, HttpPostedFileBase poster, 
                                      IDaoCountry daoCountry, IDaoGenre daoGenre, IDaoDirector daoDirector, IDaoActor daoActor)
        {
            if (data.ID != input.ID) throw new Exception("Cannot copy from foreign view to data");

            data.Title = input.Title;
            data.ReleaseDate = input.ReleaseDate;
            data.IMDbRating = input.IMDbRating;
            data.Duration = input.Duration;
            data.Countries = new List<Country>();

            foreach (var name in input.Countries)
            {
                data.Countries.Add(daoCountry.GetCountryByEnglishName(name));
            }

            data.RemoveAllGenres();

            if (input.Genres != null)
            {
                foreach (var id in input.Genres)
                {
                    var genre = daoGenre.Find(id);
                    data.AddGenre(genre);
                }

            }

            data.Director = daoDirector.Find(input.DirectorID);

            data.RemoveAllActors();
            if (input.Actors != null)
            {
                foreach (var id in input.Actors)
                {
                    var actor = daoActor.Find(id);

                    data.AddActor(actor);
                }
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

                        /*File.Delete(Path.Combine(PathUtils.GetProjectDirectory(),
                                                "Cinematheque.WebSite\\images\\films\\",
                                                filename);*/

                        data.PosterFileName = filename;
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
        }

        public static void CopyToData(this ActorInput input, Actor data, HttpPostedFileBase photo, IDaoCountry daoCountry, IDaoFilm daoFilm)
        {
            if (data.ID != input.ID) throw new Exception("Cannot copy from foreign view to data");

            data.Name = input.Name;
            data.Surname = input.Surname;
            data.Birth = input.Birth;
            data.Death = input.Death;
            data.Country = daoCountry.GetCountryByEnglishName(input.Country);

            data.RemoveAllFilms();
            if (input.FilmsStared != null)
            {
                foreach (var id in input.FilmsStared)
                {
                    var film = daoFilm.Find(id);

                    data.AddFilm(film);
                }
            }

            data.Gender = (Data.Models.Gender)input.Gender;

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

                        data.PhotoFileName = filename;
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
        }

        public static void CopyToData(this DirectorInput input, Director data, HttpPostedFileBase photo, IDaoCountry daoCountry, IDaoFilm daoFilm)
        {
            if (data.ID != input.ID) throw new Exception("Cannot copy from foreign view to data");

            data.Name = input.Name;
            data.Surname = input.Surname;
            data.Birth = input.Birth;
            data.Death = input.Death;
            data.Country = daoCountry.GetCountryByEnglishName(input.Country);

            data.RemoveAllFilms();
            if (input.FilmsDirected != null)
            {
                foreach (var id in input.FilmsDirected)
                {
                    var film = daoFilm.Find(id);

                    data.AddFilm(film);
                }
            }

            data.Gender = (Data.Models.Gender)input.Gender;

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

                        /*File.Delete(Path.Combine(PathUtils.GetProjectDirectory(),
                                                "Cinematheque.WebSite\\images\\directors\\",
                                                filename);*/

                        data.PhotoFileName = filename;
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
        }
    }
}