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
    public class ImageAdversController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

        // GET: Admin/ImageAdvers
        public ActionResult Index()
        {
            return View(db.imgadvr.ToList());
        }

        // GET: Admin/ImageAdvers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageAdver imageAdver = db.imgadvr.Find(id);
            if (imageAdver == null)
            {
                return HttpNotFound();
            }
            return View(imageAdver);
        }

        // GET: Admin/ImageAdvers/Create
        public ActionResult Create()
        {

            ViewBag.GroupIdTopRight = new SelectList(db.groupProducts, "Id", "GroupTitle");
            ViewBag.GroupIdBottomRight = new SelectList(db.groupProducts, "Id", "GroupTitle");
            ViewBag.GroupIdTopLeft = new SelectList(db.groupProducts, "Id", "GroupTitle");
            ViewBag.GroupIdBottomLeft = new SelectList(db.groupProducts, "Id", "GroupTitle");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImageAdver imageAdver, HttpPostedFileBase file, HttpPostedFileBase file2, HttpPostedFileBase file3, HttpPostedFileBase file4)
        {
            #region upload


            if (file != null)
            {
                Random random = new Random();
                string imgcode = random.Next(100000, 999000).ToString();
                string imgname = "";
                WebImage img = new WebImage(file.OpenReadStream());
                string imgsrc1 = "imgadvr" + "-" + imgname + "-" + imgcode.ToString() + "-" + file.FileName;
                if (img.Width > 900)
                    img.Resize(500, 500);
                img.Save("~/Content/imgadvr/" + imgsrc1);
                imageAdver.Img3 = imgsrc1;
            }
            if (file2 != null)
            {
                Random random = new Random();
                string imgcode = random.Next(100000, 999000).ToString();
                string imgname = "";
                WebImage img1 = new WebImage(file2.OpenReadStream());
                string imgsrc2 = "imgadvr" + "-" + imgname + "-" + imgcode.ToString() + "-" + file2.FileName;
                if (img1.Width > 900)
                    img1.Resize(500, 500);
                img1.Save("~/Content/imgadvr/" + imgsrc2);
                imageAdver.Img4 = imgsrc2;
            }
            if (file3 != null)
            {
                Random random = new Random();
                string imgcode = random.Next(100000, 999000).ToString();
                string imgname = "";
                WebImage img2 = new WebImage(file3.OpenReadStream());
                string imgsrc3 = "imgadvr" + "-" + imgname + "-" + imgcode.ToString() + "-" + file3.FileName;
                if (img2.Width > 900)
                    img2.Resize(500, 500);
                img2.Save("~/Content/imgadvr/" + imgsrc3);
                imageAdver.Img1 = imgsrc3;
            }
            if (file4 != null)
            {
                Random random = new Random();
                string imgcode = random.Next(100000, 999000).ToString();
                string imgname = "";
                WebImage img3 = new WebImage(file4.OpenReadStream());
                string imgsrc4 = "imgadvr" + "-" + imgname + "-" + imgcode.ToString() + "-" + file4.FileName;
                if (img3.Width > 900)
                    img3.Resize(500, 500);
                img3.Save("~/Content/imgadvr/" + imgsrc4);
                imageAdver.Img2 = imgsrc4;
            }
            #endregion
            db.imgadvr.Add(imageAdver);
            db.SaveChanges();


            return RedirectToAction("Index");

        }

        // GET: Admin/ImageAdvers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageAdver imageAdver = db.imgadvr.Find(id);

            #region ViewBagSelectList
            if (imageAdver.GroupIdTopRight != 0)
                ViewBag.GroupIdTopRight = new SelectList(db.groupProducts, "Id", "GroupTitle", db.groupProducts.Where(x => x.Id == imageAdver.GroupIdTopRight).FirstOrDefault().Id);
            else
                ViewBag.GroupIdTopRight = new SelectList(db.groupProducts, "Id", "GroupTitle");
            if (imageAdver.GroupIdBottomRight != 0)
                ViewBag.GroupIdBottomRight = new SelectList(db.groupProducts, "Id", "GroupTitle", db.groupProducts.Where(x => x.Id == imageAdver.GroupIdBottomRight).FirstOrDefault().Id);
            else
                ViewBag.GroupIdBottomRight = new SelectList(db.groupProducts, "Id", "GroupTitle");
            if (imageAdver.GroupIdTopLeft != 0)
                ViewBag.GroupIdTopLeft = new SelectList(db.groupProducts, "Id", "GroupTitle", db.groupProducts.Where(x => x.Id == imageAdver.GroupIdTopLeft).FirstOrDefault().Id);
            else
                ViewBag.GroupIdTopLeft = new SelectList(db.groupProducts, "Id", "GroupTitle");
            if (imageAdver.GroupIdBottomLeft != 0)
                ViewBag.GroupIdBottomLeft = new SelectList(db.groupProducts, "Id", "GroupTitle", db.groupProducts.Where(x => x.Id == imageAdver.GroupIdBottomLeft).FirstOrDefault().Id);
            else
                ViewBag.GroupIdBottomLeft = new SelectList(db.groupProducts, "Id", "GroupTitle");
            #endregion


            if (imageAdver == null)
            {
                return HttpNotFound();
            }
            return View(imageAdver);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ImageAdver imageAdver, HttpPostedFileBase file, HttpPostedFileBase file2, HttpPostedFileBase file3, HttpPostedFileBase file4)
        {
            if (ModelState.IsValid)
            {
                #region upload


                if (file != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/imgadvr/" + imageAdver.Img3)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/imgadvr/" + imageAdver.Img3));
                    }
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = "";
                    WebImage img = new WebImage(file.OpenReadStream());
                    string imgsrc1 = "imgadvr" + "-" + imgname + "-" + imgcode.ToString() + "-" + file.FileName;
                    if (img.Width > 900)
                        img.Resize(500, 500);
                    img.Save("~/Content/imgadvr/" + imgsrc1);
                    imageAdver.Img3 = imgsrc1;
                }
                if (file2 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/imgadvr/" + imageAdver.Img4)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/imgadvr/" + imageAdver.Img4));
                    }
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = "";
                    WebImage img = new WebImage(file2.OpenReadStream());
                    string imgsrc2 = "imgadvr" + "-" + imgname + "-" + imgcode.ToString() + "-" + file2.FileName;
                    if (img.Width > 900)
                        img.Resize(500, 500);
                    img.Save("~/Content/imgadvr/" + imgsrc2);
                    imageAdver.Img4 = imgsrc2;
                }
                if (file3 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/imgadvr/" + imageAdver.Img1)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/imgadvr/" + imageAdver.Img1));
                    }
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = "";
                    WebImage img = new WebImage(file3.OpenReadStream());
                    string imgsrc3 = "imgadvr" + "-" + imgname + "-" + imgcode.ToString() + "-" + file3.FileName;
                    if (img.Width > 900)
                        img.Resize(500, 500);
                    img.Save("~/Content/imgadvr/" + imgsrc3);
                    imageAdver.Img1 = imgsrc3;
                }
                if (file4 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/imgadvr/" + imageAdver.Img2)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/imgadvr/" + imageAdver.Img2));
                    }
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = "";
                    WebImage img = new WebImage(file4.OpenReadStream());
                    string imgsrc4 = "imgadvr" + "-" + imgname + "-" + imgcode.ToString() + "-" + file4.FileName;
                    if (img.Width > 900)
                        img.Resize(500, 500);
                    img.Save("~/Content/imgadvr/" + imgsrc4);
                    imageAdver.Img2 = imgsrc4;
                }
                #endregion
                db.Entry(imageAdver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            #region ViewBagSelectList
            if (imageAdver.GroupIdTopRight != 0)
                ViewBag.GroupIdTopRight = new SelectList(db.groupProducts, "Id", "GroupTitle", db.groupProducts.Where(x => x.Id == imageAdver.GroupIdTopRight).FirstOrDefault().Id);
            else
                ViewBag.GroupIdTopRight = new SelectList(db.groupProducts, "Id", "GroupTitle");
            if (imageAdver.GroupIdBottomRight != 0)
                ViewBag.GroupIdBottomRight = new SelectList(db.groupProducts, "Id", "GroupTitle", db.groupProducts.Where(x => x.Id == imageAdver.GroupIdBottomRight).FirstOrDefault().Id);
            else
                ViewBag.GroupIdBottomRight = new SelectList(db.groupProducts, "Id", "GroupTitle");
            if (imageAdver.GroupIdTopLeft != 0)
                ViewBag.GroupIdTopLeft = new SelectList(db.groupProducts, "Id", "GroupTitle", db.groupProducts.Where(x => x.Id == imageAdver.GroupIdTopLeft).FirstOrDefault().Id);
            else
                ViewBag.GroupIdTopLeft = new SelectList(db.groupProducts, "Id", "GroupTitle");
            if (imageAdver.GroupIdBottomLeft != 0)
                ViewBag.GroupIdBottomLeft = new SelectList(db.groupProducts, "Id", "GroupTitle", db.groupProducts.Where(x => x.Id == imageAdver.GroupIdBottomLeft).FirstOrDefault().Id);
            else
                ViewBag.GroupIdBottomLeft = new SelectList(db.groupProducts, "Id", "GroupTitle");
            #endregion

            return View(imageAdver);
        }

        // GET: Admin/ImageAdvers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageAdver imageAdver = db.imgadvr.Find(id);
            if (imageAdver == null)
            {
                return HttpNotFound();
            }
            return View(imageAdver);
        }

        // POST: Admin/ImageAdvers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ImageAdver imageAdver = db.imgadvr.Find(id);
            db.imgadvr.Remove(imageAdver);
            db.SaveChanges();
            if (System.IO.File.Exists(Server.MapPath("~/Content/imgadvr/" + imageAdver.Img1)))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/imgadvr/" + imageAdver.Img1));
            }
            if (System.IO.File.Exists(Server.MapPath("~/Content/imgadvr/" + imageAdver.Img2)))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/imgadvr/" + imageAdver.Img2));
            }
            if (System.IO.File.Exists(Server.MapPath("~/Content/imgadvr/" + imageAdver.Img3)))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/imgadvr/" + imageAdver.Img3));
            }
            if (System.IO.File.Exists(Server.MapPath("~/Content/imgadvr/" + imageAdver.Img4)))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/imgadvr/" + imageAdver.Img4));
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
