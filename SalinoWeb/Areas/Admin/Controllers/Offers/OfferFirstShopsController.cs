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
    public class OfferFirstShopsController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

        // GET: Admin/OfferFirstShops
        public ActionResult Index()
        {
          
            var result = db.OfferFirstShops.Where(x => x.PriceDiscount != 0).ToList();
            return View(result);
        }

        // GET: Admin/OfferFirstShops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferFirstShop offerFirstShop = db.OfferFirstShops.Find(id);
            if (offerFirstShop == null)
            {
                return HttpNotFound();
            }
            return View(offerFirstShop);
        }

        // GET: Admin/OfferFirstShops/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/OfferFirstShops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,PriceDiscount")] OfferFirstShop offerFirstShop)
        {
            if (ModelState.IsValid)
            {
                db.OfferFirstShops.Add(offerFirstShop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(offerFirstShop);
        }

        // GET: Admin/OfferFirstShops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferFirstShop offerFirstShop = db.OfferFirstShops.Find(id);
            if (offerFirstShop == null)
            {
                return HttpNotFound();
            }
            return View(offerFirstShop);
        }

        // POST: Admin/OfferFirstShops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,PriceDiscount")] OfferFirstShop offerFirstShop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offerFirstShop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(offerFirstShop);
        }

        // GET: Admin/OfferFirstShops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferFirstShop offerFirstShop = db.OfferFirstShops.Find(id);
            if (offerFirstShop == null)
            {
                return HttpNotFound();
            }
            return View(offerFirstShop);
        }

        // POST: Admin/OfferFirstShops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OfferFirstShop offerFirstShop = db.OfferFirstShops.Find(id);
            db.OfferFirstShops.Remove(offerFirstShop);
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
