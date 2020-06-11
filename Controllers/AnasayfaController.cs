using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using yazilim_ogrenme_blog.Models;
using yazilim_ogrenme_blog.Models.Managers;
using yazilim_ogrenme_blog.ViewModel.Anasayfa;

namespace yazilim_ogrenme_blog.Controllers
{
    public class AnasayfaController : Controller
    {
        AnasayfaModel  veriler = new AnasayfaModel();
        DatabaseContext db = new DatabaseContext();


        // GET: Anasayfa
        public ActionResult Index()
        {  
            veriler.Kullanicilar = db.Kullanicilar.ToList();
            veriler.Kategoriler = db.Kategoriler.ToList();
            veriler.Yazilar = db.Yazilar.ToList();
            veriler.Yorumlar = db.Yorumlar.ToList();
            veriler.Ayarlar = db.Ayarlar.ToList();

            return View(veriler);
        }

        public ActionResult GirisYap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GirisYap(GirisYapModel model)
        {

            if (ModelState.IsValid)
            {
                veriler.Kullanicilar = db.Kullanicilar.Where(m => m.Durum == true).ToList();
                int mevcut = 0;
                string userId ="";
                foreach (var kullanici in veriler.Kullanicilar)
                {
                    if (model.Kullanici == kullanici.Kullanici_ad.ToString() && model.Parola == kullanici.Sifre.ToString())
                    {
                        
                        mevcut++;
                        userId = kullanici.ID.ToString();
                    }
                }

                if (mevcut > 0)
                {
                    FormsAuthentication.SetAuthCookie(model.Kullanici,false);
                    return RedirectToAction("Index");

                }
                else
                {
                    ModelState.AddModelError("", model.Kullanici + " Hatalı Giriş ya da Onaylanmamış Hesap");
                }


                foreach (var item in ModelState)
                {
                    if (item.Value.Errors.Count > 0)
                    {
                        return View(model);
                    }
                }


            }

            return View();
        }

        public ActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KayitOl(KayitOlModel model)
        {

            if (ModelState.IsValid)
            {
                
               veriler.Kullanicilar = db.Kullanicilar.ToList();

                foreach (var kullanici in veriler.Kullanicilar)
                {
                    if (model.Kullanici == kullanici.Kullanici_ad.ToString() )
                    {
                        ModelState.AddModelError("", model.Kullanici + " kullanıcı adı kullanılıyor.");

                    }

                    if (model.Email == kullanici.Mail.ToString())
                    {
                        ModelState.AddModelError("", model.Email + "  E-posta adresi kullanılıyor.");

                    }

                }

 
                 

                foreach (var item in ModelState)
                {
                    if(item.Value.Errors.Count > 0)
                    {
                        return View(model);
                    }
                }



                Kullanicilar k = new Kullanicilar();
                
                k.Kullanici_ad = model.Kullanici;
                k.Ad = model.Ad;
                k.Soyad = model.Soyad;
                k.Mail = model.Email;
                k.Sifre = model.Parola;
                k.Uyelik_Tarih = DateTime.Now;
                k.Yetki = 0;

                 
                db.Kullanicilar.Add(k);
               int sonuc = db.SaveChanges();

                if(sonuc > 0)
                {
                    return RedirectToAction("KayitOk");
                }
                
               

    
            }

            return View(model);
        }

        public ActionResult KayitOk()
        {
            return View();
        }

        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap");
        }

        public ActionResult Hakkimizda()
        {
            var ayarlar = db.Ayarlar.ToList();
            return View(ayarlar);
        }

        public ActionResult Iletisim()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Iletisim(IletisimModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in ModelState)
                {
                    if (item.Value.Errors.Count > 0)
                    {
                        return View(model);
                    }
                }

                Iletisim i = new Iletisim();
                i.Ad = model.Ad;
                i.Soyad = model.Soyad;
                i.Mail = model.Email;
                i.Icerik = model.Mesaj;
                db.Iletisim.Add(i);
                int sonuc = db.SaveChanges();

                if(sonuc > 0)
                {
 TempData["message"] = "<div class='alert alert-warning alert - dismissible fade show' role='alert'> Mesajınız başarılıyla iletildi. <br> En kısa sürede cevaplamaya çalışacağız.<button type = 'button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span>  </button></div>";

                }
            }

            return View(model);
        }

        public ActionResult Blog()
        {
            var blog = db.Yazilar.ToList();
            return View(blog);
        }

     

    }
}