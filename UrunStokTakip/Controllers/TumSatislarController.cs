using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrunStokTakip.Models;

namespace UrunStokTakip.Controllers
{
    public class TumSatislarController : Controller
    {
        Takip_SistemiEntities2 db = new Takip_SistemiEntities2();

        // GET: TumSatislar
        public ActionResult Index(int sayfa=1)
        {
            return View(db.Satislar.ToList().ToPagedList(sayfa,5)) ;
        }
    }
}