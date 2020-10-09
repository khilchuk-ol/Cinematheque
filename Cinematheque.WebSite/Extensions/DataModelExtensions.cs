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
        public static void CopyToData(this FilmView @view, Film data, HttpPostedFileBase poster)
        {
            if (data.ID != view.ID) throw new Exception("Cannot copy from foreign view to data");

            data.Title = view.Title;
            data.ReleaseDate = view.ReleaseDate;
            data.IMDbRating = view.IMDbRating;
            data.Duration = view.Duration;
            data.Country = new List<RegionInfo>();

            foreach (var name in view.Country)
            {
                var c = CountryList.GetRegionByEnglishName(name);
                data.Country.Add(CountryList.GetRegionByEnglishName(name));
            }

            //data.Genres = view.Genres;

            data.Director = DataHolder.Directors.Where(d => d.ID == view.DirectorID).FirstOrDefault();

            //data.Actors = view.Actors;

            if (poster != null && poster.ContentLength > 0)
            { 
                try
                {
                    if (poster.ContentType.Contains("image"))
                    {
                        var filename = Path.Combine(new Guid().ToString(), Path.GetExtension(poster.FileName));

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

        public static void CopyToData(this ActorView @view, Actor data, HttpPostedFileBase photo)
        {
            if (data.ID != view.ID) throw new Exception("Cannot copy from foreign view to data");

            data.Name = view.Name;
            data.Surname = view.Surname;
            data.Birth = view.Birth;
            data.Death = view.Death;
            data.Country = CountryList.GetRegionByEnglishName(view.Country);

            //data.FilmsStared = view.FilmsStared;

            data.Gender = (Data.Gender)view.Gender;

            if (photo != null && photo.ContentLength > 0)
            {
                try
                {
                    if (photo.ContentType.Contains("image"))
                    {
                        var filename = Path.Combine(new Guid().ToString(), Path.GetExtension(photo.FileName));

                        var path = Path.Combine(PathUtils.GetProjectDirectory(),
                                                "Cinematheque.WebSite\\images\\actors\\",
                                                filename);
                        photo.SaveAs(path);

                        data.PhotoFileNames.Append(filename);
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

        public static void CopyToData(this DirectorView @view, Director data, HttpPostedFileBase photo)
        {
            if (data.ID != view.ID) throw new Exception("Cannot copy from foreign view to data");

            data.Name = view.Name;
            data.Surname = view.Surname;
            data.Birth = view.Birth;
            data.Death = view.Death;
            data.Country = CountryList.GetRegionByEnglishName(view.Country);

            //data.FilmsDirected = view.FilmsDirected;

            data.Gender = (Data.Gender)view.Gender;

            if (photo != null && photo.ContentLength > 0)
            {
                try
                {
                    if (photo.ContentType.Contains("image"))
                    {
                        var filename = Path.Combine(new Guid().ToString(), Path.GetExtension(photo.FileName));

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