using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZombiMiyim.Models;

namespace ZombiMiyim.Controllers
{
    public class UserController : Controller
    {

        DbZombimiyimEntities db = new DbZombimiyimEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SingUp(User user)
        {
            var kul=(from u in db.User
                         where u.Mail==user.Mail
                         select u).SingleOrDefault();

            if (kul != null)
                return Json("Bu mail adresi ile daha önce bir kayıt yapılmıştır.");
            else
            {
                string c = "P";
                Random r = new Random();
                for (int i = 0; i < 5; i++)
                    c += r.Next().ToString();
              
              
               
                if (!Tools.MailGonder(user.Mail, "Zombi Miyim Mail Kontrolü", "MAİL AKTİVASYONU \n Üyeliğinizi aktive etmek için linke tıklayınız. " + Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/User/MailActivition?code=" + c))
                    return Json("Mail Gönderilemedi. Mail Alanı Yanlış...");
                else
                {
                    user.Code = c;
                    db.User.Add(user);
                    db.SaveChanges(); 
                    return Json("Kayıt başarılı.");
                }
                    
            }

        }
        [HttpPost]
        public JsonResult Login(string mail,string pass)
        {
            var kul = (from u in db.User
                       where u.Mail == mail && u.Password == pass 
                       select u).SingleOrDefault();
            if (kul == null || kul.Code[0]=='P')
                return Json("Hatalı kullanıcı adı veya şifre");

                
            else
            {
                HttpCookie Cookie1 = new HttpCookie("ZombiMiyimUserName",kul.Name);
                Cookie1.Expires = DateTime.Now.AddDays(1);
                HttpCookie Cookie2 = new HttpCookie("ZombiMiyimUserID", kul.ID.ToString());
                Cookie2.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(Cookie1);
                Response.Cookies.Add(Cookie2);
                Session.Add("ZombiMiyimUserName",kul.Name);

                return Json("Giriş Başarılı");
            }
        }
        [HttpPost]
        public JsonResult LogOut()
        {
            Response.Cookies["ZombiMiyimUserName"].Value = null;
            Response.Cookies["ZombiMiyimUserName"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["ZombiMiyimUserID"].Value = null;
            Response.Cookies["ZombiUserID"].Expires = DateTime.Now.AddDays(-1);

            HttpCookie aCookie = new HttpCookie("lastVisit");
            aCookie.Value = null;
            aCookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(aCookie);
            Session.RemoveAll(); 
            return Json("Çıkış yapıldı");
        }
        public ActionResult MailActivition(string code)
        {
            var kul = (from u in db.User
                       where u.Code == code
                       select u).SingleOrDefault();

            if(kul!=null)
            {
                string c = "A";
                Random r = new Random();
                for (int i = 0; i < 5; i++)
                    c += r.Next().ToString();

                kul.Code = c;

                db.Entry(kul).State = EntityState.Modified;
                db.SaveChanges();

            }

            return View();
        }
    }
}
