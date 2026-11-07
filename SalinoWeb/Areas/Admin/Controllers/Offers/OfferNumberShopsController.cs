using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using Salino.DataLayer;
using Salino.Models;

namespace SalinoMvc5.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin1")]
    [Area("Admin")]
    [Authorize(Roles = "Admin1")]
    public class OfferNumberShopsController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

        // GET: Admin/OfferNumberShops
        public ActionResult Index()
        {
           
            var result = db.OfferNumberShops.Where(x => x.PriceDiscount != 0).ToList();
            return View(result);
        }

        // GET: Admin/OfferNumberShops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferNumberShop offerNumberShop = db.OfferNumberShops.Find(id);
            if (offerNumberShop == null)
            {
                return HttpNotFound();
            }
            return View(offerNumberShop);
        }

        // GET: Admin/OfferNumberShops/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/OfferNumberShops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,ToNumber,PriceDiscount")] OfferNumberShop offerNumberShop)
        {
            if (ModelState.IsValid)
            {
                db.OfferNumberShops.Add(offerNumberShop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(offerNumberShop);
        }

        // GET: Admin/OfferNumberShops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferNumberShop offerNumberShop = db.OfferNumberShops.Find(id);
            if (offerNumberShop == null)
            {
                return HttpNotFound();
            }
            return View(offerNumberShop);
        }

        // POST: Admin/OfferNumberShops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,ToNumber,PriceDiscount")] OfferNumberShop offerNumberShop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offerNumberShop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(offerNumberShop);
        }

        // GET: Admin/OfferNumberShops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferNumberShop offerNumberShop = db.OfferNumberShops.Find(id);
            if (offerNumberShop == null)
            {
                return HttpNotFound();
            }
            return View(offerNumberShop);
        }

        // POST: Admin/OfferNumberShops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OfferNumberShop offerNumberShop = db.OfferNumberShops.Find(id);
            db.OfferNumberShops.Remove(offerNumberShop);
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
