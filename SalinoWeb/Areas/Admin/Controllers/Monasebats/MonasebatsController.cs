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
    public class MonasebatsController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

        // GET: Admin/Monasebats
        public ActionResult Index(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var Monasebats = db.Monasebats.Where(x => x.MonasebatTitle.Contains(search)).ToList();
                return View(Monasebats);
            }
            else
            {
                return View(db.Monasebats.ToList());
            }
           
        }
        public ActionResult DetailsProducts(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monasebat Monasebat = db.Monasebats.Find(id);
            var productmonasebat = db.MonasebatProducts.Where(x => x.MonasebatId == Monasebat.Id).ToList();
            ViewBag.monasebat = Monasebat.MonasebatTitle;
            if (Monasebat == null)
            {
                return HttpNotFound();
            }
            return View(productmonasebat);
        }
        // GET: Admin/Monasebats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monasebat monasebat = db.Monasebats.Find(id);
            if (monasebat == null)
            {
                return HttpNotFound();
            }
            return View(monasebat);
        }

        // GET: Admin/Monasebats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Monasebats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,MonasebatTitle")] Monasebat monasebat)
        {
            if (ModelState.IsValid)
            {
                db.Monasebats.Add(monasebat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(monasebat);
        }

        // GET: Admin/Monasebats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monasebat monasebat = db.Monasebats.Find(id);
            if (monasebat == null)
            {
                return HttpNotFound();
            }
            return View(monasebat);
        }

        // POST: Admin/Monasebats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,MonasebatTitle")] Monasebat monasebat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monasebat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(monasebat);
        }

        // GET: Admin/Monasebats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monasebat monasebat = db.Monasebats.Find(id);
            if (monasebat == null)
            {
                return HttpNotFound();
            }
            return View(monasebat);
        }

        // POST: Admin/Monasebats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Monasebat monasebat = db.Monasebats.Find(id);
            db.Monasebats.Remove(monasebat);
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

        public ActionResult ShowMonasebats()
        {
            return PartialView("_ShowMonasebats");
        }
        public ActionResult AddMonasebat()
        {
            return PartialView("_AddMonasebat");
        }
    }
}
