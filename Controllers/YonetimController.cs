using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using yazilim_ogrenme_blog.Models;
using yazilim_ogrenme_blog.Models.Managers;
using PagedList;
using PagedList.Mvc;
using yazilim_ogrenme_blog.ViewModel.Yonetim;

namespace yazilim_ogrenme_blog.Controllers
{
    [Authorize(Roles ="1")]
    public class YonetimController : Controller
    {

        YonetimModel veriler = new YonetimModel();
        DatabaseContext db = new DatabaseContext();
        


        // GET: Yonetim
       
        public ActionResult Index()
        {

            veriler.Kullanicilar = db.Kullanicilar.ToList();
            veriler.Kategoriler = db.Kategoriler.ToList();
            veriler.Yazilar = db.Yazilar.ToList();
            veriler.Yorumlar = db.Yorumlar.ToList();
            veriler.Ayarlar = db.Ayarlar.ToList();
            return View(veriler);

       
        }

        public ActionResult YaziListele(int sayfa = 1)
        {

            //  veriler.Yazilar = db.Yazilar.Where(m => m.Durum == true).ToList();
            var degerler = db.Yazilar.Where(m => m.Durum == true).OrderByDescending(c => c.ID).ToList().ToPagedList(sayfa, 10);

            return View(degerler);
        }



        //YAZI İŞLEMLERİ

