using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcMomentos.Seguridad;

namespace MvcMomentos
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_PostAuthenticateRequest(object sender,
            EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                var identity =
                    new ProveedorIdentidad(HttpContext.Current.User.Identity);
                var principal = new PrincipalPersonalizado(identity);
                HttpContext.Current.User = principal;

            }
        }
    }
}
