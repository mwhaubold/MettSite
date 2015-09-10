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
using PagedList;

namespace MettSite.Controllers
{
    public class MettOrdersController : Controller
    {
        private DataContext db = new DataContext();

        // GET: MettOrders
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var mettOrders = from s in db.MettOrders select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                mettOrders = mettOrders.Where(s => s.Customer.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    mettOrders = mettOrders.OrderByDescending(s => s.Customer.Name);
                    break;
                case "Name":
                    mettOrders = mettOrders.OrderBy(s => s.Customer.Name);
                    break;
                case "Date":
                    mettOrders = mettOrders.OrderBy(s => s.MettOrderDate);
                    break;
                default:
                    mettOrders = mettOrders.OrderByDescending(s => s.MettOrderDate);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(mettOrders.ToPagedList(pageNumber, pageSize));
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
                var result = from s in db.MettOrders where s.MettOrderDate == DateTime.Today && s.CustomerID == mettOrder.CustomerID select s;
                int anzEntries = result.Count();
                if (anzEntries == 1)
                {
                    MettOrder foundOrder = result.First();
                    return RedirectToAction("Edit", new { id = foundOrder.ID });
                }
                else if (anzEntries > 0)
                {
                    ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name", mettOrder.CustomerID);
                    ViewBag.MettShopID = new SelectList(db.MettShops, "ID", "ID", mettOrder.MettShopID);
                    return View(mettOrder);
                }

                mettOrder.MettOrderDate = DateTime.Today;

                int latestMettShopID = db.MettShops.Max(p => p.ID);
                mettOrder.MettShopID = latestMettShopID;

                MettShop latestMettShop = db.MettShops.Find(latestMettShopID);
                double charge = (latestMettShop.MettPrice * mettOrder.MettBunNumber) + (latestMettShop.TartarPrice * mettOrder.TartarBunNumber) + (latestMettShop.BeveragePrice * mettOrder.BeverageNumber);

                mettOrder.Charge = charge;


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
                if (TryUpdateModel(mettOrderToUpdate, "", new string[] { "MettBunNumber", "TartarBunNumber", "BeverageNumber" }))
                {
                    mettOrderToUpdate.Charge = (mettOrderToUpdate.MettShop.MettPrice * mettOrderToUpdate.MettBunNumber) + (mettOrderToUpdate.MettShop.TartarPrice * mettOrderToUpdate.TartarBunNumber) + (mettOrderToUpdate.MettShop.BeveragePrice * mettOrderToUpdate.BeverageNumber);

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
        public ActionResult Delete(int? id, bool? saveChangesError = false)
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
            if (mettOrder.MettOrderDate != DateTime.Today)
            {
                return RedirectToAction("Index");
            }
            return View(mettOrder);
        }

        // POST: MettOrders/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            MettOrder currentOrder = db.MettOrders.Find(id);

            if (currentOrder.MettOrderDate != DateTime.Today)
            {
                return RedirectToAction("Index");
            }

            try
            {
                db.MettOrders.Remove(currentOrder);
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
