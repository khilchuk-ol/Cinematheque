using Cinematheque.Data;
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
    public class DirectorsController : Controller
    {
        private IDaoDirector DirectorsDao { get; }

        private IDaoFilm FilmsDao { get; }

        private IDaoCountry CountriesDao { get; }

        public DirectorsController(IDaoDirector directorsDao, IDaoFilm filmsDao, IDaoCountry countriesDao)
        {
            Validator.RequireNotNull(directorsDao);
            Validator.RequireNotNull(filmsDao);
            Validator.RequireNotNull(countriesDao);

            DirectorsDao = directorsDao;
            FilmsDao = filmsDao;
            CountriesDao = countriesDao;
        }

        // GET: Directors
        public ActionResult Index()
        {
            return View(DirectorsDao.FindAll().Select(d => new DirectorView(d)).OrderBy(d => d.FullName));
        }

        public ActionResult Search(string q)
        {
            var directors = DirectorsDao.SearchDirectorsByName(q).Select(d => new DirectorView(d));
            return View(directors.OrderBy(d => d.FullName));
        }

        public ActionResult Edit(Guid id)
        {
            var director = DirectorsDao.GetDirectorAndFilms(id);

            return View(new DirectorInfoContainer()
            {
                Director = new DirectorView(director),
                AvailableFilms = FilmsDao.GetFilmsWithoutDirector(director.ID)
            });
        }

        [HttpPost]
        public ActionResult DoEdit(Guid id, HttpPostedFileBase file, DirectorInput newView)
        {
            var data = DirectorsDao.Find(id);
            newView.CopyToData(data, file, CountriesDao, FilmsDao);

            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid id)
        {
            var director = DirectorsDao.GetDirectorAndFilms(id);

            return View(new DirectorView(director));
        }

        public ActionResult Delete(Guid id)
        {
            var director = DirectorsDao.GetDirectorAndFilms(id);

            return View(new DirectorView(director));
        }

        [HttpPost]
        public ActionResult DoDelete(Guid id)
        {
            var toRemove = DirectorsDao.Find(id);

            toRemove.RemoveAllFilms();

            DirectorsDao.Delete(toRemove);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View(new DirectorInfoContainer()
            {
                AvailableFilms = FilmsDao.FindAll()
            });
        }

        public ActionResult DoCreate(HttpPostedFileBase file, DirectorInput input)
        {
            var director = input.CreateDirector(file, CountriesDao, FilmsDao);
            DirectorsDao.Add(director);

            return RedirectToAction("Index");
        }
    }
}