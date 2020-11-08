using Cinematheque.Data.Dao;
using Cinematheque.Utils;
using Cinematheque.WebSite.Extensions;
using Cinematheque.WebSite.Models;
using Cinematheque.WebSite.Models.InfoContainers;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinematheque.WebSite.Controllers
{
    public class ActorsController : Controller
    {
        private IDaoActor ActorsDao { get; }

        private IDaoFilm FilmsDao { get; }

        private IDaoCountry CountriesDao { get; }

        public ActorsController(IDaoActor actorsDao, IDaoFilm filmsDao, IDaoCountry countriesDao)
        {
            Validator.RequireNotNull(actorsDao);
            Validator.RequireNotNull(filmsDao);
            Validator.RequireNotNull(countriesDao);

            ActorsDao = actorsDao;
            FilmsDao = filmsDao;
            CountriesDao = countriesDao;
        }
        // GET: Actors
        public ActionResult Index()
        {
            return View(ActorsDao.FindAll().Select(a => new ActorView(a)).OrderBy(a => a.FullName));
        }

        public ActionResult Search(string q)
        {
            var actors = ActorsDao.SearchActorsByName(q).Select(a => new ActorView(a));
            return View(actors.OrderBy(a => a.FullName));
        }

        public ActionResult Edit(Guid id)
        {
            var actor = ActorsDao.GetActorWithFilms(id);

            return View(new ActorInfoContainer()
            {
                Actor = new ActorView(actor),
                AvailableFilms = FilmsDao.GetFilmsWithoutActor(actor.ID),
                Countries = CountriesDao.FindAll().OrderBy(c => c.Name)
            });
        }

        [HttpPost]
        public ActionResult DoEdit(Guid id, HttpPostedFileBase file, ActorInput newView)
        {
            var data = ActorsDao.GetActorWithFilms(id);
            newView.CopyToData(data, file, CountriesDao, FilmsDao);
            ActorsDao.Update(data);

            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid id)
        {
            var actor = ActorsDao.GetActorWithFilms(id);

            return View(new ActorView(actor));
        }

        public ActionResult Delete(Guid id)
        {
            var actor = ActorsDao.GetActorWithFilms(id);

            return View(new ActorView(actor));
        }

        [HttpPost]
        public ActionResult DoDelete(Guid id)
        {
            var toRemove = ActorsDao.Find(id);
            ActorsDao.Delete(toRemove);

            toRemove.RemoveAllFilms();

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View(new ActorInfoContainer()
            {
                AvailableFilms = FilmsDao.FindAll(),
                Countries = CountriesDao.FindAll().OrderBy(c => c.Name)
            }) ;
        }

        public ActionResult DoCreate(HttpPostedFileBase file, ActorInput input)
        {
            var actor = input.CreateActor(file, CountriesDao, FilmsDao);
            ActorsDao.Add(actor);

            return RedirectToAction("Index");
        }
    }
}