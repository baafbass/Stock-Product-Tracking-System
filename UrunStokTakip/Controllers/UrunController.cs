using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrunStokTakip.Models;

namespace UrunStokTakip.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Takip_SistemiEntities1 db = new Takip_SistemiEntities1();
        [Authorize]
        public ActionResult Index()
        {
            var List = db.Urun.ToList();
            return View(List);
        }
    }
}