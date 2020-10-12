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
    public class DirectorsController : Controller
    {
        // GET: Directors
        public ActionResult Index()
        {
            return View(DataHolder.Directors.Select(d => new DirectorView(d)).OrderBy(d => d.GetName()));
        }

        public ActionResult Search(string q)
        {
            var directors = DataHolder.Directors.Where(d => d.GetFullName().Contains(q, StringComparison.OrdinalIgnoreCase)).Select(d => new DirectorView(d));
            return View(directors.OrderBy(d => d.FullName));
        }

        public ActionResult Edit(Guid id)
        {
            var director = DataHolder.GetDirectorById(id);

            return View(new DirectorView(director));
        }

        [HttpPost]
        public ActionResult DoEdit(Guid id, HttpPostedFileBase file, DirectorInput newView)
        {
            var data = DataHolder.GetDirectorById(id);
            newView.CopyToData(data, file);

            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid id)
        {
            var director = DataHolder.GetDirectorById(id);

            return View(new DirectorView(director));
        }

        public ActionResult Delete(Guid id)
        {
            var director = DataHolder.GetDirectorById(id);

            return View(new DirectorView(director));
        }

        [HttpPost]
        public ActionResult DoDelete(Guid id)
        {
            var toRemove = DataHolder.GetDirectorById(id);

            toRemove.RemoveAllFilms();

            DataHolder.Directors.Remove(toRemove);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult DoCreate(HttpPostedFileBase file, DirectorInput input)
        {
            var director = input.CreateDirector(file);
            DataHolder.Directors.Add(director);

            return RedirectToAction("Index");
        }
    }
}