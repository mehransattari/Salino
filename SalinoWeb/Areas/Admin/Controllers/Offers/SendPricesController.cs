using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using Salino.DataLayer;
using Salino.Models;

namespace SalinoMvc5.Areas.Admin.Controllers.Offers
{
    [Authorize(Roles = "Admin1")]
    [Area("Admin")]
    [Authorize(Roles = "Admin1")]
    public class SendPricesController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

        // GET: Admin/SendPrices
        public ActionResult Index()
        {
            var result = db.SendPrices.Where(x => x.AsNumber != 0);
            return View(result.ToList());
        }

        // GET: Admin/SendPrices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendPrice sendPrice = db.SendPrices.Find(id);
            if (sendPrice == null)
            {
                return HttpNotFound();
            }
            return View(sendPrice);
        }

        // GET: Admin/SendPrices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SendPrices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( SendPrice sendPrice)
        {
            if (ModelState.IsValid)
            {
                db.SendPrices.Add(sendPrice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sendPrice);
        }

        // GET: Admin/SendPrices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendPrice sendPrice = db.SendPrices.Find(id);
            if (sendPrice == null)
            {
                return HttpNotFound();
            }
            return View(sendPrice);
        }

        // POST: Admin/SendPrices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( SendPrice sendPrice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sendPrice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sendPrice);
        }

        // GET: Admin/SendPrices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendPrice sendPrice = db.SendPrices.Find(id);
            if (sendPrice == null)
            {
                return HttpNotFound();
            }
            return View(sendPrice);
        }

        // POST: Admin/SendPrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SendPrice sendPrice = db.SendPrices.Find(id);
            db.SendPrices.Remove(sendPrice);
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
