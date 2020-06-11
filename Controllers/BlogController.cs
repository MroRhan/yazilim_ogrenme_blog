using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using yazilim_ogrenme_blog.Models;
using yazilim_ogrenme_blog.Models.Managers;
using yazilim_ogrenme_blog.ViewModel.Anasayfa;
using yazilim_ogrenme_blog.ViewModel.Blog;

namespace yazilim_ogrenme_blog.Controllers
{
    public class BlogController : Controller
    {
        BlogModel veriler = new BlogModel();
        DatabaseContext db = new DatabaseContext();


        


        // GET: Blog
        public ActionResult Index(int sayfa = 1)
        {
           
            //  veriler.Yazilar = db.Yazilar.Where(m => m.Durum == true).ToList();
            var degerler = db.Yazilar.Where(m => m.Durum == true).OrderByDescending(c => c.ID).ToList().ToPagedList(sayfa, 10);
         
            return View(degerler);
        }

        public ActionResult Detay(int id)
        {
            Yazilar yazi = db.Yazilar.Find(id);
            if (yazi != null)
            {
                return View(yazi);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Detay(int id, BlogModel p1,YorumModel model)
        {
            Yazilar yazi = db.Yazilar.Find(id);
            if (ModelState.IsValid)
            {

                foreach (var item in ModelState)
                {
                    if (item.Value.Errors.Count > 0)
                    {
                        return View(model);
                    }
                }

                Yorumlar y = new Yorumlar();
                y.Icerik = model.Icerik;
                y.Durum = false;
                y.Kulanici = db.Kullanicilar.Where(m => m.Kullanici_ad == User.Identity.Name.ToString()).FirstOrDefault();
                var yzi = db.Yazilar.Where(m => m.ID == id).FirstOrDefault();
                y.Yazi = yzi;

                db.Yorumlar.Add(y);
                int sonuc = db.SaveChanges();

                if (sonuc > 1)
                {
                    TempData["message"] = "Yorumunuz gönderildi, Kontrol edildikten sonra yayınlanacaktır.";
                }
                //        return RedirectToAction("Detay", "Blog", y1.ID);


            }
          
            return View(yazi);


            //      return RedirectToAction("Detay", "Blog", y1.ID);

        }


        public ActionResult Kategori(int id, int sayfa = 1)
        {
            veriler.Kullanicilar = db.Kullanicilar.ToList();
            veriler.Kategoriler = db.Kategoriler.ToList();
            veriler.Yazilar = db.Yazilar.Where(c => c.Kategori.ID == id).ToList();
            veriler.Yorumlar = db.Yorumlar.ToList();
            veriler.Ayarlar = db.Ayarlar.ToList();

            var degerler = db.Yazilar.Where(m => m.Durum == true && m.Kategori.ID == id).OrderByDescending(c => c.ID).ToList().ToPagedList(sayfa, 10);

            return View(degerler);
        }

        public ActionResult Ara(string s, int sayfa = 1)
        {
            if(s == "")
            {
                return RedirectToAction("/Index");
            }
            else
            {
                var degerler = db.Yazilar.Where(m => m.Durum == true && (m.Baslik.Contains(s) || m.Icerik.Contains(s))).OrderByDescending(c => c.ID).ToList().ToPagedList(sayfa, 10);

                return View(degerler);
            }
           

        }

    }
}