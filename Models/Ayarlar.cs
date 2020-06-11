using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace yazilim_ogrenme_blog.Models
{

    [Table("Ayarlar")]
    public class Ayarlar
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Hakkimizda { get; set; }

        
        public Boolean UyelikDurum { get; set; }

    }
}