using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrunStokTakip.Models;

namespace UrunStokTakip.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class KategoriController : Controller
    {
        // GET: Kategori
        Takip_SistemiEntities1 db = new Takip_SistemiEntities1();
       
        public ActionResult Index()
        {
            return View(db.Kategori.Where(x=>x.Durum==true).ToList());
        }

        
        public ActionResult Ekle()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Ekle(Kategori ktgr)
        {
            db.Kategori.Add(ktgr);
            ktgr.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
   
        public ActionResult Sil(int id = 1)
        {
            var kategori = db.Kategori.Where(x => x.Id == id).FirstOrDefault();
            db.Kategori.Remove(kategori);
            kategori.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Guncelle(Kategori data)
        {
            var guncelle = db.Kategori.Where(x => x.Id == data.Id).FirstOrDefault();
            guncelle.Aciklama = data.Aciklama;
            guncelle.Ad = data.Ad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}