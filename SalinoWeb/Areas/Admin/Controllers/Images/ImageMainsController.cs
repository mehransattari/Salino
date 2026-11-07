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
    public class ImageMainsController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

        // GET: Admin/ImageMains
        public ActionResult Index(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var gproducts = db.ImageMains.Where(x => x.SliderTitle.Contains(search)).ToList();
                return View(gproducts);
            }
            else
            {
                var gproducts = db.ImageMains.ToList();
                return View(gproducts);
            }
        }

        // GET: Admin/ImageMains/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageMain imageMain = db.ImageMains.Find(id);
            if (imageMain == null)
            {
                return HttpNotFound();
            }
            return View(imageMain);
        }

        // GET: Admin/ImageMains/Create
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ImageMain imageMain, HttpPostedFileBase file, HttpPostedFileBase file1)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = imageMain.SliderTitle;
                    WebImage img = new WebImage(file.OpenReadStream());
                    string imgsrc3 = "imgmain" + "-" + imgname + "-" + imgcode.ToString() + "-" + file.FileName;
                
                    img.Save("~/Content/imgmain/" + imgsrc3);
                    imageMain.SliderIMG = imgsrc3;
                }
                if (file1 != null)
                {
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = imageMain.SliderTitle;
                    WebImage img = new WebImage(file1.OpenReadStream());
                    string imgsrc3 = "imgmain" + "-" + imgname + "-" + imgcode.ToString() + "-" + file1.FileName;
                  
                    img.Save("~/Content/imgmain/" + imgsrc3);
                    imageMain.SliderIMGMob = imgsrc3;
                }
                db.ImageMains.Add(imageMain);
                db.SaveChanges();
                TempData["Okresult_imgmain"] = "گروه " + imageMain.SliderTitle + " " + "با موفقیت درج گردید .";
                return RedirectToAction("Index");
            }

            return View(imageMain);
        }

        // GET: Admin/ImageMains/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageMain imageMain = db.ImageMains.Find(id);
            if (imageMain == null)
            {
                return HttpNotFound();
            }
            return View(imageMain);
        }

        // POST: Admin/ImageMains/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( ImageMain imageMain, HttpPostedFileBase file, HttpPostedFileBase file1)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/imgmain/" + imageMain.SliderIMG)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/imgmain/" + imageMain.SliderIMG));
                    }
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = imageMain.SliderTitle;
                    WebImage img = new WebImage(file.OpenReadStream());
                    string imgsrc3 = "imageMain" + "-" + imgname + "-" + imgcode.ToString() + "-" + file.FileName;
               
                    img.Save("~/Content/imgmain/" + imgsrc3);
                    imageMain.SliderIMG = imgsrc3;
                }
                if (file1 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/imgmain/" + imageMain.SliderIMGMob)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/imgmain/" + imageMain.SliderIMGMob));
                    }
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = imageMain.SliderTitle;
                    WebImage img = new WebImage(file1.OpenReadStream());
                    string imgsrc3 = "imgmain" + "-" + imgname + "-" + imgcode.ToString() + "-" + file1.FileName;
                  
                    img.Save("~/Content/imgmain/" + imgsrc3);
                    imageMain.SliderIMGMob = imgsrc3;
                }
                db.Entry(imageMain).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(imageMain);
        }

        // GET: Admin/ImageMains/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageMain imageMain = db.ImageMains.Find(id);
            if (imageMain == null)
            {
                return HttpNotFound();
            }
            return View(imageMain);
        }

        // POST: Admin/ImageMains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ImageMain imageMain = db.ImageMains.Find(id);
            db.ImageMains.Remove(imageMain);
            db.SaveChanges();
            if (System.IO.File.Exists(Server.MapPath("~/Content/imgmain/" + imageMain.SliderIMG)))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/imgmain/" + imageMain.SliderIMG));
            }
            if (System.IO.File.Exists(Server.MapPath("~/Content/imgmain/" + imageMain.SliderIMGMob)))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/imgmain/" + imageMain.SliderIMGMob));
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
