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
    public class GroupProductsController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

        // GET: Admin/GroupProducts
        public ActionResult Index()
        {
            return View(db.groupProducts.ToList());
        }

        // GET: Admin/GroupProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupProduct groupProduct = db.groupProducts.Find(id);
            if (groupProduct == null)
            {
                return HttpNotFound();
            }
            return View(groupProduct);
        }

        // GET: Admin/GroupProducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/GroupProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,GroupTitle,GroupNotShow,imagepath,imagepathMob")] GroupProduct groupProduct, HttpPostedFileBase filedesk)
        {
            if (ModelState.IsValid)
            {
                #region UploadFile
                if (filedesk != null)
                {
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = groupProduct.GroupTitle;
                    WebImage img = new WebImage(filedesk.OpenReadStream());
                    string imgsrc3 = "Group" + "-" + imgname + "-" + imgcode.ToString() + "-" + filedesk.FileName;

                    img.Save("~/Content/imggroup/" + imgsrc3);
                    groupProduct.imagepath = imgsrc3;
                }
         
          
                #endregion

                db.groupProducts.Add(groupProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(groupProduct);
        }

        // GET: Admin/GroupProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupProduct groupProduct = db.groupProducts.Find(id);
            if (groupProduct == null)
            {
                return HttpNotFound();
            }
            return View(groupProduct);
        }

        // POST: Admin/GroupProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,GroupTitle,GroupNotShow,imagepath")] GroupProduct groupProduct, HttpPostedFileBase filedesk)
        {
            if (ModelState.IsValid)
            {
                #region UploadFile

             
                if (filedesk != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/imggroup/" + groupProduct.imagepath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/imggroup/" + groupProduct.imagepath));
                    }
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = groupProduct.GroupTitle;
                    WebImage img = new WebImage(filedesk.OpenReadStream());
                    string imgsrc3 = "Group" + "-" + imgname + "-" + imgcode.ToString() + "-" + filedesk.FileName;
                 
                    img.Save("~/Content/imggroup/" + imgsrc3);
                    groupProduct.imagepath = imgsrc3;
                }
          
               
                #endregion
                db.Entry(groupProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(groupProduct);
        }

        // GET: Admin/GroupProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupProduct groupProduct = db.groupProducts.Find(id);
            if (groupProduct == null)
            {
                return HttpNotFound();
            }
            return View(groupProduct);
        }

        // POST: Admin/GroupProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GroupProduct groupProduct = db.groupProducts.Find(id);
            if (System.IO.File.Exists(Server.MapPath("~/Content/imggroup/" + groupProduct.imagepath)))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/imggroup/" + groupProduct.imagepath));
            }
        
            db.groupProducts.Remove(groupProduct);
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
