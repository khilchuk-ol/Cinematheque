using Cinematheque.Data;
using Cinematheque.WebSite.Extensions;
using Cinematheque.WebSite.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinematheque.WebSite.Controllers
{
    public class DirectorsController : Controller
    {
        // GET: Directors
        public ActionResult Index()
        {
            return View(DataHolder.DaoDirector.FindAll().Select(d => new DirectorView(d)).OrderBy(d => d.FullName));
        }

        public ActionResult Search(string q)
        {
            var directors = DataHolder.DaoDirector.SearchDirectorsByName(q).Select(d => new DirectorView(d));
            return View(directors.OrderBy(d => d.FullName));
        }

        public ActionResult Edit(Guid id)
        {
            var director = DataHolder.DaoDirector.GetDirectorAndFilms(id);

            return View(new DirectorView(director));
        }

        [HttpPost]
        public ActionResult DoEdit(Guid id, HttpPostedFileBase file, DirectorInput newView)
        {
            var data = DataHolder.DaoDirector.Find(id);
            newView.CopyToData(data, file);

            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid id)
        {
            var director = DataHolder.DaoDirector.GetDirectorAndFilms(id);

            return View(new DirectorView(director));
        }

        public ActionResult Delete(Guid id)
        {
            var director = DataHolder.DaoDirector.GetDirectorAndFilms(id);

            return View(new DirectorView(director));
        }

        [HttpPost]
        public ActionResult DoDelete(Guid id)
        {
            var toRemove = DataHolder.DaoDirector.Find(id);

            toRemove.RemoveAllFilms();

            DataHolder.DaoDirector.Delete(toRemove);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult DoCreate(HttpPostedFileBase file, DirectorInput input)
        {
            var director = input.CreateDirector(file);
            DataHolder.DaoDirector.Add(director);

            return RedirectToAction("Index");
        }
    }
}