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
    public class OfferPricesController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

        // GET: Admin/OfferPrices
        public ActionResult Index()
        {
            var result = db.OfferPrices.Where(x => x.AsNumber != 0 ).ToList();
            return View(result);
        }

        // GET: Admin/OfferPrices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferPrice offerPrice = db.OfferPrices.Find(id);
            if (offerPrice == null)
            {
                return HttpNotFound();
            }
            return View(offerPrice);
        }

        // GET: Admin/OfferPrices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/OfferPrices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( OfferPrice offerPrice)
        {
            if (ModelState.IsValid)
            {
                db.OfferPrices.Add(offerPrice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(offerPrice);
        }

        // GET: Admin/OfferPrices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferPrice offerPrice = db.OfferPrices.Find(id);
            if (offerPrice == null)
            {
                return HttpNotFound();
            }
            return View(offerPrice);
        }

        // POST: Admin/OfferPrices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( OfferPrice offerPrice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offerPrice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(offerPrice);
        }

        // GET: Admin/OfferPrices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferPrice offerPrice = db.OfferPrices.Find(id);
            if (offerPrice == null)
            {
                return HttpNotFound();
            }
            return View(offerPrice);
        }

        // POST: Admin/OfferPrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OfferPrice offerPrice = db.OfferPrices.Find(id);
            db.OfferPrices.Remove(offerPrice);
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
