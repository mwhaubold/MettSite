using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MettSite.Models
{
    [Table("Mett Bestellung")]
    public class MettOrder
    {
        [Key]
        public string Customer { get; set; }

        public int OrderID { get; set; }

        public int MettAmount { get; set; }

        public int TartarAmount { get; set; }

        public int ToothpickAmount { get; set; }

        public int BeerAmount { get; set; }

        public int SurpriseBeverageAmount { get; set; }
    }
}