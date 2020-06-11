using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace yazilim_ogrenme_blog.Models
{
    [Table("Kategoriler")]
    public class Kategoriler
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(150), Required(ErrorMessage = "Kategori Başlık Giriniz")]
        public string Baslik { get; set; }

        public Boolean Durum { get; set; }

        public virtual List<Yazilar> Yazi { get; set; }


    }
}