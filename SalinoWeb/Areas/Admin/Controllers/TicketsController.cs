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
    public class TicketsController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

        // GET: Admin/Tickets
        public ActionResult Index(string search, int? page = 1)
        {
          
       

            if (!string.IsNullOrEmpty(search))
            {
                Session["pagecurrent"] = page;
                if (page == 1) ViewBag.counter = 1;
                else ViewBag.counter = ((page - 1) * 14) + 1;
                var ticket = db.Ticket.Where(x => x.ParentId == null&&(x.User.Mobile.Contains(search)|| x.User.UserName.Contains(search))).Include(t => t.User).OrderByDescending(x => x.Id).ToList();
             
                return View(ticket.ToPagedList((int)page, 14));
                //return View(products);

            }

            else
            {
                var page1 = page == 0 ? 1 : page;
                Session["pagecurrent"] = page1;
                if (page == 1) ViewBag.counter = 1;
                else ViewBag.counter = ((page1 - 1) * 14) + 1;
                var ticket = db.Ticket.Where(x => x.ParentId == null ).Include(t => t.User).OrderByDescending(x => x.Id).ToList();

                return View(ticket.ToPagedList((int)page, 14));
            }
        }

        // GET: Admin/Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            int currentPage = Convert.ToInt32(Session["pagecurrent"]);
            ViewBag.currentPage = currentPage;
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        public ActionResult answer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            var AnswerTicket = db.Ticket.Where(x => x.ParentId == ticket.Id).FirstOrDefault();
    
            ViewBag.username = ticket.User.UserName;
            ViewBag.TextTicket= ticket.TextTicket;
            if(AnswerTicket!=null)
            {
              ticket.TextTicket = AnswerTicket.TextTicket;
            }
            else
            {
                ticket.TextTicket = " ";
            }
       
         
            if (ticket == null)
            {
                return HttpNotFound();
            }
            if (AnswerTicket != null)
            {
                AnswerTicket.OkAnswer = true;
                return View(AnswerTicket);
            }
            else
            {
              return View(ticket);
            }
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult answer(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var Isanswerticket = db.Ticket.Where(x => x.ParentId == ticket.Id).FirstOrDefault();
                if (Isanswerticket != null)
                {
                    Isanswerticket.TextTicket = ticket.TextTicket;
                    Isanswerticket.Createdate = Isanswerticket.Createdate + "_" + DateTime.Now.ToString("hh:mm:dd");
                    db.Entry(Isanswerticket).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {

                    Ticket model = new Ticket()
                    {
                        Id = ticket.Id + 1,
                        UserId = ticket.UserId,
                        TextTicket = ticket.TextTicket,
                        ParentId = ticket.Id,
                        TitleTicket = ticket.TitleTicket

                    };
                    model.Createdate = model.Createdate + "_" + DateTime.Now.ToString("hh:mm:dd");
                    db.Ticket.Add(model);
                    db.SaveChanges();

                    var model2 = db.Ticket.Where(x => x.Id == model.ParentId).FirstOrDefault();
                    model2.OkAnswer = true;
                    db.SaveChanges();
                }
                int currentPage = Convert.ToInt32(Session["pagecurrent"]);
                return RedirectToAction("Index", new { page = currentPage });
            }
        
            return View(ticket);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                ViewBag.UserName = ticket.User.UserName;
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.Ticket, "Id", "TitleTicket", ticket.ParentId);
            ViewBag.UserId = new SelectList(db.users, "Id", "UserName", ticket.UserId);
            return View(ticket);
        }

        // POST: Admin/Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.Ticket, "Id", "TitleTicket", ticket.ParentId);
            ViewBag.UserId = new SelectList(db.users, "Id", "UserName", ticket.UserId);
            int currentPage = Convert.ToInt32(Session["pagecurrent"]);
            return RedirectToAction("Index", new { page = currentPage });
        }

        // GET: Admin/Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Admin/Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Ticket.Find(id);
            db.Ticket.Remove(ticket);
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
