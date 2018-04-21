using DyBlog.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace DyBlog.Controllers
{
    public class UyeController : Controller
    {
        // GET: Uye
        DyBlogDB db = new DyBlogDB();
        public ActionResult Index(int id)
        {
            var uye = db.Uyes.Where(u => u.UyeId == id).SingleOrDefault();
            if (Convert.ToInt32(Session["uyeid"])!=uye.UyeId)
            {
                return HttpNotFound();
            }
            return View(uye);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost, AllowAnonymous]
        public ActionResult Login(FormCollection form)
        {
            string username = form["kullaniciAdi"].ToString();
            string password = form["sifre"].ToString();
            Uye user = db.Uyes.Where(x => x.KullaniciAdi == username && x.Sifre == password).FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.KullaniciAdi, false);
                Session["AdSoyad"] = user.AdSoyad;
                Session["kullaniciAdi"] = user.KullaniciAdi;
                Session["uyeid"] = user.UyeId;
                Session["YetkiId"] = user.YetkiId;
                TempData["Message"] = Alert("Giriş Başarılı", true);

                return RedirectToAction("Index", "Home");
            }
            TempData["Message"] = Alert("Hatalı giriş yaptınız. Lütfen Kullanıcı Adı veya Şifrenizi Kontrol Ediniz!", false);
            return View();
        }
       
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["uyeid"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Uye uye,HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                if (Foto != null)
                {
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(150, 150);
                    img.Save("~/Uploads/UyeFoto/" + newfoto);
                    uye.Foto = "/Uploads/UyeFoto/" + newfoto;
                    uye.YetkiId = 2;
                    db.Uyes.Add(uye);
                    db.SaveChanges();
                    Session["uyeid"] = uye.UyeId;
                    Session["kullaniciAdi"] = uye.KullaniciAdi;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Fotoğraf", "Fotoğraf Seçiniz");
                }
            }
            return View(uye);
        }

        public ActionResult Edit(int id)
        {
            var uye = db.Uyes.Where(u => u.UyeId == id).SingleOrDefault();
            if (Convert.ToInt32(Session["uyeid"]) != uye.UyeId)
            {
                return HttpNotFound();
            }
            return View(uye);
        }
        [HttpPost]
        public ActionResult Edit(Uye uye,int id,HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                var uyes = db.Uyes.Where(u => u.UyeId == id).SingleOrDefault();
                if (Foto!=null)
                {
                    if (System.IO.File.Exists(Server.MapPath(uyes.Foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(uyes.Foto));
                    }
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/UyeFoto/" + newfoto);
                    uyes.Foto = "/Uploads/UyeFoto/" + newfoto;
                }
                uyes.AdSoyad = uye.AdSoyad;
                uyes.Email = uye.Email;
                uyes.KullaniciAdi = uye.KullaniciAdi;
                uyes.Sifre = uye.Sifre;
                db.SaveChanges();
                Session["kullaniciAdi"] = uye.KullaniciAdi;
                return RedirectToAction("Index","Home",new { id=uyes.UyeId});
            }
            return View();
        }
        public string Alert(string message, bool? type = null)
        {
            string tip;
            switch (type)
            {
                case false: tip = "danger"; break;
                case true: tip = "success"; break;
                default:
                    tip = "info";
                    break;
            }
            string msg = "<div class='alert alert-" + tip + " alert-dismissible'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>" + message + "</div>";
            return msg;
        }
    }
}