using CursoMVC.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CursoMVC.Controllers
{
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


    }
}