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
    public class ActorsController : Controller
    {
        private IDaoActor Dao { get; }

        public ActorsController(IDaoActor dao)
        {
            Validator.RequireNotNull(dao);
            Dao = dao;
        }
        // GET: Actors
        public ActionResult Index()
        {
            return View(Dao.FindAll().Select(a => new ActorView(a)).OrderBy(a => a.FullName));
        }

        public ActionResult Search(string q)
        {
            var actors = Dao.SearchActorsByName(q).Select(a => new ActorView(a));
            return View(actors.OrderBy(a => a.FullName));
        }

        public ActionResult Edit(Guid id)
        {
            var actor = Dao.GetActorWithFilms(id);

            return View(new ActorView(actor));
        }

        [HttpPost]
        public ActionResult DoEdit(Guid id, HttpPostedFileBase file, ActorInput newView)
        {
            var data = Dao.GetActorWithFilms(id);
            newView.CopyToData(data, file);

            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid id)
        {
            var actor = Dao.GetActorWithFilms(id);

            return View(new ActorView(actor));
        }

        public ActionResult Delete(Guid id)
        {
            var actor = Dao.GetActorWithFilms(id);

            return View(new ActorView(actor));
        }

        [HttpPost]
        public ActionResult DoDelete(Guid id)
        {
            var toRemove = Dao.Find(id);
            Dao.Delete(toRemove);

            toRemove.RemoveAllFilms();

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult DoCreate(HttpPostedFileBase file, ActorInput input)
        {
            var actor = input.CreateActor(file);
            Dao.Add(actor);

            return RedirectToAction("Index");
        }
    }
}