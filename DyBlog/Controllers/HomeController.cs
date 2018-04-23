using DyBlog.Models;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DyBlog.Controllers
{
    public class HomeController : Controller
    {
        DyBlogDB db = new DyBlogDB();
        // GET: Home
        public ActionResult Index(int page=1)
        {
            var makale = db.Makales.OrderByDescending(m => m.MakaleId).ToPagedList(page,5);
            return View(makale);
        }

        public ActionResult BlogAra(string Ara=null)
        {
            var aranan = db.Makales.Where(m => m.Baslik.Contains(Ara)).ToList();
            return View(aranan.OrderByDescending(m=>m.Tarih));
        }
      
        public ActionResult MakaleDetay(int id)
        {
            var makales = db.Makales.Where(m => m.MakaleId == id).SingleOrDefault();
            if (makales==null)
            {
                return HttpNotFound();
            }

            return View(makales);
        }
        public ActionResult KategoriMakale(int id)
        {
            var katego = db.Makales.Where(m => m.Kategori.KategoriId == id).ToList();
            if (katego == null)
            {
                return HttpNotFound();
            }

            return View(katego);
        }
        public ActionResult Hakkimizda()
        {
            return View();
        }
        public ActionResult Galeri()
        {
            return View();
        }
        public ActionResult Iletisim()
        {
            return View();
        }
        
        public JsonResult YorumYap(string yorum,int makaleId)
        {
            var uyeid = Session["uyeid"];
            if (yorum==null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            db.Yorums.Add(new Yorum { UyeId = Convert.ToInt32(uyeid), MakaleId = makaleId, Icerik = yorum, Tarih = DateTime.Now });
            db.SaveChanges();
            return Json(false,JsonRequestBehavior.AllowGet);
        }
        public ActionResult YorumSil(int id)
        {
            var uyeid = Session["uyeid"];
            var yorum = db.Yorums.Where(y => y.YorumId == id).SingleOrDefault();
            var makale = db.Makales.Where(m => m.MakaleId == yorum.MakaleId).SingleOrDefault();
            if (yorum.UyeId==Convert.ToInt32(uyeid))
            {
                db.Yorums.Remove(yorum);
                db.SaveChanges();
                return RedirectToAction("MakaleDetay","Home",new { id=makale.MakaleId});
            }
            else
            {
                return HttpNotFound();
            }
            
        }
        public ActionResult OkumaSayisi(int Makaleid)
        {
            var makale = db.Makales.Where(m => m.MakaleId == Makaleid).SingleOrDefault();
            makale.Okuma+=1;
            db.SaveChanges();
            return View();
        }

    }
}