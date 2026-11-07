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
    public class MonasebatProductsController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

        // GET: Admin/MonasebatProducts
        public ActionResult Index(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var Monasebats = db.MonasebatProducts.Where(x => x.Monasebats.MonasebatTitle.Contains(search)||x.Products.CodeKala.Contains(search)).Include(m => m.Monasebats).Include(m => m.Products).ToList();
                return View(Monasebats);
            }
            else
            {
                var monasebatProducts = db.MonasebatProducts.Include(m => m.Monasebats).Include(m => m.Products);
                return View(monasebatProducts.ToList());
            }

        }

        // GET: Admin/MonasebatProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonasebatProducts monasebatProducts = db.MonasebatProducts.Find(id);
            if (monasebatProducts == null)
            {
                return HttpNotFound();
            }
            return View(monasebatProducts);
        }

        // GET: Admin/MonasebatProducts/Create
        public ActionResult Create()
        {
            ViewBag.MonasebatId = new SelectList(db.Monasebats, "Id", "MonasebatTitle");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "CodeKala");
            return View();
        }

        // POST: Admin/MonasebatProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,MonasebatId,ProductId")] MonasebatProducts monasebatProducts)
        {
            if (ModelState.IsValid)
            {
                db.MonasebatProducts.Add(monasebatProducts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MonasebatId = new SelectList(db.Monasebats, "Id", "MonasebatTitle", monasebatProducts.MonasebatId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "CodeKala", monasebatProducts.ProductId);
            return View(monasebatProducts);
        }

        // GET: Admin/MonasebatProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonasebatProducts monasebatProducts = db.MonasebatProducts.Find(id);
            if (monasebatProducts == null)
            {
                return HttpNotFound();
            }
            ViewBag.MonasebatId = new SelectList(db.Monasebats, "Id", "MonasebatTitle", monasebatProducts.MonasebatId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "CodeKala", monasebatProducts.ProductId);
            return View(monasebatProducts);
        }

        // POST: Admin/MonasebatProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,MonasebatId,ProductId")] MonasebatProducts monasebatProducts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monasebatProducts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MonasebatId = new SelectList(db.Monasebats, "Id", "MonasebatTitle", monasebatProducts.MonasebatId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "CodeKala", monasebatProducts.ProductId);
            return View(monasebatProducts);
        }

        // GET: Admin/MonasebatProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonasebatProducts monasebatProducts = db.MonasebatProducts.Find(id);
            if (monasebatProducts == null)
            {
                return HttpNotFound();
            }
            return View(monasebatProducts);
        }

        // POST: Admin/MonasebatProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MonasebatProducts monasebatProducts = db.MonasebatProducts.Find(id);
            db.MonasebatProducts.Remove(monasebatProducts);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public int DeleteAll(string [] checkedNames)
        {
            foreach (var item in checkedNames)
            {
               
                int id = int.Parse(item);
                MonasebatProducts monasebatProducts = db.MonasebatProducts.Find(id);
                db.MonasebatProducts.Remove(monasebatProducts);
                db.SaveChanges();

            }
        
            return 1;
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
