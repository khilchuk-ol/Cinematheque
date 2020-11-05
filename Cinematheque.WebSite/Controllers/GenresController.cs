using Cinematheque.Data;
using Cinematheque.Data.Dao;
using Cinematheque.Utils;
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
        private IDaoGenre Dao { get; }

        public GenresController(IDaoGenre dao)
        {
            Validator.RequireNotNull(dao);
            Dao = dao;
        }

        // GET: Genres
        public ActionResult Index()
        {
            return View(Dao.FindAll().OrderBy(g => g.Name));
        }

        public ActionResult Search(string q)
        {
            var genres =Dao.SearchGenresByName(q);
            return View(genres.OrderBy(f => f.Name));
        }

        //GET: Genres/Films
        public ActionResult Films(Guid id)
        {
            var genre = Dao.GetGenreWithFilms(id);

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
            Dao.Add(genre);

            return RedirectToAction("Index");
        }
    }
}