using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace yazilim_ogrenme_blog.Models
{
    [Table("Yazilar")]
    public class Yazilar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DisplayName("Başlık"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(250, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Baslik { get; set; }

        [DisplayName("İçerik"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Icerik { get; set; }
 

        public DateTime Tarih { get; set; }

        [Required]
        public Boolean Onay { get; set; }


        public Boolean Durum { get; set; }

        [DisplayName("Yazı Resmi"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Resim { get; set; }

        public virtual Kategoriler Kategori { get; set; }
        public virtual Kullanicilar Kullanici { get; set; }
        public virtual List<Yorumlar> Yorumlar { get; set; }

    }
}