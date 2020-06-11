using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace yazilim_ogrenme_blog.Models
{

    [Table("Kullanicilar")]
    public class Kullanicilar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(50),Required]
        public string Kullanici_ad { get; set; }

        [StringLength(30), Required]
        public string Ad { get; set; }

        [StringLength(30), Required]
        public string Soyad { get; set; }

        [StringLength(40), Required]
        public string Mail { get; set; }

        [StringLength(30), Required]
        public string Sifre { get; set; }

        [Required]
        public int Yetki { get; set; }

        
        public  DateTime Uyelik_Tarih { get; set; }


        public Boolean Durum { get; set; }



        public virtual List<Yazilar> Yazilar { get; set; }
        public virtual List<Yorumlar> Yorumlar { get; set; }


    }
}