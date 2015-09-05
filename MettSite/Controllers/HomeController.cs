using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MettSite.DataLayer;
using MettSite.ViewModels;

namespace MettSite.Controllers
{

    public class HomeController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<MettStatistics> data = from mettorder in db.MettOrders
                                              group mettorder by mettorder.Customer.Name into ordergroup
                                              select new MettStatistics()
                                              {
                                                  CustomerName = ordergroup.Key,
                                                  MettWeight = ordergroup.Sum(c => (c.MettBunNumber * c.MettShop.MettAmount)/1000),
                                                  TartarWeight = ordergroup.Sum(c => (c.TartarBunNumber * c.MettShop.TartarAmount)/1000),
                                                  BeverageCount = ordergroup.Sum(c => c.BeverageNumber)
                                              };

            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}