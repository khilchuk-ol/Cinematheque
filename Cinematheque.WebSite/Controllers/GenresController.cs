using Cinematheque.Data;
using Cinematheque.WebSite.Extensions;
using Cinematheque.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinematheque.WebSite.Controllers
{
    public class GenresController : Controller
    {
        // GET: Genres
        public ActionResult Index()
        {
            return View(DataHolder.Genres.OrderBy(g => g.Name));
        }

        public ActionResult Search(string q)
        {
            var genres = DataHolder.Genres.Where(g => g.Name.Contains(q, StringComparison.OrdinalIgnoreCase));
            return View(genres.OrderBy(f => f.Name));
        }

        //GET: Genres/Films
        public ActionResult Films(Guid id)
        {
            var genre = DataHolder.GetGenreById(id);

            return View(new GenreAndFilmsView
            {
                Films = genre?.FilmsOfGenre.Select(f => new FilmView(f)).OrderBy(f => f.Title),
                Genre = genre
            });
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult DoCreate(HttpPostedFileBase file, GenreInput input)
        {
            var genre = input.CreateGenre(file);
            DataHolder.Genres.Add(genre);

            return RedirectToAction("Index");
        }
    }
}