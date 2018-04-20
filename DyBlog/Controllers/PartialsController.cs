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
    }
}