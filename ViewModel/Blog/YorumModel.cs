using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace yazilim_ogrenme_blog.ViewModel.Blog
{
    public class YorumModel
    {


        [DisplayName("Yorumunuz"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Icerik { get; set; }


    }
}