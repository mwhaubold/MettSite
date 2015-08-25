using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MettSite.Models
{
    public class MettShop
    {
        public int ID { get; set; }

        public double MettPrice { get; set; }
        public double MettAmount { get; set; }

        public double TartarPrice { get; set; }
        public double TartarAmount { get; set; }

        public double BeveragePrice { get; set; }

        [Display(Name = "Mettshop Änderungsdatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ChangeDate { get; set; }
    }
}