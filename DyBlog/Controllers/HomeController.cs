using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DyBlog.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Hakkimizda()
        {
            return View();
        }
        public ActionResult Galeri()
        {
            return View();
        }
        public ActionResult Kategoriler()
        {
            return View();
        }
        public ActionResult Iletisim()
        {
            return View();
        }
    }
}