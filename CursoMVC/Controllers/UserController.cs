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

        public ActionResult DeleteUser(string id)
        {
            var User = db.Users.Single(x => x.Cedula == id);
            return View(User);
        }

        [HttpPost]
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

    }
}