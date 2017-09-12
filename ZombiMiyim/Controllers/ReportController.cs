using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZombiMiyim.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/

        public ActionResult Index()
        {

            if (Session["ZombiMiyimUserName"] == null)
                ViewBag.Logined = null;
            else
                ViewBag.Logined = Session["ZombiMiyimUserName"].ToString();


            ViewBag.Message = Session["ZombiMiyimUserName"];
            return View();
        }

      

    }
}
