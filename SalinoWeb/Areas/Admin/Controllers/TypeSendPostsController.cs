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
    public class TypeSendPostsController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

        // GET: Admin/TypeSendPosts
        public ActionResult Index()
        {
            return View(db.TypeSendPosts.Where(x=>x.PriceHaml!="0").ToList());
        }

        // GET: Admin/TypeSendPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeSendPost typeSendPost = db.TypeSendPosts.Find(id);
            if (typeSendPost == null)
            {
                return HttpNotFound();
            }
            return View(typeSendPost);
        }

        // GET: Admin/TypeSendPosts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TypeSendPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,NameHaml,PriceHaml")] TypeSendPost typeSendPost)
        {
            if (ModelState.IsValid)
            {
                db.TypeSendPosts.Add(typeSendPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeSendPost);
        }

        // GET: Admin/TypeSendPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeSendPost typeSendPost = db.TypeSendPosts.Find(id);
            if (typeSendPost == null)
            {
                return HttpNotFound();
            }
            return View(typeSendPost);
        }

        // POST: Admin/TypeSendPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,NameHaml,PriceHaml")] TypeSendPost typeSendPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeSendPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeSendPost);
        }

        // GET: Admin/TypeSendPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeSendPost typeSendPost = db.TypeSendPosts.Find(id);
            if (typeSendPost == null)
            {
                return HttpNotFound();
            }
            return View(typeSendPost);
        }

        // POST: Admin/TypeSendPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeSendPost typeSendPost = db.TypeSendPosts.Find(id);
            db.TypeSendPosts.Remove(typeSendPost);
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
