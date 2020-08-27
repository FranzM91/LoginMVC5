using LoginMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User entity)
        {
            if (ModelState.IsValid)
            {
                using (DB_Entities db = new DB_Entities())
                {
                    var obj = db.Users.Where(f => f.FirstName.Equals(entity.FirstName) && f.Password.Equals(entity.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.IdUser.ToString();
                        Session["UserName"] = obj.FirstName.ToString();
                        return RedirectToAction("UserDashBoard");
                    }
                }
            }
            return View(entity);
        }
        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult ClearSession()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }
    }
}