using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UrunStokTakip.Models;

namespace UrunStokTakip.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        Takip_SistemiEntities2 db = new Takip_SistemiEntities2();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Kullanici kullanici)
        {
            var bilgiler = db.Kullanici.FirstOrDefault(x => x.Email == kullanici.Email && x.Sifre == kullanici.Sifre);
            if (bilgiler!=null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Email, false);
                Session["Mail"] = bilgiler.Email.ToString();
                Session["Ad"] = bilgiler.Ad.ToString();
                Session["Soyad"] = bilgiler.Soyad.ToString();
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                ViewBag.Hata = "Kullanici Adi ve Sifre Yanlış Girdiniz";
            }
            return View();
        }

        public ActionResult Guncelle()
        {
            var kullanicilar = (string)Session["Mail"];
            var degerler = db.Kullanici.FirstOrDefault(x => x.Email == kullanicilar);
            return View(degerler);
        }

        [HttpPost]
        public ActionResult Guncelle(Kullanici data)
        {
            var kullanicilar = (string)Session["Mail"];
            var user = db.Kullanici.Where(x => x.Email == kullanicilar).FirstOrDefault();
            db.KullaniciGuncelle(user.Id, data.Ad, data.Soyad, data.Email, data.KullaniciAd, data.Sifre, data.SifreTekrar);
            /*user.Ad = data.Ad;
            user.Soyad = data.Soyad;
            user.Email = data.Email;
            user.KullaniciAd = data.KullaniciAd;
            user.Sifre = data.Sifre;
            user.SifreTekrar = data.SifreTekrar;*/
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Kullanici data)
        {
            db.kullaniciEkle(data.Ad, data.Soyad, data.Email, data.KullaniciAd, data.Sifre,data.SifreTekrar);
            //data.Rol = "User";
            db.SaveChanges();
            return RedirectToAction("Login", "Account");
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}