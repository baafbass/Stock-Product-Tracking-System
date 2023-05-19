using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrunStokTakip.Models;

namespace UrunStokTakip.Controllers
{
    public class IstatistikController : Controller
    {
        Takip_SistemiEntities2 db = new Takip_SistemiEntities2();

        // GET: Istatistik
        public ActionResult Index()
        {
            var satis = db.Satislar.Count();
            ViewBag.satis = satis;
            var urun = db.Urun.Count();
            ViewBag.urun = urun;
            var kategori = db.Kategori.Count();
            ViewBag.Kategori = kategori;
            var sepet = db.Sepet.Count();
            ViewBag.sepet = sepet;
            var kullanici = db.Kullanici.Count();
            ViewBag.kullanici = kullanici;
            return View();
        }
    }
}