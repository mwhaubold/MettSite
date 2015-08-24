using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MettSite.Models
{
    public class MettOrder
    {
        public int ID { get; set; }

        [ForeignKey("Customer")]
        [Display(Name = "Besteller")]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        [Display(Name = "Anzahl Mettbrötchen")]
        public int MettBunNumber { get; set; }

        [Display(Name = "Anzahl Tartarbrötchen")]
        public int TartarBunNumber { get; set; }

        [Display(Name = "Anzahl Getränke")]
        public int BeverageNumber { get; set; }

        [Display(Name = "Bestelldatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime MettOrderDate { get; set; }

        [ForeignKey("MettShop")]
        [Display(Name = "MettShopID")]
        public int MettShopID { get; set; }
        public virtual MettShop MettShop { get; set; }
    }
}