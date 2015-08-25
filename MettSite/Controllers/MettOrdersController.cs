using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MettSite.DataLayer;
using MettSite.Models;

namespace MettSite.Controllers
{
    public class MettOrdersController : Controller
    {
        private DataContext db = new DataContext();

        // GET: MettOrders
        public ActionResult Index()
        {
            var mettOrders = db.MettOrders.Include(m => m.Customer).Include(m => m.MettShop);
            return View(mettOrders.ToList());
        }

        // GET: MettOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MettOrder mettOrder = db.MettOrders.Find(id);
            if (mettOrder == null)
            {
                return HttpNotFound();
            }
            return View(mettOrder);
        }

        // GET: MettOrders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name");
            ViewBag.MettShopID = new SelectList(db.MettShops, "ID", "ID");
            return View();
        }

        // POST: MettOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CustomerID,MettBunNumber,TartarBunNumber,BeverageNumber,MettOrderDate,MettShopID")] MettOrder mettOrder)
        {
            if (ModelState.IsValid)
            {
                db.MettOrders.Add(mettOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name", mettOrder.CustomerID);
            ViewBag.MettShopID = new SelectList(db.MettShops, "ID", "ID", mettOrder.MettShopID);
            return View(mettOrder);
        }

        // GET: MettOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MettOrder mettOrder = db.MettOrders.Find(id);
            if (mettOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name", mettOrder.CustomerID);
            ViewBag.MettShopID = new SelectList(db.MettShops, "ID", "ID", mettOrder.MettShopID);
            return View(mettOrder);
        }

        // POST: MettOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CustomerID,MettBunNumber,TartarBunNumber,BeverageNumber,MettOrderDate,MettShopID")] MettOrder mettOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mettOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name", mettOrder.CustomerID);
            ViewBag.MettShopID = new SelectList(db.MettShops, "ID", "ID", mettOrder.MettShopID);
            return View(mettOrder);
        }

        // GET: MettOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MettOrder mettOrder = db.MettOrders.Find(id);
            if (mettOrder == null)
            {
                return HttpNotFound();
            }
            return View(mettOrder);
        }

        // POST: MettOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MettOrder mettOrder = db.MettOrders.Find(id);
            db.MettOrders.Remove(mettOrder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
