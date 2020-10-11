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
    public class ActorsController : Controller
    {
        // GET: Actors
        public ActionResult Index()
        {
            return View(DataHolder.Actors.Select(a => new ActorView(a)).OrderBy(a => a.GetName()));
        }

        public ActionResult Edit(Guid id)
        {
            var actor = DataHolder.Actors.Where(a => a.ID.Equals(id)).FirstOrDefault();

            return View(new ActorView(actor));
        }

        [HttpPost]
        public ActionResult DoEdit(Guid id, HttpPostedFileBase file, ActorInput newView)
        {
            var data = DataHolder.Actors.Where(a => a.ID.Equals(id)).FirstOrDefault();
            newView.CopyToData(data, file);

            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid id)
        {
            var actor = DataHolder.Actors.Where(a => a.ID.Equals(id)).FirstOrDefault();

            return View(new ActorView(actor));
        }

        public ActionResult Delete(Guid id)
        {
            var actor = DataHolder.Actors.Where(a => a.ID.Equals(id)).FirstOrDefault();

            return View(new ActorView(actor));
        }

        [HttpPost]
        public ActionResult DoDelete(Guid id)
        {
            var toRemove = DataHolder.Actors.Where(a => a.ID.Equals(id)).FirstOrDefault();
            DataHolder.Actors.Remove(toRemove);

            toRemove.RemoveAllFilms();

            return RedirectToAction("Index");
        }
    }
}