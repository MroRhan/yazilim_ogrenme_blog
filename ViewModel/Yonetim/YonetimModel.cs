using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using yazilim_ogrenme_blog.Models;

namespace yazilim_ogrenme_blog.ViewModel.Yonetim
{
    public class YonetimModel
    {
        public List<Kullanicilar> Kullanicilar { get; set; }
        public List<Yazilar> Yazilar { get; set; }
        public List<Kategoriler> Kategoriler { get; set; }
        public List<Yorumlar> Yorumlar { get; set; }
        public List<Ayarlar> Ayarlar { get; set; }
    }
}