        public ActionResult YeniYaziEkle()
        {
            List<SelectListItem> degerler = (from i in db.Kategoriler.Where(m=> m.Durum == true).ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Baslik,
                                                 Value = i.ID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult YeniYaziEkle(Yazilar p1, HttpPostedFileBase yaziResim,YaziEkleModel m1)
        {

           
             if (yaziResim != null && (yaziResim.ContentType == "image/jpeg" || yaziResim.ContentType == "image/jpg" || yaziResim.ContentType == "image/png"))
                {
                    string filename = $"yazi_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}.{yaziResim.ContentType.Split('/')[1]}";
                    yaziResim.SaveAs(Server.MapPath($"~/Images/Post/{filename}"));
                    p1.Resim = filename;

                try
                {
                    var ktg = db.Kategoriler.Where(m => m.ID == p1.Kategori.ID).FirstOrDefault();
                    p1.Kategori = ktg;
                    p1.Tarih = DateTime.Now;
                    p1.Onay = true;
                    p1.Kullanici = db.Kullanicilar.Where(m => m.Kullanici_ad == User.Identity.Name.ToString()).FirstOrDefault();
                    p1.Durum = true;
                    db.Yazilar.Add(p1);
                    int sonuc = db.SaveChanges();
                }
                catch (Exception)
                {
                    ViewBag.gelenHata = "Hay Aksi! Bir hata oluştu. Eksik alan bırakmayınız.";
                    List<SelectListItem> degerler = (from i in db.Kategoriler.Where(m => m.Durum == true).ToList()
                                                     select new SelectListItem
                                                     {
                                                         Text = i.Baslik,
                                                         Value = i.ID.ToString()

                                                     }).ToList();
                    ViewBag.dgr = degerler;

                    return View(m1);
                }



                return RedirectToAction("YaziListele");
            }
            else
            {
                ViewBag.gelenHata = "Hay Aksi! Bir hata oluştu. Eksik alan bırakmayınız.";
                List<SelectListItem> degerler = (from i in db.Kategoriler.Where(m => m.Durum == true).ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = i.Baslik,
                                                     Value = i.ID.ToString()

                                                 }).ToList();
                ViewBag.dgr = degerler;
                ViewBag.gelenHata = "Hatalı Resim yüklediniz. Lütfen <strong>jpeg, jpg, png</strong> formatları dışında yükleme denemeyiniz.";
                return View(m1);
            }

         
        }

        public ActionResult YaziGuncelle(int id)
        {

            List<SelectListItem> degerler = (from i in db.Kategoriler.Where(m => m.Durum == true).ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Baslik,
                                                 Value = i.ID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;


            var yazi = db.Yazilar.Find(id);
            return View("YaziGuncelle", yazi);

        }

        [HttpPost, ValidateInput(false)]
        public ActionResult YaziGuncelle(Yazilar p1, HttpPostedFileBase yaziResim, YaziEkleModel m1)
        {

            if(yaziResim != null)
            {

                if (yaziResim.ContentType == "image/jpeg" || yaziResim.ContentType == "image/jpg" || yaziResim.ContentType == "image/png")
                {
                    string filename = $"yazi_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}.{yaziResim.ContentType.Split('/')[1]}";
                    yaziResim.SaveAs(Server.MapPath($"~/Images/Post/{filename}"));


                    try
                    {

                        var yazi = db.Yazilar.Find(p1.ID);
                        var ktg = db.Kategoriler.Where(m => m.ID == p1.Kategori.ID).FirstOrDefault();
                        yazi.Resim = filename;
                        yazi.Baslik = p1.Baslik;
                        yazi.Icerik = p1.Icerik;
                        yazi.Kategori = ktg;
                        db.SaveChanges();

                    }
                    catch (Exception)
                    {
                        ViewBag.gelenHata = "Hay Aksi! Bir hata oluştu. Eksik alan bırakmayınız.";
                        List<SelectListItem> degerler = (from i in db.Kategoriler.Where(m => m.Durum == true).ToList()
                                                         select new SelectListItem
                                                         {
                                                             Text = i.Baslik,
                                                             Value = i.ID.ToString()

                                                         }).ToList();
                        ViewBag.dgr = degerler;

                        return View(m1);
                    }
                    return RedirectToAction("YaziListele");

                }
                else
                {

                    ViewBag.gelenHata = "Hay Aksi! Bir hata oluştu. Eksik alan bırakmayınız.";
                    List<SelectListItem> degerler = (from i in db.Kategoriler.Where(m => m.Durum == true).ToList()
                                                     select new SelectListItem
                                                     {
                                                         Text = i.Baslik,
                                                         Value = i.ID.ToString()

                                                     }).ToList();
                    ViewBag.dgr = degerler;
                    ViewBag.gelenHata = "Hatalı Resim yüklediniz. Lütfen <strong>jpeg, jpg, png</strong> formatları dışında yükleme denemeyiniz.";
                    return View(m1);
                }

            }
            else
            {

                    try
                    {

                        var yazi = db.Yazilar.Find(p1.ID);
                        var ktg = db.Kategoriler.Where(m => m.ID == p1.Kategori.ID).FirstOrDefault();
                        yazi.Baslik = p1.Baslik;
                        yazi.Icerik = p1.Icerik;
                        yazi.Kategori = ktg;
                        db.SaveChanges();

                    }
                    catch (Exception)
                    {
                        ViewBag.gelenHata = "Hay Aksi! Bir hata oluştu. Eksik alan bırakmayınız.";
                        List<SelectListItem> degerler = (from i in db.Kategoriler.Where(m => m.Durum == true).ToList()
                                                         select new SelectListItem
                                                         {
                                                             Text = i.Baslik,
                                                             Value = i.ID.ToString()

                                                         }).ToList();
                        ViewBag.dgr = degerler;

                        return View(m1);
                    }
                    return RedirectToAction("YaziListele");

                }
               

            }

        public ActionResult YaziSil(int id)
        {
            /*  var yrms = (from c in db.Yorumlar
                          where c.Yazi.ID == id
                          select c).Single();
              if(yrms != null)
              {
                  db.Yorumlar.Remove(yrms);
                  db.SaveChanges();
              }
             */
            try
            {
                var yrms = (from c in db.Yorumlar
                            where c.Yazi.ID == id
                            select c).ToList();
                if (yrms != null)
                {
                    foreach (var item in yrms)
                    {
                        db.Yorumlar.Remove(item);
                        db.SaveChanges();
                    }
                   
                }
            }
            catch (Exception)
            {

             
            }
            


            var yazi = db.Yazilar.Find(id);
            db.Yazilar.Remove(yazi);
            db.SaveChanges();
            return RedirectToAction("YaziListele");
        }

         


        //KATEGORİ İŞLEMLERİ

        public ActionResult KategoriListele()
        {
            veriler.Kategoriler = db.Kategoriler.Where(m=>m.Durum == true).ToList();
            return View(veriler);
        }

        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KategoriEkle(Kategoriler p1)
        {
            if (!ModelState.IsValid)
            {
                return View("KategoriEkle");
            }

            veriler.Kategoriler = db.Kategoriler.Where(m => m.Durum == true).ToList();

            foreach (var kategori in veriler.Kategoriler)
            {
                if(p1.Baslik == kategori.Baslik.ToString())
                {

                    ModelState.AddModelError("", p1.Baslik + " kategori adı kullanılıyor.");
                }
            }



            foreach (var item in ModelState)
            {
                if (item.Value.Errors.Count > 0)
                {
                    return View(p1);
                }
            }


            p1.Durum = true;
            db.Kategoriler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("KategoriListele");
        }

        public ActionResult KategoriSil(int id)
        {
            var kategori = db.Kategoriler.Find(id);
            kategori.Durum = false;
            db.SaveChanges();
            return RedirectToAction("KategoriListele");

        }

        public ActionResult KategoriGuncelle(int id)
        {


            var kategori = db.Kategoriler.Find(id);
            return View("KategoriGuncelle", kategori);
        }

        [HttpPost]
        public ActionResult KategoriGuncelle(Kategoriler p1)
        {
            var kategori = db.Kategoriler.Find(p1.ID);
            kategori.Baslik = p1.Baslik;
            db.SaveChanges();
            return RedirectToAction("KategoriListele");
        }



        // KULLANICI İŞLEMLERİ

        public ActionResult KullaniciListele(int sayfa =1)
        {
            // veriler.Kullanicilar = db.Kullanicilar.ToList();
            var degerler = db.Kullanicilar.OrderByDescending(c => c.ID).ToList().ToPagedList(sayfa,10);
            return View(degerler);
        }

        public ActionResult KullaniciGuncelle(int id)
        {
            var kullanici = db.Kullanicilar.Find(id);
            return View("KullaniciGuncelle", kullanici);
        }

        [HttpPost]
        public ActionResult KullaniciGuncelle(Kullanicilar p1)
        {
            var kullanici = db.Kullanicilar.Find(p1.ID);
            kullanici.Ad = p1.Ad;
            kullanici.Soyad = p1.Soyad;
            kullanici.Yetki = p1.Yetki;
            kullanici.Durum = p1.Durum;
            kullanici.Mail = p1.Mail;
            db.SaveChanges();

            return RedirectToAction("KullaniciListele");
        }

        public ActionResult KullaniciPasif(int id)
        {
            var kullanici = db.Kullanicilar.Find(id);
            kullanici.Durum = false;
            db.SaveChanges();
            return RedirectToAction("KullaniciListele");

        }

        public ActionResult KullaniciAktif(int id)
        {
            var kullanici = db.Kullanicilar.Find(id);
            kullanici.Durum = true;
            db.SaveChanges();
            return RedirectToAction("KullaniciListele");

        }



        // AYAR İŞLEMLERİİİ

        public ActionResult Ayarlar()
        {
            var ayar = db.Ayarlar.Find(1);
            return View("Ayarlar",ayar);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Ayarlar(Ayarlar p1)
        {
            var deger = db.Ayarlar.Find(1);
            deger.Hakkimizda = p1.Hakkimizda;
            db.SaveChanges();
            return RedirectToAction("Ayarlar");
        }

        

        //YORUM İŞLEMLERİİ

        public ActionResult Yorumlar(int sayfa = 1)
        {
            var yorumlar = db.Yorumlar.OrderBy(c => c.Durum).ToList().ToPagedList(sayfa, 10);
            return View(yorumlar);
        }

        public ActionResult OnayBekleyenYorumlar()
        {
            var onayyorum = db.Yorumlar.ToList();
            return View(onayyorum);
        }

        
        public ActionResult YorumAktif(int id)
        {
            var yorum = db.Yorumlar.Find(id);
            yorum.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Yorumlar");
        }


        
        public ActionResult YorumPasif(int id)
        {
            var yorum = db.Yorumlar.Find(id);
            yorum.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Yorumlar");

        }

  
        public ActionResult YorumSil(int id)
        {

            Yorumlar yorum = null;
            if(id != null)
            {
                yorum = db.Yorumlar.Where(m => m.ID == id).FirstOrDefault();
                if(yorum != null)
                {
                    db.Yorumlar.Remove(yorum);
                    db.SaveChanges();
                }

            }

            return RedirectToAction("Yorumlar");

        }



    }
}