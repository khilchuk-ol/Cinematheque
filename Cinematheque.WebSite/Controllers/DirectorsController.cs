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

        public ActionResult Edit(Guid id)
        {
            var director = DataHolder.Directors.Where(d => d.ID.Equals(id)).FirstOrDefault();

            return View(new DirectorView(director));
        }

        [HttpPost]
        public ActionResult DoEdit(Guid id, HttpPostedFileBase file, DirectorView newView)
        {
            var data = DataHolder.Directors.Where(d => d.ID.Equals(id)).FirstOrDefault();
            newView.CopyToData(data, file);

            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid id)
        {
            var director = DataHolder.Directors.Where(d => d.ID.Equals(id)).FirstOrDefault();

            return View(new DirectorView(director));
        }

        public ActionResult Delete(Guid id)
        {
            var director = DataHolder.Directors.Where(d => d.ID.Equals(id)).FirstOrDefault();

            return View(new DirectorView(director));
        }

        [HttpPost]
        public ActionResult DoDelete(Guid id)
        {
            var toRemove = DataHolder.Directors.Where(d => d.ID.Equals(id)).FirstOrDefault();
            DataHolder.Directors.Remove(toRemove);
            return RedirectToAction("Index");
        }
    }
}