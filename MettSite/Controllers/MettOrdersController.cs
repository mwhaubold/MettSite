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
        public ActionResult Create([Bind(Include = "ID,CustomerID,MettBunNumber,TartarBunNumber,BeverageNumber")] MettOrder mettOrder)
        {
            if (ModelState.IsValid)
            {
                mettOrder.MettOrderDate = DateTime.Today;

                int latestMettShop = db.MettShops.Max(p => p.ID);
                mettOrder.MettShopID = latestMettShop;

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
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MettOrder mettOrderToUpdate = db.MettOrders.Find(ID);

            if (mettOrderToUpdate.MettOrderDate == DateTime.Today)
            {
                if (TryUpdateModel(mettOrderToUpdate, "", new string[] {"MettBunNumber", "TartarBunNumber", "BeverageNumber"}))
                {
                    try
                    {
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (DataException)
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
                }
            }
            return View(mettOrderToUpdate);
        }

        // GET: MettOrders/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists, see your system administrator.";
            }
            MettOrder mettOrder = db.MettOrders.Find(id);
            if (mettOrder == null)
            {
                return HttpNotFound();
            }
            return View(mettOrder);
        }

        // POST: MettOrders/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                MettOrder mettorder = db.MettOrders.Find(id);
                db.MettOrders.Remove(mettorder);
                db.SaveChanges();
            }
            catch (DataException)
            {

                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
