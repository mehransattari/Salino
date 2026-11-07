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
    public class FactorDetailsController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

        // GET: Admin/FactorDetails
        public ActionResult Index(int? Id)
        {
            try
            {
                if (Id!=0)
                {
                    var factorDetails = db.FactorDetails.Where(x=>x.FactorMainId==Id).Include(f => f.FactorMain);
                    ViewBag.ShomareFactor = db.FactorMains.Find(Id).SaleReferenceId;
                    return View(factorDetails.ToList());
                }
                else
                {
                    return View("Index", "FactorMains");
                }
            }
            catch (Exception)
            {

                return Redirect("~/Home/Error");
            }
         
  
        }

        // GET: Admin/FactorDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FactorDetail factorDetail = db.FactorDetails.Find(id);
            if (factorDetail == null)
            {
                return HttpNotFound();
            }
            return View(factorDetail);
        }

        // GET: Admin/FactorDetails/Create
        public ActionResult Create()
        {
            ViewBag.FactorMainId = new SelectList(db.FactorMains, "Id", "Paymentstatus");
            return View();
        }

        // POST: Admin/FactorDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,FactorMainId,ProductId,ProductName,FarbicTypeId,FarbicTypeName,DetailCount,DetailPrice,SumPrice")] FactorDetail factorDetail)
        {
            if (ModelState.IsValid)
            {
                db.FactorDetails.Add(factorDetail);
                db.SaveChanges();
                return RedirectToAction("Index",new { Id= factorDetail .FactorMainId});
            }

            ViewBag.FactorMainId = new SelectList(db.FactorMains, "Id", "Paymentstatus", factorDetail.FactorMainId);
            return View(factorDetail);
        }

        // GET: Admin/FactorDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FactorDetail factorDetail = db.FactorDetails.Find(id);
            if (factorDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.FactorMainId = new SelectList(db.FactorMains, "Id", "Paymentstatus", factorDetail.FactorMainId);
            return View(factorDetail);
        }

        // POST: Admin/FactorDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,FactorMainId,ProductId,ProductName,FarbicTypeId,FarbicTypeName,DetailCount,DetailPrice,SumPrice")] FactorDetail factorDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factorDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { Id = factorDetail.FactorMainId });
            }
            ViewBag.FactorMainId = new SelectList(db.FactorMains, "Id", "Paymentstatus", factorDetail.FactorMainId);
            return View(factorDetail);
        }

        // GET: Admin/FactorDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FactorDetail factorDetail = db.FactorDetails.Find(id);
            if (factorDetail == null)
            {
                return HttpNotFound();
            }
            return View(factorDetail);
        }

        // POST: Admin/FactorDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FactorDetail factorDetail = db.FactorDetails.Find(id);
            db.FactorDetails.Remove(factorDetail);
            db.SaveChanges();
            return RedirectToAction("Index", new { Id = factorDetail.FactorMainId });
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
