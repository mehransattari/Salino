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
    public class SettingsController : SalinoController
    {
      
        private SalinoContext db = new SalinoContext();

        // GET: Admin/Settings
        public ActionResult Index()
        {
            return View(db.Settings.ToList());
        }

        // GET: Admin/Settings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setting setting = db.Settings.Find(id);
            if (setting == null)
            {
                return HttpNotFound();
            }
            return View(setting);
        }

        // GET: Admin/Settings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Settings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Setting setting, HttpPostedFileBase file, HttpPostedFileBase file1)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = "setting";
                    WebImage img = new WebImage(file.OpenReadStream());
                    string imgsrc3 = "Group" + "-" + imgname + "-" + imgcode.ToString() + "-" + file.FileName;

                    img.Save("~/Content/imgsetting/" + imgsrc3);
                    setting.imageSrc = imgsrc3;
                }
                if (file1 != null)
                {
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = "setting";
                    WebImage img = new WebImage(file1.OpenReadStream());
                    string imgsrc3 = "Group" + "-" + imgname + "-" + imgcode.ToString() + "-" + file1.FileName;

                    img.Save("~/Content/imgsetting/" + imgsrc3);
                    setting.imageSrcMain = imgsrc3;
                }
                db.Settings.Add(setting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(setting);
        }

        // GET: Admin/Settings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setting setting = db.Settings.Find(id);
            if (setting == null)
            {
                return HttpNotFound();
            }
            return View(setting);
        }

        // POST: Admin/Settings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Setting setting, HttpPostedFileBase file, HttpPostedFileBase file1)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/imgsetting/" + setting.imageSrc)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/imgsetting/" + setting.imageSrc));
                    }
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = "setting";
                    WebImage img = new WebImage(file.OpenReadStream());
                    string imgsrc3 = "Group" + "-" + imgname + "-" + imgcode.ToString() + "-" + file.FileName;

                    img.Save("~/Content/imgsetting/" + imgsrc3);
                    setting.imageSrc = imgsrc3;
                }
                if (file1 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/imgsetting/" + setting.imageSrcMain)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/imgsetting/" + setting.imageSrcMain));
                    }
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = "setting";
                    WebImage img = new WebImage(file1.OpenReadStream());
                    string imgsrc3 = "Group" + "-" + imgname + "-" + imgcode.ToString() + "-" + file1.FileName;

                    img.Save("~/Content/imgsetting/" + imgsrc3);
                    setting.imageSrcMain = imgsrc3;
                }
                db.Entry(setting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(setting);
        }

        // GET: Admin/Settings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setting setting = db.Settings.Find(id);
            if (setting == null)
            {
                return HttpNotFound();
            }
            return View(setting);
        }

        // POST: Admin/Settings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Setting setting = db.Settings.Find(id);
            if (System.IO.File.Exists(Server.MapPath("~/Content/imgsetting/" + setting.imageSrc)))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/imgsetting/" + setting.imageSrc));
            }
            if (System.IO.File.Exists(Server.MapPath("~/Content/imgsetting/" + setting.imageSrcMain)))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/imgsetting/" + setting.imageSrcMain));
            }
            db.Settings.Remove(setting);
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
