﻿using System;
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
        Takip_SistemiEntities db = new Takip_SistemiEntities();
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
            var bilgiler = db.Kullanicis.FirstOrDefault(x => x.Email == kullanici.Email && x.Sifre == kullanici.Sifre);
            if (bilgiler!=null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Email, false);
                Session["Mail"] = bilgiler.Email.ToString();
                Session["Ad"] = bilgiler.Ad.ToString();
                Session["Soyad"] = bilgiler.Ad.ToString();
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
            db.Kullanicis.Add(data);
            data.Rol = "User";
            db.SaveChanges();
            return RedirectToAction("Login", "Account");
        }








    }
}