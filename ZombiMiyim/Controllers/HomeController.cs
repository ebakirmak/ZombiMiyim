using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZombiMiyim.Models;

namespace ZombiMiyim.Controllers
{
    public class HomeController : Controller
    {
       
        DbZombimiyimEntities db = new DbZombimiyimEntities();
        public ActionResult Index()
        {
            if (Session["ZombiMiyimUserName"] == null)
                ViewBag.Logined = null;
            else
                ViewBag.Logined = Session["ZombiMiyimUserName"].ToString();
            return View();

        }
        [HttpPost]
        public JsonResult linkCodeMessage(User user)
        {
            int ID=0;
            Random rand = new Random();
            int rast=rand.Next(10000000,99999999);
            ID = Convert.ToInt32(Request.Cookies["ZombiMiyimUserID"].Value);
            user= (from u in db.User where u.ID == ID select u).SingleOrDefault();

            if(user!=null)
            {
                user.Code = "A"+rast;
                db.SaveChanges();
                return Json("A"+rast);
            }
            else
            {
                return Json("Uye yok");
            }   
        }

    }
}
