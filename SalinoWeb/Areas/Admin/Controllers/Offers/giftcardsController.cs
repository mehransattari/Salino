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
    public class giftcardsController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

        // GET: Admin/giftcards
        public ActionResult Index()
        {
            return View(db.giftcards.ToList());
        }

        // GET: Admin/giftcards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giftcard giftcard = db.giftcards.Find(id);
            if (giftcard == null)
            {
                return HttpNotFound();
            }
            return View(giftcard);
        }

        // GET: Admin/giftcards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/giftcards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( giftcard giftcard)
        {
            if (ModelState.IsValid)
            {
             
                db.giftcards.Add(giftcard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(giftcard);
        }

        // GET: Admin/giftcards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giftcard giftcard = db.giftcards.Find(id);
            if (giftcard == null)
            {
                return HttpNotFound();
            }
            return View(giftcard);
        }

        // POST: Admin/giftcards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( giftcard giftcard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giftcard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(giftcard);
        }

        // GET: Admin/giftcards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giftcard giftcard = db.giftcards.Find(id);
            if (giftcard == null)
            {
                return HttpNotFound();
            }
            return View(giftcard);
        }

        // POST: Admin/giftcards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            giftcard giftcard = db.giftcards.Find(id);
            db.giftcards.Remove(giftcard);
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
