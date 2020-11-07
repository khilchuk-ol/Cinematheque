using Cinematheque.Data;
using Cinematheque.Data.Dao;
using Cinematheque.Utils;
using Cinematheque.WebSite.Extensions;
using Cinematheque.WebSite.Models;
using Cinematheque.WebSite.Models.InfoContainers;
using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace Cinematheque.WebSite.Controllers
{
    public class FilmsController : Controller
    {
        private IDaoFilm FilmsDao { get; }

        private IDaoActor ActorsDao { get; }

        private IDaoDirector DirectorsDao { get; }

        private IDaoGenre GenresDao { get; }

        private IDaoCountry CountriesDao { get; }

        public FilmsController(IDaoFilm filmsDao, IDaoActor actorsDao, IDaoDirector directorsDao, IDaoGenre genresDao, IDaoCountry countriesDao)
        {
            Validator.RequireNotNull(filmsDao);
            Validator.RequireNotNull(actorsDao);
            Validator.RequireNotNull(directorsDao);
            Validator.RequireNotNull(genresDao);
            Validator.RequireNotNull(countriesDao);

            FilmsDao = filmsDao;
            ActorsDao = actorsDao;
            DirectorsDao = directorsDao;
            GenresDao = genresDao;
            CountriesDao = countriesDao;
        }

        // GET: Films
        public ActionResult Index()
        {
            var st = PathUtils.GetProjectDirectory();
            return View(FilmsDao.FindAllWithGenres().Select(f => new FilmView(f)).OrderBy(f => f.Title));
        }

        // GET: Films/Search
        public ActionResult Search(string q)
        {
            var films = FilmsDao.SearchFilmsByTitle(q).Select(f => new FilmView(f));
            return View(films.OrderBy(f => f.Title));
        }

        public ActionResult Edit(Guid id)
        {
            var film = FilmsDao.GetFilmWithFullInfo(id);

            return View(new FilmInfoContainer()
            {
                Film = new FilmView(film),
                AvailableActors = ActorsDao.GetActorsWithoutFilm(film.ID),
                AvailableDirectors = DirectorsDao.GetDirectorsWithoutFilm(film.ID),
                AvailableGenres = GenresDao.GetGenresWithoutFilm(film.ID)
            });
        }

        [HttpPost]
        public ActionResult DoEdit(Guid id, HttpPostedFileBase file, FilmInput input)
        {
            var data = FilmsDao.Find(id);
            input.CopyToData(data, file, CountriesDao, GenresDao, DirectorsDao, ActorsDao);

            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid id)
        {
            var film = FilmsDao.GetFilmWithFullInfo(id);

            return View(new FilmView(film));
        }

        public ActionResult Delete(Guid id)
        {
            var film = FilmsDao.GetFilmWithFullInfo(id);

            return View(new FilmView(film));
        }

        [HttpPost]
        public ActionResult DoDelete(Guid id)
        {
            var toRemove = FilmsDao.Find(id);

            FilmsDao.Delete(toRemove);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View(new FilmInfoContainer()
            {
                AvailableActors = ActorsDao.FindAll(),
                AvailableDirectors = DirectorsDao.FindAll(),
                AvailableGenres = GenresDao.FindAll()
            });
        }

        public ActionResult DoCreate(HttpPostedFileBase file, FilmInput input)
        {
            var film = input.CreateFilm(file, CountriesDao, ActorsDao, DirectorsDao, GenresDao);
            FilmsDao.Add(film);

            return RedirectToAction("Index");
        }

        //GET: Films/Top100
        public ActionResult Top100()
        {
            return View(FilmsDao.FindAll().OrderByDescending(f => f.IMDbRating)
                                     .Take(100)
                                     .Select(f => new FilmView(f))
                                     .ToList());
        }
    }
}