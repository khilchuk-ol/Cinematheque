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
    public class GenresController : Controller
    {
        private IDaoGenre GenresDao { get; }

        private IDaoFilm FilmsDao { get; }

        public GenresController(IDaoGenre genreDao, IDaoFilm filmsDao)
        {
            Validator.RequireNotNull(genreDao);
            Validator.RequireNotNull(filmsDao);

            GenresDao = genreDao;
            FilmsDao = filmsDao;
        }

        // GET: Genres
        public ActionResult Index()
        {
            return View(GenresDao.FindAll().OrderBy(g => g.Name));
        }

        public ActionResult Search(string q)
        {
            var genres =GenresDao.SearchGenresByName(q);
            return View(genres.OrderBy(f => f.Name));
        }

        //GET: Genres/Films
        public ActionResult Films(Guid id)
        {
            var genre = GenresDao.GetGenreWithFilms(id);

            return View(new GenreAndFilmsView
            {
                Films = genre?.Films.Select(f => new FilmView(f)).OrderBy(f => f.Title),
                Genre = genre
            });
        }

        public ActionResult Create()
        {
            return View(new GenreInfoContainer()
            {
                AvailableFilms = FilmsDao.FindAll()
            });
        }

        public ActionResult DoCreate(GenreInput input)
        {
            var genre = input.CreateGenre(FilmsDao);
            GenresDao.Add(genre);

            return RedirectToAction("Index");
        }
    }
}