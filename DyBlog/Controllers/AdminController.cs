using DyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DyBlog.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        DyBlogDB db = new DyBlogDB();
        public ActionResult Index()
        {
            ViewBag.makaleSayisi = db.Makales.Count();
            ViewBag.yorumSayisi = db.Yorums.Count();
            ViewBag.kategoriSayisi = db.Kategoris.Count();
            ViewBag.uyeSayisi = db.Uyes.Count();

            return View();
        }
    }
}