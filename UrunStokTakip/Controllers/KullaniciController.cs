using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using UrunStokTakip.Models;

namespace UrunStokTakip.Controllers
{
   
    public class KullaniciController : Controller
    {
        Takip_SistemiEntities2 db = new Takip_SistemiEntities2();

        // GET: Kullanici
        [Authorize]
        public ActionResult Index()
        {
            var kullanicilar = db.KullaniciView.ToList();
            return View(kullanicilar);
        }

        public ActionResult SifreReset()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult SifreReset(string eposta)
        {
            var mail = db.Kullanici.Where(x => x.Email == eposta).SingleOrDefault();
            if(mail!=null)
            {
                Random rnd = new Random();
                int yenisifre = rnd.Next();
                Kullanici sifre = new Kullanici();
                mail.Sifre = Crypto.Hash(Convert.ToString(yenisifre), "MD5");
                db.SaveChanges();
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "abdoulfaridbassirou7898@gmail.com";
                WebMail.Password = "787898";
                WebMail.SmtpPort = 587;
                WebMail.Send(eposta, "Giriş Şifreniz", "Şifreniz:" + yenisifre);
                ViewBag.uyari = "Şifreniz Başarıyla Gönderilmiştir";
            }
            else
            {
                ViewBag.uyari = "Hata Oluştu Tekrar Deneyiniz";
            }
            return View();
        }
    }
}