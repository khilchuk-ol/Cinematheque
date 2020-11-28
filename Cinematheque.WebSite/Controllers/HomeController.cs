using Cinematheque.Data.Dao;
using Cinematheque.WebSite.Models;
using Cinematheque.WebSite.Models.Display;
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
        public ActionResult Index()
        {
            var info = new FilmsSearchInfoBase()
            {
                Genres = GenresDao.FindAll().OrderBy(g => g.Name),
                Actors = ActorsDao.FindAll().Select(a => new ActorView(a)).OrderBy(a => a.FullName).ToList()
            };
            return View(info);
        }

        //GET: Home/About
        public ActionResult About()
        {            
            return View();
        }
    }
}