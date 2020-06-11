using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using yazilim_ogrenme_blog.Models;

namespace yazilim_ogrenme_blog.ViewModel.Yonetim
{
    public class YaziEkleModel
    {

        [DisplayName("Yazı Başlık"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(250, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Baslik { get; set; }

        [DisplayName("Yazı İçerik"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Icerik { get; set; }

        [DisplayName("Yazı Resim"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Resim { get; set; }

        public virtual Kategoriler Kategori { get; set; }



    }
}