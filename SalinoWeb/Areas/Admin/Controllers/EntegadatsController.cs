using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using Salino.DataLayer;
using Salino.Models;
using SalinoMvc5.SalinoUtilities;

namespace SalinoMvc5.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin1")]
    public class EntegadatsController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

        // GET: Admin/Entegadats
        public ActionResult Index(string search, int? page = 1)
        {
            if (!string.IsNullOrEmpty(search))
            {
                Session["pagecurrent"] = page;
                if (page == 1) ViewBag.counter = 1;
                else ViewBag.counter = ((page - 1) * 14) + 1;

                var entegadats = db.Entegadats.Where(x => x.Name.Contains(search) ||
                x.Email.Contains(search)).Include(p => p.Parent).OrderByDescending(x => x.Id).ToList();
                return View(entegadats.ToPagedList((int)page, 14));


            }

            else
            {
                var page1 = page == 0 ? 1 : page;
                Session["pagecurrent"] = page1;
                if (page == 1) ViewBag.counter = 1;
                else ViewBag.counter = ((page1 - 1) * 14) + 1;
                var entegadat1 = db.Entegadats.Include(p => p.Parent).OrderByDescending(x => x.Id).ToList();

                return View(entegadat1.ToPagedList((int)page1, 14));
            }
           
        }


        // GET: Admin/Entegadats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entegadat entegadat = db.Entegadats.Find(id);
            int currentPage = Convert.ToInt32(Session["pagecurrent"]);
            ViewBag.currentPage = currentPage;
            if (entegadat == null)
            {
                return HttpNotFound();
            }
            return View(entegadat);
        }

        // GET: Admin/Entegadats/Create

        public ActionResult answer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entegadat entegadat = db.Entegadats.Find(id);


            ViewBag.ParentId = entegadat.Id;
            ViewBag.nameadmin = "مدیر";
            ViewBag.emailadmin = "salinotbz1@gmail.com";

            ViewBag.ParentContent = entegadat.TextComment;
            ViewBag.Email = entegadat.Email;
            ViewBag.Name = entegadat.Name;
            ViewBag.Date = entegadat.Date;
            if (entegadat == null)
            {
                return HttpNotFound();
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult answer(Entegadat entegadat)
        {
            if (ModelState.IsValid)
            {
                Entegadat entega = new Entegadat()
                {
                    Name = "مدیر",                   
                    Parentid = entegadat.Id,
                    Email= entegadat.Email,
                    TextComment = entegadat.TextComment
                };

                db.Entegadats.Add(entega);
                db.SaveChanges();
                var model2 = db.Entegadats.Where(x => x.Id == entegadat.Id).FirstOrDefault();
                model2.OkAnswer = true;

                db.SaveChanges();
                //____________فرستادن کد به ایمیل
                EmailService.EmailSender(entegadat.TextComment, "salino1234m1234", "salinotbz1@gmail.com", entegadat.Email);

                //_________________________

                int currentPage = Convert.ToInt32(Session["pagecurrent"]);
                return RedirectToAction("Index", new { page = currentPage });
            }


            return View(entegadat);
        }


      
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entegadat en = db.Entegadats.Find(id);
            if (en == null)
            {
                return HttpNotFound();
            }
            return View(en);
        }

        // POST: Admin/Mojavezs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Entegadat en = db.Entegadats.Find(id);
          
            db.Entegadats.Remove(en);
            db.SaveChanges();
            int currentPage = Convert.ToInt32(Session["pagecurrent"]);
            return RedirectToAction("Index", new { page = currentPage });
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
