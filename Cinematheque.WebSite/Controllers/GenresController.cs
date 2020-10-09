using Cinematheque.Data;
using Cinematheque.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinematheque.WebSite.Controllers
{
    public class GenresController : Controller
    {
        // GET: Genres
        public ActionResult Index()
        {
            return View(DataHolder.Genres.OrderBy(g => g.Name));
        }

        //GET: Genres/Films
        public ActionResult Films(Guid id)
        {
            var genre = DataHolder.Genres.Where(g => g.ID.Equals(id)).FirstOrDefault();

            return View(new GenreAndFilmsView
            {
                Films = genre?.FilmsOfGenre.Select(f => new FilmView(f)).OrderBy(f => f.Title),
                Genre = genre
            });
        }
    }
}