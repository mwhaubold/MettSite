using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MettSite.Models;

namespace MettSite.DataLayer
{
    public class DataContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MettOrder> MettOrders { get; set; }
        public DbSet<MettShop> MettShops { get; set; }
    }
}