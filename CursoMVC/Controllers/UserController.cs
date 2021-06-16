using CursoMVC.Infraestructure;
using CursoMVC.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CursoMVC.Controllers
{
    [CustomAuthenticationFilter]
    public class UserController : Controller
    {
        private UserDBContext db = new UserDBContext();

        // GET: User
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(UserModel model)
        {
            try
            {
                model.Perfil = "Nivel_1";
                db.Users.Add(model);
                db.SaveChanges();
                return RedirectToAction("ListUsers");
            }
            catch
            {
                return View();
            }
            
        }

        public ActionResult ListUsers()
        {
            var Users = from e in db.Users
                        orderby e.Cedula
                        select e;
            return View(Users);
        }

        public ActionResult EditUser(string id)
        {
            var User = db.Users.Single(x => x.Cedula == id);
            return View(User);
        }

        [HttpPost]
        public ActionResult EditUser(string cedula, FormCollection collection)
        {
            var User = db.Users.Single(x => x.Cedula == cedula);
            if (TryUpdateModel(User))
            {
                db.SaveChanges();
                return RedirectToAction("ListUsers");
            }

            return View(User);
        }

        [CustomAuthorize(roles:"Admin1234")]
        public ActionResult DeleteUser(string id)
        {
            var User = db.Users.Single(x => x.Cedula == id);
            return View(User);
        }

        [HttpPost]
        [CustomAuthorize(roles:"Admin1234")]
        public ActionResult DeleteUser(string id, FormCollection collection)
        {
            var User = db.Users.Single(x => x.Cedula == id);
            try
            {
                db.Users.Remove(User);
                db.SaveChanges();
                return RedirectToAction("ListUsers");
            }catch (Exception e)
            {
                return View();
            }
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model)
        {
            var LoginUser = (from e in db.Users
                             where model.Login == e.Cedula && model.Password == e.Password
                             select e).ToList();

            if(LoginUser.Count == 1)
            {
                Session.Timeout = 10;
                Session["Login"] = LoginUser[0].Cedula;
                Session["Nombre"] = LoginUser[0].Nombre;
                Session["Email"] = LoginUser[0].Email;
                Session["Perfil"] = LoginUser[0].Perfil;
                return RedirectToAction("ListUsers");
            }
            else
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrecta.");
                return View(model);
            }
        }

        
        public ActionResult LogOff()
        {
            Session["Login"] = string.Empty;
            Session["Nombre"] = string.Empty;
            Session["Email"] = string.Empty;
            Session["Perfil"] = string.Empty;

            return RedirectToAction("Login");
        }

        public ActionResult Unathorized()
        {
            return View();
        }
    }
}