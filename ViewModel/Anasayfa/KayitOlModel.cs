using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace yazilim_ogrenme_blog.ViewModel.Anasayfa
{
    public class KayitOlModel
    {

        [DisplayName("Kullanıcı Adınız"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(50, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Kullanici { get; set; }

        [DisplayName("Adınız"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(30, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Ad { get; set; }

        [DisplayName("Soyadınız"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(30, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Soyad { get; set; }

        [DisplayName("E-posta"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(40, ErrorMessage = "{0} max. {1} karakter olmalı."),EmailAddress(ErrorMessage = "{0} alanı için geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [DisplayName("Parola"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(30, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Parola { get; set; }

        [DisplayName("Parola Tekrar"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(30, ErrorMessage = "{0} max. {1} karakter olmalı."),Compare("Parola",ErrorMessage ="Parolalar bir biriyle uyuşmuyor.")]
        public string RParola { get; set; }



    }
}