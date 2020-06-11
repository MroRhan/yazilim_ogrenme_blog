using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace yazilim_ogrenme_blog.Models
{
    [Table("Iletisim")]
    public class Iletisim
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(30), Required]
        public string Ad { get; set; }

        [StringLength(30), Required]
        public string Soyad { get; set; }

        [StringLength(40), Required]
        public string Mail { get; set; }

        [Required]
        public string Icerik { get; set; }




    }
}