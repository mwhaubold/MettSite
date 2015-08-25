using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MettSite.Models
{
    public class Customer
    {
        public int ID { get; set; }

        [Display(Name = "Besteller")]
        public string Name { get; set; }

        public double Balance { get; set; }

        public string Preference { get; set; }
        
        public virtual ICollection<MettOrder> MettOrders { get; set; }
    }
}