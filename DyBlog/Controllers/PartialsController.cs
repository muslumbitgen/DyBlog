using DyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DyBlog.Controllers
{
    public class PartialsController : Controller
    {
        DyBlogDB db = new DyBlogDB();
        // GET: Partials
        public ActionResult Kategori()
        {
            return View(db.Kategoris.ToList());
        }
        public ActionResult PopulerMakaleler()
        {

            return View(db.Makales.OrderByDescending(m => m.Okuma).Take(3));
        }
        public ActionResult SonGonderi()
        {
            return View(db.Makales.OrderByDescending(m => m.MakaleId).ToList().Take(3));
        }
        public ActionResult KategoriList()
        {
            return View(db.Kategoris.ToList());
        }
        public ActionResult SosyalMedya()
        {
            return View(db.SosyalMedyas.ToList());
        }
    }
}