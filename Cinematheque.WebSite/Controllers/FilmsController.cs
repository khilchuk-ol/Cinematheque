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
            var films = DataHolder.Films.Where(f => f.Title.Contains(q)).Select(f => new FilmView(f));
            return View(films.OrderBy(f => f.Title));
        }

        public ActionResult Edit(Guid id)
        {
            var film = DataHolder.Films.Where(f => f.ID.Equals(id)).FirstOrDefault();

            return View(new FilmView(film));
        }

        [HttpPost]
        public ActionResult DoEdit(Guid id, HttpPostedFileBase file, FilmView newView)
        {
            var data = DataHolder.Films.Where(f => f.ID.Equals(id)).FirstOrDefault();
            newView.CopyToData(data, file);

            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid id)
        {
            var film = DataHolder.Films.Where(f => f.ID.Equals(id)).FirstOrDefault();

            return View(new FilmView(film));
        }

        public ActionResult Delete(Guid id)
        {
            var film = DataHolder.Films.Where(f => f.ID.Equals(id)).FirstOrDefault();

            return View(new FilmView(film));
        }

        [HttpPost]
        public ActionResult DoDelete(Guid id)
        {
            var toRemove = DataHolder.Films.Where(f => f.ID.Equals(id)).FirstOrDefault();
            DataHolder.Films.Remove(toRemove);
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