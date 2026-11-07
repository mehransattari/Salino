using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using Salino.DataLayer;
using Salino.Models;
using Salino.Convertor;
using Salino.ToShamsi;

namespace SalinoMvc5.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin1")]
    [Area("Admin")]
    [Authorize(Roles = "Admin1")]
    public class gifcardDatesController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

        // GET: Admin/gifcardDates
        public ActionResult Index()
        {
            return View(db.gifcardDates.ToList());
        }

        // GET: Admin/gifcardDates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gifcardDate gifcardDate = db.gifcardDates.Find(id);
            if (gifcardDate == null)
            {
                return HttpNotFound();
            }
            return View(gifcardDate);
        }

        // GET: Admin/gifcardDates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/gifcardDates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( gifcardDate gifcardDate)
        {
            if(gifcardDate.Amount!=0&& gifcardDate.Code!=""&& gifcardDate.NumberUse!=0&& gifcardDate.ExpirationDateShamsi!="")
            {
                gifcardDate.CreateDateShamsi = gifcardDate.CreateDate.ToShortDateString().ToShamsi();
                gifcardDate.ExpirationDateShamsi = ConvertDate.ConvertToEnglish(gifcardDate.ExpirationDateShamsi);
                gifcardDate.ExpirationDate = ConvertDate.PersianDateStringToDateTime(gifcardDate.ExpirationDateShamsi);
                db.gifcardDates.Add(gifcardDate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }         
             
           
           return View(gifcardDate);
        }

       

        // GET: Admin/gifcardDates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gifcardDate gifcardDate = db.gifcardDates.Find(id);
            if (gifcardDate == null)
            {
                return HttpNotFound();
            }
            return View(gifcardDate);
        }

        // POST: Admin/gifcardDates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( gifcardDate gifcardDate)
        {
            if (gifcardDate.Amount != 0 && gifcardDate.Code != "" && gifcardDate.NumberUse != 0 && gifcardDate.ExpirationDateShamsi != "")
            {
                var gif = db.gifcardDates.Find(gifcardDate.Id);

                if (gif.ExpirationDateShamsi!=gifcardDate.ExpirationDateShamsi)
                    gifcardDate.ExpirationDate = ConvertDate.PersianDateStringToDateTime(ConvertDate.ConvertToEnglish(gifcardDate.ExpirationDateShamsi));
                //Edit          
                gif.Code = gifcardDate.Code;
                gif.Amount = gifcardDate.Amount;
                gif.NumberUse = gifcardDate.NumberUse;
                gif.ExpirationDateShamsi = gifcardDate.ExpirationDateShamsi;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gifcardDate);
        }

        // GET: Admin/gifcardDates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gifcardDate gifcardDate = db.gifcardDates.Find(id);
            if (gifcardDate == null)
            {
                return HttpNotFound();
            }
            return View(gifcardDate);
        }

        // POST: Admin/gifcardDates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gifcardDate gifcardDate = db.gifcardDates.Find(id);
            db.gifcardDates.Remove(gifcardDate);
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
