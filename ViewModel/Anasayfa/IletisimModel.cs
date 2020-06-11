using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace yazilim_ogrenme_blog.ViewModel.Anasayfa
{
    public class IletisimModel
    {

   

        [DisplayName("Adınız"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(30, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Ad { get; set; }

        [DisplayName("Soyadınız"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(30, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Soyad { get; set; }

        [DisplayName("E-posta"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(40, ErrorMessage = "{0} max. {1} karakter olmalı."), EmailAddress(ErrorMessage = "{0} alanı için geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [DisplayName("Mesajınız"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Mesaj { get; set; }



    }
}