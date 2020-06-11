using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace yazilim_ogrenme_blog.Models.Managers
{
    public class DatabaseContext : DbContext
    {

        public DbSet<Kullanicilar> Kullanicilar { get; set; }
        public DbSet<Yazilar> Yazilar { get; set; }
        public DbSet<Kategoriler> Kategoriler { get; set; }
        public DbSet<Yorumlar> Yorumlar { get; set; }
        public DbSet<Ayarlar> Ayarlar { get; set; }
        public DbSet<Iletisim> Iletisim { get; set; }


        public DatabaseContext()
        {
            Database.SetInitializer(new VeritabaniOlustur());
        }


    }

    public  class VeritabaniOlustur : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {

            // Kullanıcı Ekleme
            for (int i = 0; i < 10; i++)
            {
                Kullanicilar kullanici = new Kullanicilar();
                kullanici.Kullanici_ad = FakeData.NameData.GetSurname();
                kullanici.Ad = FakeData.NameData.GetFirstName();
                kullanici.Soyad = FakeData.NameData.GetSurname();
                kullanici.Mail = FakeData.NetworkData.GetEmail();
                kullanici.Sifre = "123456";
                kullanici.Yetki = 0;
                kullanici.Durum = true;
                kullanici.Uyelik_Tarih = FakeData.DateTimeData.GetDatetime();
                context.Kullanicilar.Add(kullanici);
            }

            Kullanicilar admin = new Kullanicilar();
            admin.Kullanici_ad = "admin";
            admin.Ad = "Orhan";
            admin.Soyad = "ÖZTOP";
            admin.Mail = "orhan7874@hotmail.com";
            admin.Sifre = "123456";
            admin.Yetki = 1;
            admin.Durum = true;
            admin.Uyelik_Tarih = DateTime.Now;
            context.Kullanicilar.Add(admin);

            context.SaveChanges();



        
            //Kategori Ekleme
            for (int i = 0; i < 10; i++)
            {
                Kategoriler kategori = new Kategoriler();
                kategori.Baslik = FakeData.TextData.GetAlphabetical(15).ToUpper();
                kategori.Durum = true;

                context.Kategoriler.Add(kategori);
            }

            context.SaveChanges();


            //Ayarlar Default Ekleme
            Ayarlar ayar = new Ayarlar();
            ayar.Hakkimizda = "";
            context.Ayarlar.Add(ayar);
            context.SaveChanges();

             //Yazı Ekleme
            List<Kullanicilar> tumKullanicilar = context.Kullanicilar.ToList();
            List<Kategoriler> tumKategoriler = context.Kategoriler.ToList();


            foreach (Kullanicilar kullanici in tumKullanicilar)
            {
                for (int i = 0; i < FakeData.NumberData.GetNumber(1,5); i++)
                {
                    Yazilar yazi = new Yazilar();
                    yazi.Baslik = FakeData.TextData.GetSentences(1);
                    yazi.Icerik = FakeData.TextData.GetSentences(350);
                    yazi.Onay = true;
                    yazi.Durum = true;
                    yazi.Tarih = FakeData.DateTimeData.GetDatetime();
                    yazi.Kullanici = kullanici;
                    yazi.Resim = "https://i.picsum.photos/id/"+ FakeData.NumberData.GetNumber(0,1000) +"/700/500.jpg";
                    yazi.Kategori = tumKategoriler[FakeData.NumberData.GetNumber(0,9)];
                    context.Yazilar.Add(yazi);
                }
        
            }

            context.SaveChanges();

            
        }
    }

}