using Cinematheque.Data;
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
    public static class DataModelExtensions
    {
        public static void CopyToData(this FilmInput input, Film data, HttpPostedFileBase poster)
        {
            if (data.ID != input.ID) throw new Exception("Cannot copy from foreign view to data");

            data.Title = input.Title;
            data.ReleaseDate = input.ReleaseDate;
            data.IMDbRating = input.IMDbRating;
            data.Duration = input.Duration;
            data.Country = new List<RegionInfo>();

            foreach (var name in input.Country)
            {
                data.Country.Add(CountryList.GetRegionByEnglishName(name));
            }

            data.RemoveAllGenres();

            if (input.Genres != null)
            {
                foreach (var id in input.Genres)
                {
                    var genre = DataHolder.GetGenreById(id);
                    data.AddGenre(genre);
                }

            }

            data.Director = DataHolder.GetDirectorById(input.DirectorID);

            data.RemoveAllActors();
            if (input.Actors != null)
            {
                foreach (var id in input.Actors)
                {
                    var actor = DataHolder.GetActorById(id);

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

        public static void CopyToData(this ActorInput input, Actor data, HttpPostedFileBase photo)
        {
            if (data.ID != input.ID) throw new Exception("Cannot copy from foreign view to data");

            data.Name = input.Name;
            data.Surname = input.Surname;
            data.Birth = input.Birth;
            data.Death = input.Death;
            data.Country = CountryList.GetRegionByEnglishName(input.Country);

            data.RemoveAllFilms();
            if (input.FilmsStared != null)
            {
                foreach (var id in input.FilmsStared)
                {
                    var film = DataHolder.GetFilmById(id);

                    data.AddFilm(film);
                }
            }

            data.Gender = (Data.Gender)input.Gender;

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

        public static void CopyToData(this DirectorInput input, Director data, HttpPostedFileBase photo)
        {
            if (data.ID != input.ID) throw new Exception("Cannot copy from foreign view to data");

            data.Name = input.Name;
            data.Surname = input.Surname;
            data.Birth = input.Birth;
            data.Death = input.Death;
            data.Country = CountryList.GetRegionByEnglishName(input.Country);

            data.RemoveAllFilms();
            if (input.FilmsDirected != null)
            {
                foreach (var id in input.FilmsDirected)
                {
                    var film = DataHolder.GetFilmById(id);

                    data.AddFilm(film);
                }
            }

            data.Gender = (Data.Gender)input.Gender;

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