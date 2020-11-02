using Cinematheque.Data;
using Cinematheque.WebSite.Extensions;
using Cinematheque.WebSite.Models;
using System;
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
            return View(DataHolder.DaoGenre.FindAll().OrderBy(g => g.Name));
        }

        public ActionResult Search(string q)
        {
            var genres = DataHolder.DaoGenre.SearchGenresByName(q);
            return View(genres.OrderBy(f => f.Name));
        }

        //GET: Genres/Films
        public ActionResult Films(Guid id)
        {
            var genre = DataHolder.DaoGenre.GetGenreWithFilms(id);

            return View(new GenreAndFilmsView
            {
                Films = genre?.Films.Select(f => new FilmView(f)).OrderBy(f => f.Title),
                Genre = genre
            });
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult DoCreate(GenreInput input)
        {
            var genre = input.CreateGenre();
            DataHolder.DaoGenre.Add(genre);

            return RedirectToAction("Index");
        }
    }
}