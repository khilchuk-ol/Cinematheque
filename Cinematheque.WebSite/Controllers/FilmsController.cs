using Cinematheque.Data;
using Cinematheque.Data.Utils;
using Cinematheque.WebSite.Extensions;
using Cinematheque.WebSite.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinematheque.WebSite.Controllers
{
    public class FilmsController : Controller
    {
        // GET: Films
        public ActionResult Index()
        {
            var st = PathUtils.GetProjectDirectory();
            return View(DataHolder.Films.Select(f => new FilmView(f)).OrderBy(f => f.Title));
        }

        // GET: Films/Search
        public ActionResult Search(string q)
        {
            var films = DataHolder.Films.Where(f => f.Title.Contains(q, StringComparison.OrdinalIgnoreCase)).Select(f => new FilmView(f));
            return View(films.OrderBy(f => f.Title));
        }

        public ActionResult Edit(Guid id)
        {
            var film = DataHolder.GetFilmById(id);

            return View(new FilmView(film));
        }

        [HttpPost]
        public ActionResult DoEdit(Guid id, HttpPostedFileBase file, FilmInput input)
        {
            var data = DataHolder.GetFilmById(id);
            input.CopyToData(data, file);

            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid id)
        {
            var film = DataHolder.GetFilmById(id);

            return View(new FilmView(film));
        }

        public ActionResult Delete(Guid id)
        {
            var film = DataHolder.GetFilmById(id);

            return View(new FilmView(film));
        }

        [HttpPost]
        public ActionResult DoDelete(Guid id)
        {
            var toRemove = DataHolder.GetFilmById(id);

            toRemove.RemoveAllActors();
            toRemove.RemoveAllGenres();
            toRemove.RemoveDirector();

            DataHolder.Films.Remove(toRemove);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult DoCreate(HttpPostedFileBase file, FilmInput input)
        {
            var film = input.CreateFilm(file);
            DataHolder.Films.Add(film);

            return RedirectToAction("Index");
        }

        //GET: Films/Top100
        public ActionResult Top100()
        {
            return View(DataHolder.Films.OrderByDescending(f => f.IMDbRating)
                                        .Take(100)
                                        .Select(f => new FilmView(f))
                                        .ToList());
        }
    }
}