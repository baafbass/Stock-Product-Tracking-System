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
        Takip_SistemiEntities1 db = new Takip_SistemiEntities1();
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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Kullanici data)
        {
            db.Kullanici.Add(data);
            data.Rol = "User";
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