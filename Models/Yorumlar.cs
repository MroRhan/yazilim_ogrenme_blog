using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace yazilim_ogrenme_blog.Models
{
    [Table("Yorumlar")]
    public class Yorumlar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Icerik { get; set; }

        public Boolean Durum { get; set; }


        public virtual  Yazilar Yazi{ get; set; }
        public virtual Kullanicilar Kulanici { get; set; }

    }
}