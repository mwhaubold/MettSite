using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MettSite.Models;

namespace MettSite.DataLayer
{
    public class MettInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var customers = new List<Customer>
            {
                new Customer{Name="ga", Balance=0.0, Preference="Mett"},
                new Customer{Name="lf", Balance=-4.5, Preference="Tartar"},
                new Customer{Name="fc", Balance=1.0, Preference="none"},
                new Customer{Name="hm", Balance=0.0, Preference="Mett"},
                new Customer{Name="be", Balance=5.5, Preference="both"}
            };
            customers.ForEach(s => context.Customers.Add(s));
            context.SaveChanges();

            var mettshops = new List<MettShop>
            {
                new MettShop{MettPrice=1.1, MettAmount=110, TartarPrice=2.5, TartarAmount=90, BeveragePrice=1.0, ChangeDate=DateTime.Parse("01-07-2015")},
                new MettShop{MettPrice=1.2, MettAmount=100, TartarPrice=2.6, TartarAmount=95, BeveragePrice=1.0, ChangeDate=DateTime.Parse("01-08-2015")}
            };
            mettshops.ForEach(s => context.MettShops.Add(s));
            context.SaveChanges();

            var mettorders = new List<MettOrder>
            {
                new MettOrder{CustomerID=1, MettBunNumber=1, TartarBunNumber=0, BeverageNumber=1, MettOrderDate=DateTime.Parse("26-08-2015"), MettShopID=2, Charge=2.2},
                new MettOrder{CustomerID=1, MettBunNumber=2, TartarBunNumber=0, BeverageNumber=2, MettOrderDate=DateTime.Parse("12-08-2015"), MettShopID=2, Charge=4.4},
                new MettOrder{CustomerID=1, MettBunNumber=0, TartarBunNumber=1, BeverageNumber=1, MettOrderDate=DateTime.Parse("29-07-2015"), MettShopID=1, Charge=3.5},

                new MettOrder{CustomerID=2, MettBunNumber=3, TartarBunNumber=2, BeverageNumber=2, MettOrderDate=DateTime.Parse("12-08-2015"), MettShopID=2, Charge=10.8},
                new MettOrder{CustomerID=2, MettBunNumber=2, TartarBunNumber=2, BeverageNumber=1, MettOrderDate=DateTime.Parse("29-07-2015"), MettShopID=1, Charge=8.2},

                new MettOrder{CustomerID=5, MettBunNumber=1, TartarBunNumber=1, BeverageNumber=1, MettOrderDate=DateTime.Parse("12-08-2015"), MettShopID=2, Charge=4.8}
            };
            mettorders.ForEach(s => context.MettOrders.Add(s));
            context.SaveChanges();
        }
    }
}