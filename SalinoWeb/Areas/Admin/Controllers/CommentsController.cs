using Salino.DataLayer;
using Salino.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
namespace SalinoMvc5.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin1")]
    [Area("Admin")]
    [Authorize(Roles = "Admin1")]
    public class CommentsController : SalinoController
    {
        private SalinoContext db = new SalinoContext();


        public ActionResult Index(int? page = 1)
        {
            var comments = db.Comments.Where(x => x.ParentId == null).Include(c => c.product).ToList();
            return View(comments.ToPagedList((int)page, 24));
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }


        //public ActionResult Create()
        //{
        //    ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
        //    return View();
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind("Id,ProductId,Name,Email,TextComment,Date,IsShow")] Comments comments)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Comments.Add(comments);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", comments.ProductId);
        //    return View(comments);
        //}


        public ActionResult answer(int? id)
        {
            if (id != null)
            {
              
                Comments comment = db.Comments.Find(id.Value);
                var Answercomment = db.Comments.Where(x => x.ParentId == comment.Id).FirstOrDefault();

                ViewBag.ProductId = comment.Id;
                ViewBag.ProductId = comment.ProductId;
                ViewBag.ProductName = comment.product.Name;
                ViewBag.ParentId = comment.Id;
                ViewBag.ParentContent = comment.TextComment;

                ViewBag.Name = comment.Name;
                ViewBag.Date = comment.Createdate;
                if (Answercomment != null)
                {
                    comment.TextComment = Answercomment.TextComment;
                }
                else
                {
                    comment.TextComment = " ";
                }


                if (comment == null)
                {
                    return HttpNotFound();
                }
                if (Answercomment != null)
                {
                    Answercomment.OkAnswer = true;
                    return View(Answercomment);
                }
                else
                {
                    return View(comment);
                }
             
            }
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult answer(Comments comments)
        {
          
            if (ModelState.IsValid)
            {
                var Isanswerticket = db.Comments.Where(x => x.ParentId == comments.Id).FirstOrDefault();
                if (Isanswerticket != null)
                {
                    Isanswerticket.IsShow = comments.IsShow;
                    Isanswerticket.TextComment = comments.TextComment;
                   
                    db.Entry(Isanswerticket).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {

                    Comments model = new Comments()
                    {
                        Id = comments.Id + 1,
                        ProductId = comments.ProductId,
                        ParentId = comments.Id,
                        TextComment = comments.TextComment,
                        Name = "مدیر",

                        IsShow = comments.IsShow,
                      
                    };
                   
                    db.Comments.Add(model);
                    db.SaveChanges();

                    var model2 = db.Comments.Where(x => x.Id == model.ParentId).FirstOrDefault();
                    model2.OkAnswer = true;
                    model2.IsShow = true;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(comments);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", comments.ProductId);
            return View(comments);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comment = db.Comments.Find(id);
            if (comment.ParentId != null)
            {
                //ViewBag.ParentName = comment.Parent.Name;
                ViewBag.ProNmae = comment.product.Name;
            }
            ViewBag.ProNmae = comment.product.Name;
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }
        [HttpPost]
        public ActionResult Edit(Comments comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }
        public ActionResult Delete(int? id)
        {
            Comments comments = db.Comments.Find(id);
            db.Comments.Remove(comments);
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
