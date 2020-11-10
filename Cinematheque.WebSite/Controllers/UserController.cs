using Cinematheque.Data.Dao;
using Cinematheque.Data.Dao.Impl;
using Cinematheque.Utils;
using Cinematheque.WebSite.Models;
using System.Web.Mvc;

namespace Cinematheque.WebSite.Controllers
{
    public class UserController : Controller
    {
        private IDaoUser UserDao { get; }

        public UserController(IDaoUser daoUser)
        {
            Validator.RequireNotNull(daoUser);

            UserDao = daoUser;
        }

        // GET: User
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult CompleteRegister(UserView user)
        {
            return View();
        }

        public ActionResult Authenticate()
        {
            return View();
        }

        public ActionResult CompleteAuthentication (UserView user)
        {
            return View();
        }
    }
}