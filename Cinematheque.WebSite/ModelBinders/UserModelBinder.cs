using Cinematheque.Data.Models;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace Cinematheque.WebSite.ModelBinders
{
    public class UserModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var cookie = controllerContext.HttpContext.Request.Cookies[bindingContext.ModelName];

            if(!string.IsNullOrEmpty(cookie?.Value))
            {
                try
                {
                    return JsonConvert.DeserializeObject<User>(cookie.Value);
                }
                catch(Exception e)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, e);
                }
            }

            return new User();
        }
    }
}