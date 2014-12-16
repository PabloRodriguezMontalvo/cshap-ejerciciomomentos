using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcMomentos.Models;


namespace MvcMomentos.Controllers
{
    [AllowAnonymous]
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Login()
        {
            return View(new Usuario());
        }
        [HttpPost]
        public ActionResult Login(Usuario modelo,String returnUrl="/")
        {
            if (Membership.ValidateUser(modelo.login, modelo.password))
            {
                FormsAuthentication.RedirectFromLoginPage(modelo.login,false);
                return null;
            }
            else
            {
                ModelState.AddModelError("","Login incorrecto");
                return View(modelo);
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
           return RedirectToAction("Login");
        }
    }
}