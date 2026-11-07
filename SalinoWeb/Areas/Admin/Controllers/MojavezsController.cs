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
    public class MojavezsController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

        // GET: Admin/Mojavezs
        public ActionResult Index()
        {
            return View(db.Mojavezs.ToList());
        }

        // GET: Admin/Mojavezs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mojavez mojavez = db.Mojavezs.Find(id);
            if (mojavez == null)
            {
                return HttpNotFound();
            }
            return View(mojavez);
        }

        // GET: Admin/Mojavezs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Mojavezs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,ListKhadamat,Img1,Img2,Img3")] Mojavez mojavez, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            if (ModelState.IsValid)
            {
                if (file1 != null)
                {
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = "mojavez";
                    WebImage img = new WebImage(file1.OpenReadStream());
                    string imgsrc3 = "mojavez" + "-" + imgname + "-" + imgcode.ToString() + "-" + file1.FileName;

                    img.Save("~/Content/imgMojavez/" + imgsrc3);
                    mojavez.Img1 = imgsrc3;
                }
                if (file2 != null)
                {
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = "mojavez";
                    WebImage img = new WebImage(file2.OpenReadStream());
                    string imgsrc3 = "mojavez" + "-" + imgname + "-" + imgcode.ToString() + "-" + file2.FileName;

                    img.Save("~/Content/imgMojavez/" + imgsrc3);
                    mojavez.Img2 = imgsrc3;
                }
                if (file3 != null)
                {
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = "mojavez";
                    WebImage img = new WebImage(file3.OpenReadStream());
                    string imgsrc3 = "mojavez" + "-" + imgname + "-" + imgcode.ToString() + "-" + file3.FileName;

                    img.Save("~/Content/imgMojavez/" + imgsrc3);
                    mojavez.Img3 = imgsrc3;
                }
                db.Mojavezs.Add(mojavez);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mojavez);
        }

        // GET: Admin/Mojavezs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mojavez mojavez = db.Mojavezs.Find(id);
            if (mojavez == null)
            {
                return HttpNotFound();
            }
            return View(mojavez);
        }

        // POST: Admin/Mojavezs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,ListKhadamat,Img1,Img2,Img3")] Mojavez mojavez, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            if (ModelState.IsValid)
            {
                if (file1 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/imgMojavez/" + mojavez.Img1)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/imgMojavez/" + mojavez.Img1));
                    }
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = "mojavez";
                    WebImage img = new WebImage(file1.OpenReadStream());
                    string imgsrc3 = "mojavez" + "-" + imgname + "-" + imgcode.ToString() + "-" + file1.FileName;

                    img.Save("~/Content/imgMojavez/" + imgsrc3);
                    mojavez.Img1 = imgsrc3;
                }
                if (file2 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/imgMojavez/" + mojavez.Img2)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/imgMojavez/" + mojavez.Img2));
                    }
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = "mojavez";
                    WebImage img = new WebImage(file2.OpenReadStream());
                    string imgsrc3 = "mojavez" + "-" + imgname + "-" + imgcode.ToString() + "-" + file2.FileName;

                    img.Save("~/Content/imgMojavez/" + imgsrc3);
                    mojavez.Img2 = imgsrc3;
                }
                if (file3 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/imgMojavez/" + mojavez.Img3)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/imgMojavez/" + mojavez.Img3));
                    }
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = "mojavez";
                    WebImage img = new WebImage(file3.OpenReadStream());
                    string imgsrc3 = "mojavez" + "-" + imgname + "-" + imgcode.ToString() + "-" + file3.FileName;

                    img.Save("~/Content/imgMojavez/" + imgsrc3);
                    mojavez.Img3 = imgsrc3;
                }
                db.Entry(mojavez).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mojavez);
        }

        // GET: Admin/Mojavezs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mojavez mojavez = db.Mojavezs.Find(id);
            if (mojavez == null)
            {
                return HttpNotFound();
            }
            return View(mojavez);
        }

        // POST: Admin/Mojavezs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
          
            Mojavez mojavez = db.Mojavezs.Find(id);
            if (System.IO.File.Exists(Server.MapPath("~/Content/imgMojavez/" + mojavez.Img1)))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/imgMojavez/" + mojavez.Img1));
            }
            if (System.IO.File.Exists(Server.MapPath("~/Content/imgMojavez/" + mojavez.Img2)))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/imgMojavez/" + mojavez.Img2));
            }
            if (System.IO.File.Exists(Server.MapPath("~/Content/imgMojavez/" + mojavez.Img3)))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/imgMojavez/" + mojavez.Img3));
            }
            db.Mojavezs.Remove(mojavez);
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
