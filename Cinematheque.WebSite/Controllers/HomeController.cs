using System.Web.Mvc;

namespace Cinematheque.WebSite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //GET: Home/About
        public ActionResult About()
        {            
            return View();
        }
    }
}