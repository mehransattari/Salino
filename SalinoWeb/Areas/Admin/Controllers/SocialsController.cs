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
    public class SocialsController : SalinoController
    {
     
        private SalinoContext db = new SalinoContext();


        // GET: Admin/Socials
        public ActionResult Index()
        {
            return View(db.Socials.ToList());
        }

        // GET: Admin/Socials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Social social = db.Socials.Find(id);
            if (social == null)
            {
                return HttpNotFound();
            }
            return View(social);
        }

        // GET: Admin/Socials/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Socials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,SocialName,SocialIcon,SocialLink,NotShow,SocialOrder")] Social social, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                if (file != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/imgsocial/" + social.SocialIcon)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/imgsocial/" + social.SocialIcon));
                    }
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = "";
                    WebImage img = new WebImage(file.OpenReadStream());
                    string imgsrc1 = "imgsocial" + "-" + imgname + "-" + imgcode.ToString() + "-" + file.FileName;
                    if (img.Width > 900)
                        img.Resize(500, 500);
                    img.Save("~/Content/imgsocial/" + imgsrc1);
                    social.SocialIcon = imgsrc1;
                }
                db.Socials.Add(social);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(social);
        }

        // GET: Admin/Socials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Social social = db.Socials.Find(id);
            if (social == null)
            {
                return HttpNotFound();
            }
            return View(social);
        }

        // POST: Admin/Socials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,SocialName,SocialIcon,SocialLink,NotShow,SocialOrder")] Social social, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                if (file != null)
                {
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = "";
                    WebImage img = new WebImage(file.OpenReadStream());
                    string imgsrc1 = "imgsocial" + "-" + imgname + "-" + imgcode.ToString() + "-" + file.FileName;
                    if (img.Width > 900)
                        img.Resize(500, 500);
                    img.Save("~/Content/imgsocial/" + imgsrc1);
                    social.SocialIcon = imgsrc1;
                }
                db.Entry(social).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(social);
        }

        // GET: Admin/Socials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Social social = db.Socials.Find(id);
            if (social == null)
            {
                return HttpNotFound();
            }
            return View(social);
        }

        // POST: Admin/Socials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Social social = db.Socials.Find(id);
            db.Socials.Remove(social);
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
