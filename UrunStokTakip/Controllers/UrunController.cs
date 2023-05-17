//using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using UrunStokTakip.Models;

namespace UrunStokTakip.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Takip_SistemiEntities2 db = new Takip_SistemiEntities2();
        [Authorize]
        public ActionResult Index(string ara)
        {
            var List = db.Urun.ToList();
            if(!string.IsNullOrEmpty(ara))
            {
                List = List.Where(x => x.Ad.Contains(ara) || x.Aciklama.Contains(ara)).ToList();
            }
            return View(List);
        }
       // [Authorize(Roles = "Admin")]
        public ActionResult Ekle()
        {
            List<SelectListItem> deger1 = (from x in db.Kategori.ToList()

                                           select new SelectListItem
                                           {
                                               Text = x.Ad,
                                               Value = x.Id.ToString()
                                           }).ToList();
            ViewBag.ktgr = deger1;
            return View();
        }
       
       // [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Ekle(Urun data, HttpPostedFileBase File)
        {
            string path = Path.Combine("~/Content/Image" + File.FileName);
            File.SaveAs(Server.MapPath(path));
            data.Resim = File.FileName.ToString();
            db.Urun.Add(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       
       // [Authorize(Roles = "Admin")]
        public ActionResult Sil(int id =1)
        {
            var urun = db.Urun.Where(x => x.Id == id).FirstOrDefault();
            db.Urun.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       // [Authorize(Roles = "Admin")]
        public ActionResult Guncelle(int id=1)
        {
            var GuncelUrun = db.Urun.Where(x => x.Id == id).FirstOrDefault();
            List<SelectListItem> deger1 = (from x in db.Kategori.ToList()

                                           select new SelectListItem
                                           {
                                               Text = x.Ad,
                                               Value = x.Id.ToString()
                                           }).ToList();
            ViewBag.ktgr = deger1;
            return View(GuncelUrun);
        }

       // [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Guncelle(Urun model,HttpPostedFileBase File)
        {
            var urun = db.Urun.Find(model.Id);
            if (File==null)
            {
                urun.Ad = model.Ad;
                urun.Aciklama = model.Aciklama;
                urun.Fiyat = model.Fiyat;
                urun.Stok = model.Stok;

                urun.Populer = model.Populer;
                urun.KategoriId = model.KategoriId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                urun.Resim = File.FileName.ToString();
                urun.Ad = model.Ad;
                urun.Aciklama = model.Aciklama;
                urun.Fiyat = model.Fiyat;
                urun.Stok = model.Stok;
                urun.Populer = model.Populer;
                urun.KategoriId = model.KategoriId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        
        //[Authorize(Roles = "Admin")]
        public ActionResult KritikStok()
        {
            var kritik = db.Urun.Where(x => x.Stok <= 50).ToList();
            return View(kritik);
        }

        public PartialViewResult StokCount()
        {
            if(User.Identity.IsAuthenticated)
            {
                var count = db.Urun.Where(x => x.Stok < 50).Count();
                ViewBag.count = count;
                var azalan = db.Urun.Where(x => x.Stok == 50).Count();
                ViewBag.azalan = azalan;
            }
            return PartialView();
        }

        public ActionResult StokGrafik()
        {
            ArrayList deger1 = new ArrayList();
            ArrayList deger2 = new ArrayList();
            var veriler = db.Urun.ToList();
            veriler.ToList().ForEach(x => deger1.Add(x.Ad));
            veriler.ToList().ForEach(x => deger2.Add(x.Stok));
            var grafik = new Chart(width: 500, height: 500).AddTitle("Ürün-Stok-Grafiği").AddSeries(chartType: "Column", name: "Ad", xValue: deger1, yValues: deger2);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }
    }
}