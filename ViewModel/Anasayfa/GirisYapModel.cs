using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace yazilim_ogrenme_blog.ViewModel.Anasayfa
{
    public class GirisYapModel
    {
        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez."),StringLength(50, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Kullanici { get; set; }

        [DisplayName("Parola"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(30, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Parola { get; set; }
    }
}