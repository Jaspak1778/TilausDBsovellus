using MVC_TKsovellus_1001.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MVC_TKsovellus_1001.Controllers
{

    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            
            ViewBag.Message = "Yhtiön perustietojen kuvailua";
            ViewBag.Herja = "Ota yhteyttä!";

            return View();
            
        }

        public ActionResult Contact()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                ViewBag.Message = "Yhteystietojamme";
                ViewBag.UserName = Session["UserName"];
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(Logins LoginModel)
        {
            TilauksetEntities db = new TilauksetEntities();
            //Haetaan käyttäjän/Loginin tiedot annetuilla tunnustiedoilla tietokannasta LINQ -kyselyllä
            var LoggedUser = db.Logins.SingleOrDefault(x => x.UserName == LoginModel.UserName && x.PassWord == LoginModel.PassWord);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Successfull login";
                ViewBag.LoggedStatus = "In";
                Session["UserName"] = LoggedUser.UserName;
                return RedirectToAction("Index", "Home"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa --> Home/Index
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessfull";
                ViewBag.LoggedStatus = "Out";
                LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";
                return View("Login", LoginModel);
            }
        }
  
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Endsession", "Home"); //Uloskirjautumisen jälkeen pääsivulle
        }
        public ActionResult Endsession()
        {

            Session.Clear();
            ViewBag.LoggedOut = "Olet kirjautunut ulos järjestelmästä.";
            return View();
        }
    }
}