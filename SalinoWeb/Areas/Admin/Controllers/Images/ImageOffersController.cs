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
    public class ImageOffersController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

        // GET: Admin/ImageOffers
        public ActionResult Index()
        {
            return View(db.ImageOffers.ToList());
        }

        // GET: Admin/ImageOffers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageOffer imageOffer = db.ImageOffers.Find(id);
            if (imageOffer == null)
            {
                return HttpNotFound();
            }
            return View(imageOffer);
        }

        // GET: Admin/ImageOffers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ImageOffers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,SliderTitle,SliderIMG,SliderIMGMob")] ImageOffer imageOffer, HttpPostedFileBase file, HttpPostedFileBase file1)
        {
            if (ModelState.IsValid)
            {

                if (file != null)
                {
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = imageOffer.SliderTitle;
                    WebImage img = new WebImage(file.OpenReadStream());
                    string imgsrc3 = "imgoffer" + "-" + imgname + "-" + imgcode.ToString() + "-" + file.FileName;
             
                    img.Save("~/Content/imgoffer/" + imgsrc3);
                    imageOffer.SliderIMG = imgsrc3;
                }
                if (file1 != null)
                {
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = imageOffer.SliderTitle;
                    WebImage img = new WebImage(file1.OpenReadStream());
                    string imgsrc3 = "imgoffermob" + "-" + imgname + "-" + imgcode.ToString() + "-" + file1.FileName;
             
                    img.Save("~/Content/imgoffer/" + imgsrc3);
                    imageOffer.SliderIMGMob = imgsrc3;
                }
                db.ImageOffers.Add(imageOffer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(imageOffer);
        }

        // GET: Admin/ImageOffers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageOffer imageOffer = db.ImageOffers.Find(id);
            if (imageOffer == null)
            {
                return HttpNotFound();
            }
            return View(imageOffer);
        }

        // POST: Admin/ImageOffers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,SliderTitle,SliderIMG,SliderIMGMob")] ImageOffer imageOffer, HttpPostedFileBase file, HttpPostedFileBase file1)
        {
            if (ModelState.IsValid)
            {

                if (file != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/imgoffer/" + imageOffer.SliderIMG)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/imgoffer/" + imageOffer.SliderIMG));
                    }
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = imageOffer.SliderTitle;
                    WebImage img = new WebImage(file.OpenReadStream());
                    string imgsrc3 = "imgoffer" + "-" + imgname + "-" + imgcode.ToString() + "-" + file.FileName;
             
                    img.Save("~/Content/imgoffer/" + imgsrc3);
                    imageOffer.SliderIMG = imgsrc3;
                }
                if (file1 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/imgoffer/" + imageOffer.SliderIMGMob)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/imgoffer/" + imageOffer.SliderIMGMob));
                    }
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = imageOffer.SliderTitle;
                    WebImage img = new WebImage(file1.OpenReadStream());
                    string imgsrc3 = "imgoffermob" + "-" + imgname + "-" + imgcode.ToString() + "-" + file1.FileName;
                  
                    img.Save("~/Content/imgoffer/" + imgsrc3);
                    imageOffer.SliderIMGMob = imgsrc3;
                }
                db.Entry(imageOffer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(imageOffer);
        }

        // GET: Admin/ImageOffers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageOffer imageOffer = db.ImageOffers.Find(id);
            if (imageOffer == null)
            {
                return HttpNotFound();
            }
            return View(imageOffer);
        }

        // POST: Admin/ImageOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ImageOffer imageOffer = db.ImageOffers.Find(id);
            db.ImageOffers.Remove(imageOffer);
            db.SaveChanges();
            if (System.IO.File.Exists(Server.MapPath("~/Content/imgoffer/" + imageOffer.SliderIMG)))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/imgoffer/" + imageOffer.SliderIMG));
            }
            if (System.IO.File.Exists(Server.MapPath("~/Content/imgoffer/" + imageOffer.SliderIMGMob)))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/imgoffer/" + imageOffer.SliderIMGMob));
            }
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
