using Cinematheque.Data.Dao;
using Cinematheque.Data.Models;
using Cinematheque.WebSite.ModelBinders;
using Cinematheque.WebSite.Models;
using Cinematheque.WebSite.Models.Display;
using Cinematheque.WebSite.Models.InfoContainers;
using System.Linq;
using System.Web.Mvc;

namespace Cinematheque.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private IDaoFilm FilmsDao { get; }

        private IDaoGenre GenresDao { get; }

        private IDaoActor ActorsDao { get; }

        public HomeController(IDaoFilm filmsDao, IDaoGenre genresDao, IDaoActor actorsDao)
        {
            FilmsDao = filmsDao;
            GenresDao = genresDao;
            ActorsDao = actorsDao;
            //FilmsDbEmulXml.Films = FilmsDao.FindAll();
            //FilmsDbEmulXml.Serialize();
        }

        // GET: Home
        public ActionResult Index([ModelBinder(typeof(UserModelBinder))] User user)
        {
            var info = new FilmsSearchInfoBase()
            {
                Genres = GenresDao.FindAll()
                                  .OrderBy(g => g.Name),
                Actors = ActorsDao.FindAll()
                                  .Select(a => new ActorView(a))
                                  .OrderBy(a => a.FullName)
                                  .ToList(),
                FilmsDefaultView = FilmsDao.FindAllWithGenres()
                                           .Select(f => new FilmView(f) { IsFav = user.HasFavourite(f.ID) })
                                           .OrderByDescending(f => f.ReleaseDate)
            };
            return View(info);
        }


        // GET: Home/Search
        public ActionResult Search(FilmsSearchInfoContainer info)
        {
            var settings = new FilmsSearchSettings()
            {
                IncludeGenresIDs = info.IncludeGenres,
                IncludeActorsIDs = info.IncludeActors,
                ExcludeActorsIDs = info.ExcludeActors,
                ExcludeGenresIDs = info.ExcludeGenres,
                Query = info.Query
            };

            return View(FilmsDao.SearchFilmsWithSettings(settings)
                                .Select(f => new FilmView(f))
                                .OrderBy(f => f.Title));
        }

        //GET: Home/About
        public ActionResult About()
        {            
            return View();
        }
    }
}