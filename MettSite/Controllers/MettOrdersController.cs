using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MettSite.Models;

namespace MettSite.Controllers
{
    public class MettOrdersController : Controller
    {
        private MettSiteContext db = new MettSiteContext();

        // GET: MettOrders
        public ActionResult Index()
        {
            return View(db.MettOrders.ToList());
        }

        // GET: MettOrders/Details/5
        public ActionResult Details(string id)
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
            return View();
        }

        // POST: MettOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Customer,OrderID,MettAmount,TartarAmount,ToothpickAmount,BeerAmount,SurpriseBeverageAmount")] MettOrder mettOrder)
        {
            if (ModelState.IsValid)
            {
                db.MettOrders.Add(mettOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mettOrder);
        }

        // GET: MettOrders/Edit/5
        public ActionResult Edit(string id)
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

        // POST: MettOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Customer,OrderID,MettAmount,TartarAmount,ToothpickAmount,BeerAmount,SurpriseBeverageAmount")] MettOrder mettOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mettOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mettOrder);
        }

        // GET: MettOrders/Delete/5
        public ActionResult Delete(string id)
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
        public ActionResult DeleteConfirmed(string id)
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
