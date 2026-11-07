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
    public class FarbicTypesController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

     
        public ActionResult Index(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var FarbicTypes = db.FarbicTypes.Where(x => x.tiltle.Contains(search) ).ToList();
                return View(FarbicTypes);
            }
            else
            {
                var FarbicTypes = db.FarbicTypes.ToList();
                return View(FarbicTypes);
            }


        }
        // GET: Admin/FarbicTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FarbicType farbicType = db.FarbicTypes.Find(id);
            if (farbicType == null)
            {
                return HttpNotFound();
            }
            return View(farbicType);
        }

        // GET: Admin/FarbicTypes/Create
        public ActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( FarbicType farbicType)
        {
            if (ModelState.IsValid)
            {

                db.FarbicTypes.Add(farbicType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(farbicType);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FarbicType farbicType = db.FarbicTypes.Find(id);
            if (farbicType == null)
            {
                return HttpNotFound();
            }
            return View(farbicType);
        }

        // POST: Admin/FarbicTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FarbicType farbicType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(farbicType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(farbicType);
        }

        // GET: Admin/FarbicTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FarbicType farbicType = db.FarbicTypes.Find(id);
            if (farbicType == null)
            {
                return HttpNotFound();
            }
            return View(farbicType);
        }

        // POST: Admin/FarbicTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FarbicType farbicType = db.FarbicTypes.Find(id);
            db.FarbicTypes.Remove(farbicType);
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
