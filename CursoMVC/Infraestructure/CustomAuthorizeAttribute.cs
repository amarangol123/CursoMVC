using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CursoMVC.Infraestructure
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedRoles;

        public CustomAuthorizeAttribute (params string[] roles)
        {
            this.allowedRoles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // True significa que esta autorizado, false no esta autorizado
            var userRole = Convert.ToString(httpContext.Session["Perfil"]);
            foreach(var role in allowedRoles)
            {
                if(role == userRole)
                {
                    return true;
                }
            }
            return false;
        }

        protected override HandleUnauthorizeRequest(AuthorizationContext filterContext)
        {
            // Manejador de casos no autorizados (devuelve false en el metodo AuthorizeCore)
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary{
                    {"controller", "User"},
                    {"action", "Unauthorized"}
                }
            );
        }

    }
}