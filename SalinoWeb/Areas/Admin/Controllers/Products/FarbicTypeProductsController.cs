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
    public class FarbicTypeProductsController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

        // GET: Admin/FarbicTypeProducts
        public ActionResult Index()
        {
            var farbicTypeProducts = db.FarbicTypeProducts.Include(f => f.FarbicTypes).Include(f => f.Products);
            return View(farbicTypeProducts.ToList());
        }

        // GET: Admin/FarbicTypeProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FarbicTypeProducts farbicTypeProducts = db.FarbicTypeProducts.Find(id);
            if (farbicTypeProducts == null)
            {
                return HttpNotFound();
            }
            return View(farbicTypeProducts);
        }

        // GET: Admin/FarbicTypeProducts/Create
        public ActionResult Create()
        {
            ViewBag.FarbicTypeId = new SelectList(db.FarbicTypes, "Id", "tiltle");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "CodeKala");
            return View();
        }

        // POST: Admin/FarbicTypeProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,FarbicTypeId,ProductId")] FarbicTypeProducts farbicTypeProducts)
        {
            if (ModelState.IsValid)
            {
                db.FarbicTypeProducts.Add(farbicTypeProducts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FarbicTypeId = new SelectList(db.FarbicTypes, "Id", "tiltle", farbicTypeProducts.FarbicTypeId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "CodeKala", farbicTypeProducts.ProductId);
            return View(farbicTypeProducts);
        }

        // GET: Admin/FarbicTypeProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FarbicTypeProducts farbicTypeProducts = db.FarbicTypeProducts.Find(id);
            if (farbicTypeProducts == null)
            {
                return HttpNotFound();
            }
            ViewBag.FarbicTypeId = new SelectList(db.FarbicTypes, "Id", "tiltle", farbicTypeProducts.FarbicTypeId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "CodeKala", farbicTypeProducts.ProductId);
            return View(farbicTypeProducts);
        }

        // POST: Admin/FarbicTypeProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,FarbicTypeId,ProductId")] FarbicTypeProducts farbicTypeProducts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(farbicTypeProducts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FarbicTypeId = new SelectList(db.FarbicTypes, "Id", "tiltle", farbicTypeProducts.FarbicTypeId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "CodeKala", farbicTypeProducts.ProductId);
            return View(farbicTypeProducts);
        }

        // GET: Admin/FarbicTypeProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FarbicTypeProducts farbicTypeProducts = db.FarbicTypeProducts.Find(id);
            if (farbicTypeProducts == null)
            {
                return HttpNotFound();
            }
            return View(farbicTypeProducts);
        }

        // POST: Admin/FarbicTypeProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FarbicTypeProducts farbicTypeProducts = db.FarbicTypeProducts.Find(id);
            db.FarbicTypeProducts.Remove(farbicTypeProducts);
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
