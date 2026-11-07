using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using Salino.DataLayer;
using Salino.Models;

namespace SalinoMvc5.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin1")]
    public class GalleriesController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

        // GET: Admin/Galleries
        public ActionResult Index(int? id)
        {

            Session["ProId"] = id;
            if (id != null)
            {
                Session["id"] = id;
                var galleryProducts = db.Galleries.Where(x => x.ProductId == id);
                return View(galleryProducts.ToList());

            }
            int idMe = Convert.ToInt32(Session["id"]);
            return View(db.Galleries.Where(x => x.ProductId == idMe));
        }

     

        // GET: Admin/Galleries/Create
        public ActionResult Create()
        {
           
            int id = Convert.ToInt32(Session["ProId"]);
            ViewBag.ProductId = new SelectList(db.Products.Where(x => x.Id == id), "Id", "CodeKala");
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,ProductId,ImgPath")] Gallery gallery, HttpPostedFileBase file)
        {
            
            
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = gallery.Id.ToString();
                    WebImage img = new WebImage(file.OpenReadStream());
                    string imgsrc3 = "Gallery" + "-" + imgname + "-" + imgcode.ToString() + "-" + file.FileName;

                    img.Save("~/Content/ImgGallery/" + imgsrc3);
                    gallery.ImgPath = imgsrc3;
                }

                db.Galleries.Add(gallery);
                db.SaveChanges();
                TempData["id"] = gallery.ProductId;

                return RedirectToAction("Index", new { @id = gallery.ProductId });
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "CodeKala", gallery.ProductId);
            return View(gallery);
        }

        // GET: Admin/Galleries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "CodeKala", gallery.ProductId);
            return View(gallery);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,ProductId,ImgPath")] Gallery gallery, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/ImgGallery/" + gallery.ImgPath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/ImgGallery/" + gallery.ImgPath));
                    }
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = gallery.Id.ToString();
                    WebImage img = new WebImage(file.OpenReadStream());
                    string imgsrc3 = "Gallery" + "-" + imgname + "-" + imgcode.ToString() + "-" + file.FileName;

                    img.Save("~/Content/ImgGallery/" + imgsrc3);
                    gallery.ImgPath = imgsrc3;
                }
                db.Entry(gallery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { @id = gallery.ProductId });
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "CodeKala", gallery.ProductId);
            return View(gallery);
        }

        // GET: Admin/Galleries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Admin/Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
          
            Gallery gallery = db.Galleries.Find(id);
            if (System.IO.File.Exists(Server.MapPath("~/Content/ImgGallery/" + gallery.ImgPath)))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/ImgGallery/" + gallery.ImgPath));
            }
            db.Galleries.Remove(gallery);
            db.SaveChanges();
            return RedirectToAction("Index", new { @id = gallery.ProductId });
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
