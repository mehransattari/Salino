using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using Salino.DataLayer;
using Salino.Models;

namespace SalinoMvc5.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin1")]
    [Area("Admin")]
    [Authorize(Roles = "Admin1")]
    public class UsersController : SalinoController
    {
        private SalinoContext db = new SalinoContext();
        public ActionResult Index(string search, int? page = 1,int? typeuser=0)
        {
         
            int roleId=0;
            if(typeuser!=0)
            {
                switch (typeuser)
                {
                    case 1:
                        roleId = 3;
                        break;
                    case 2:
                        roleId = 4;
                        break;
                    case 3:
                        roleId = 5;
                        break;
                    default:
                        break;
                }
                Session["roleId"] = roleId;
            }
         
            if (!string.IsNullOrEmpty(search))
            {
                if (Session["roleId"] != null && typeuser == 0)
                {
                    roleId = Convert.ToInt32(Session["roleId"]);
                }
              
                ViewBag.ShowroleId = roleId;
                Session["pagecurrent"] = page;
                if (page == 1) ViewBag.counter = 1;
                else ViewBag.counter = ((page - 1) * 16) + 1;
                if (roleId!=0)
                {
                    var resultWithRole = db.users.Where(x =>
                                                        (x.UserName.Contains(search) ||
                                                            x.Roles.RoleName.Contains(search) ||
                                                            x.Roles.RoleTitle.Contains(search) ||
                                                            x.Mobile.Contains(search) ||
                                                            x.cityname.Contains(search) ||
                                                            x.ostanname.Contains(search) ||
                                                            x.PostalCode.Contains(search))
                                                         ).Include(u => u.Roles).ToList();
                    return View(resultWithRole.ToPagedList((int)page, 16));
                }
                else
                {
                    var result = db.users.Where(x => (x.UserName.Contains(search) ||
                                                       x.Roles.RoleName.Contains(search) ||
                                                       x.Roles.RoleTitle.Contains(search) ||
                                                       x.Mobile.Contains(search) ||
                                                       x.cityname.Contains(search) ||
                                                       x.ostanname.Contains(search) ||
                                                       x.PostalCode.Contains(search))
                                                    ).Include(u => u.Roles).ToList();
                    return View(result.ToPagedList((int)page, 16));
                }
              

            }
            else
            {

                if (Session["roleId"] != null && typeuser == 0)
                {
                    roleId = Convert.ToInt32(Session["roleId"]);
                }
                ViewBag.ShowroleId = roleId;
                var page1 = page == 0 ? 1 : page;
                Session["pagecurrent"] = page1;
                if (page == 1) ViewBag.counter = 1;
                else ViewBag.counter = ((page1 - 1) * 16) + 1;
                if (roleId != 0)
                {
                    //var resultWithRole = db.users.Where(x => x.RoleId == roleId )
                    //                                   .Include(u => u.Roles).ToList();

                    //StoreProcedure   GetUserAsRoleId               
                    // Stored procedures dbo.GetUserAsRoleId / GetUsersWithRoles are not part of the schema; LINQ equivalents:
                    var resultWithRole = db.users.Where(x => x.RoleId == roleId).Include(u => u.Roles).ToList();
                                  
                    return View(resultWithRole.ToPagedList((int)page, 16));
                }
                else
                {
                    // var rs = db.users.SqlQuery("dbo.GetUsersWithRoles").ToList();
                    //   var rs1 = db.users.SqlQuery("dbo.GetUsersWithRoles @fname",new SqlParameter("fname","mehran")).ToList();

                    //StoreProcedure   GetUsersWithRoles                  
                    var users = db.users.Include(u => u.Roles).ToList();
                    //var users = db.users.Include(u => u.Roles).ToList();

                    return View(users.ToPagedList((int)page1, 16));
                }
        
            }

     
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.roles, "Id", "RoleTitle");
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( User user, string ostannameCurrent, string citynameCurrent)
        {
            if (ModelState.IsValid)
            {
                if (user.cityname == "0" || user.ostanname == "0")
                {
                    ViewBag.RoleId = new SelectList(db.roles, "Id", "RoleTitle", user.RoleId);
                    ViewBag.MessageCity_OStan = "لطفا شهر واستان را وارد کنید .";
                    return View(user);
                }
                var username = db.users.Where(x => x.Mobile == user.Mobile).FirstOrDefault();
                if (username == null)
                {
                    Random random = new Random();
                    int mycode = random.Next(10000, 900000);
                    string pashash = FormsAuthentication.HashPasswordForStoringInConfigFile(user.UserPassword, "MD5");
                    user.CodeActivate = mycode.ToString();
                    user.UserPassword = pashash;
                    user.RoleId = user.RoleId;
                    user.cityname = Session["city"] != null ? Session["city"].ToString() : user.cityname;
                    user.OstanId = int.Parse(Session["ostan"] != null? Session["ostan"].ToString():  user.ostanname);  ;
                 
                    db.users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    Session["city"] = user.cityname;
                    Session["ostan"] = user.ostanname;
                    ViewBag.RoleId = new SelectList(db.roles, "Id", "RoleTitle", user.RoleId);
                    ViewBag.msg = "شماره همراه شما قبلا ثبت شده است.";
                    return View();
                }
            
            }

            ViewBag.RoleId = new SelectList(db.roles, "Id", "RoleTitle", user.RoleId);
            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.users.Find(id);
         
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.roles, "Id", "RoleTitle", user.RoleId);
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( User user,string ostannameCurrent,string citynameCurrent)
        {
            if (true)
            {
                if (user.cityname == "0")
                {
                    user.cityname = db.users.Find(user.Id).cityname;
                }
                if (user.ostanname == "0")
                {
                    user.ostanname = db.users.Find(user.Id).ostanname;
                   
                }

                Random random = new Random();
                int mycode = random.Next(10000, 900000);
             
                user.CodeActivate = mycode.ToString();
                user.UserPassword = user.UserPassword;
                user.RoleId = user.RoleId;
                user.OstanId = int.Parse(user.ostanname);
                var userresult = db.users.Where(x => x.Id == user.Id).FirstOrDefault();

                userresult.cityname = user.cityname;
                userresult.ostanname = user.ostanname;
                userresult.OstanId = user.OstanId;
                userresult.CodeActivate = user.CodeActivate;
                userresult.UserPassword = user.UserPassword;
                userresult.Createdate = user.Createdate;
                userresult.StreetAddress = user.StreetAddress;
                userresult.FactorMain = user.FactorMain;
                userresult.IsActive = user.IsActive;
                userresult.RoleId = user.RoleId;
                userresult.Mobile = user.Mobile;
                userresult.PostalCode = user.PostalCode;
                userresult.UserName = user.UserName;                 
                         
                db.Entry(userresult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.roles, "Id", "RoleTitle", user.RoleId);
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.users.Find(id);
            db.users.Remove(user);
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
