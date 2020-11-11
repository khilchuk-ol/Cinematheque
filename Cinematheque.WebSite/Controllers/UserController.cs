using Cinematheque.Data.Models;
using Cinematheque.WebSite.ModelBinders;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;

namespace Cinematheque.WebSite.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Cabinet([ModelBinder(typeof(UserModelBinder))] User user)
        { 
            return View(user);
        }

        public ActionResult EditName([ModelBinder(typeof(UserModelBinder))] User user, string username)
        {
            if(!string.IsNullOrWhiteSpace(username))
            {
                user.Username = username;

                var cookie = new HttpCookie(nameof(User))
                {
                    Value = JsonConvert.SerializeObject(user),
                    Expires = DateTime.Now.AddYears(1),
                    Path = "/"
                };

                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Cabinet");
        }
    }
}