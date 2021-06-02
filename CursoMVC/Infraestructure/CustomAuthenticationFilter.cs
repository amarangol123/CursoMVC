using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace CursoMVC.Infraestructure
{
    public class CustomAuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext) 
        {
            if(string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["Login"])))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        /* Automaticamente llama al challenge después de llamar el primer metodo
        */

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if(filterContext == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"controller", "User"},
                        {"action", "Login"}
                    }
                );
            }
        }
    }
}
