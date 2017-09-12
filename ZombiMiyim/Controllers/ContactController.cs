using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZombiMiyim.Models;

namespace ZombiMiyim.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/
        DbZombimiyimEntities db = new DbZombimiyimEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SendMessage(Contact c)
        {
            c.Time = DateTime.Now;
            try
            {
                db.Contact.Add(c);
                db.SaveChanges();
                return Json("Mesajınız Gönderildi..");
            }
            catch
            {
                return Json("Mesajınız Gönderilmedi...");
            }


        }



    }
}
