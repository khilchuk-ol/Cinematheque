using Cinematheque.Data.Dao;
using Cinematheque.Data.Models;
using Cinematheque.Utils;
using Cinematheque.WebSite.Extensions;
using Cinematheque.WebSite.ModelBinders;
using Cinematheque.WebSite.Models;
using Cinematheque.WebSite.Models.InfoContainers;
using Newtonsoft.Json;
using System;
using System.Linq;
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

            //var emul = FilmsDbEmulXml.Deserialize();
        }

        // GET: Films
        public ActionResult Index([ModelBinder(typeof(UserModelBinder))] User user)
        {
            return View(FilmsDao.FindAllWithGenres()
                                .Select(f => new FilmView(f) { IsFav = user.HasFavourite(f) })
                                .OrderBy(f => f.Title));
        }

        // GET: Films/Search
        public ActionResult Search(string q)
        {
            return View(FilmsDao.SearchFilmsByTitle(q)
                                .Select(f => new FilmView(f))
                                .OrderBy(f => f.Title));
        }

        
        public ActionResult Edit(Guid id)
        {
            var film = FilmsDao.GetFilmWithFullInfo(id);

            return View(new FilmInfoContainer()
            {
                Film = new FilmView(film),
                AvailableActors = ActorsDao.GetActorsWithoutFilm(film.ID),
                AvailableDirectors = DirectorsDao.FindAll(),
                AvailableGenres = GenresDao.GetGenresWithoutFilm(film.ID)
            });
        }

        [HttpPost]
        public ActionResult DoEdit(Guid id, HttpPostedFileBase file, FilmInput input)
        {
            var data = FilmsDao.GetFilmWithFullInfo(id);
            input.CopyToData(data, file, CountriesDao, GenresDao, DirectorsDao, ActorsDao);
            FilmsDao.Update(data);

            return RedirectToAction("Index");
        }

        public ActionResult Details([ModelBinder(typeof(UserModelBinder))] User user, Guid id)
        {
            var film = FilmsDao.GetFilmWithFullInfo(id);

            return View(new FilmView(film) { IsFav = user.HasFavourite(film) });
        }

        public ActionResult Delete([ModelBinder(typeof(UserModelBinder))] User user, Guid id)
        {
            var film = FilmsDao.GetFilmWithFullInfo(id);

            return View(new FilmView(film) { IsFav = user.HasFavourite(film)});
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
        public ActionResult Top100([ModelBinder(typeof(UserModelBinder))] User user)
        {
           return View(FilmsDao.FindAll()
                               .OrderByDescending(f => f.IMDbRating)
                               .Take(100)
                               .Select(f => new FilmView(f) { IsFav = user.HasFavourite(f) })
                               .ToList());
        }

        public ActionResult AddToFav([ModelBinder(typeof(UserModelBinder))] User user, Guid id)
        {
            var film = FilmsDao.Find(id);
            user.FavFilms.Add(film);

            var cookie = new HttpCookie(nameof(User))
            {
                Value = JsonConvert.SerializeObject(user),
                Expires = DateTime.Now.AddYears(1),
                Path = "/"
            };

            Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Films");
        }

        public ActionResult RemoveFromFav([ModelBinder(typeof(UserModelBinder))] User user, Guid id)
        {
            user.FavFilms.RemoveAll(f => f.ID == id);

            var cookie = new HttpCookie(nameof(User))
            {
                Value = JsonConvert.SerializeObject(user),
                Expires = DateTime.Now.AddYears(1),
                Path = "/"
            };

            Response.Cookies.Add(cookie);

            return RedirectToAction("Cabinet", "User");
        }
    }
}