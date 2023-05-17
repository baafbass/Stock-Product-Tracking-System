 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrunStokTakip.Models;
using PagedList;
using PagedList.Mvc;

namespace UrunStokTakip.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        Takip_SistemiEntities2 db = new Takip_SistemiEntities2();
        public ActionResult Index(int sayfa=1)
        {
            if(User.Identity.IsAuthenticated)
            {
                var kullaniciadi = User.Identity.Name;
                var kullanici = db.Kullanici.FirstOrDefault(x => x.Email == kullaniciadi);
                var model = db.Satislar.Where(x => x.kullaniciId == kullanici.Id).ToList().ToPagedList(sayfa, 5);
                return View(model);
            }
            return HttpNotFound();
        }

        public ActionResult SatinAl(int id=1)
        {
            var model = db.Sepet.FirstOrDefault(x => x.Id == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult SatinAl2(int id=1)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var model = db.Sepet.FirstOrDefault(x => x.Id == id);
                    var satis = new Satislar
                    {
                        kullaniciId = model.KullaniciId,
                        UrunId = model.UrunId,
                        Adet = model.Adet,
                        Resim = model.Resim,
                        Fiyat = model.Fiyat,
                        Tarih = model.Tarih,
                    };
                    db.Sepet.Remove(model);
                    db.Satislar.Add(satis);
                    db.SaveChanges();
                    ViewBag.islem = "Satın alma işlemi başarılı bir şekilde gerçekleştirmiştir.";
                }
            }
            catch (Exception)
            {

                ViewBag.islem = "Satın Alma işlemi Başarısızdır";
            }
            return View("islem");
        }
    }
}