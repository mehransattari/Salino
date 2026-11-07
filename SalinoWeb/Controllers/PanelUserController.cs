
using Salino.DataLayer;
using Salino.Models;
using Salino.ViewModels;
using SalinoMvc5.SalinoUtilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalinoMvc5.Controllers
{
    public class PanelUserController : SalinoController
    {
        SalinoContext db = new SalinoContext();
        // GET: PanelUser
        public ActionResult Index()
        {
            return View();
        }





        #region Information

        public ActionResult PersonalInformation()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(db.users.Where(x => x.Mobile == User.Identity.Name).FirstOrDefault());
            }
            else
            {
                return View();
            }

        }
        public ActionResult EditPersonalInformation( int typePage = 0)
        {
            ViewBag.typePage = typePage;
            if (User.Identity.IsAuthenticated)
            {
                return View(db.users.Where(x => x.Mobile == User.Identity.Name).FirstOrDefault());
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult EditPersonalInformation(User user,int typePage=0)
        {
            if (user != null)
            {
                if (user.cityname == "0")
                {
                    user.cityname = db.users.Find(user.Id).cityname;
                }
                if (user.ostanname == "0")
                {
                    user.ostanname = db.users.Find(user.Id).ostanname;

                }



                var userresult = db.users.Where(x => x.Id == user.Id).FirstOrDefault();
                userresult.CodeActivate = RandomNumber.RandomMake().ToString();
                userresult.cityname = user.cityname;
                userresult.ostanname = user.ostanname;
                userresult.OstanId =Convert.ToInt32( user.ostanname);
                userresult.CodeActivate = RandomNumber.RandomMake().ToString();
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
                if(typePage==1)
                return Redirect("/Product/ShopCart");
                return RedirectToAction("PersonalInformation");
            }
            return View();
        }


        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChgangePasswordViewModel change)
        {
            string passOld = change.UserOldPassword.ToHashPassword();
            var user = db.users.Where(x => x.Mobile == User.Identity.Name && x.UserPassword == passOld).FirstOrDefault();
            if (user != null)
            {
                string passNew = change.UserPassword.ToHashPassword();
                user.UserPassword = passNew;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("UserPassword", "رمزعبورجاری شما صحیح نیست.");
            }
            return View();
        }
        #endregion


        #region Orders

        public ActionResult FactorMainShow(int? page = 1)
        {
            ViewBag.pagecurrent = page == 1 ? 1 : 1 + 4;
            ViewBag.page = page;

            var result = db.FactorMains.Where(x => x.UserMobile == User.Identity.Name&&x.IsPay==true)
                .OrderByDescending(x=>x.DateCreateDatetime).ToList();
     
            return View(result.ToPagedList((int)page, 4));
        }

        public ActionResult FactorDetailShow(int? id, int? page = 1)
        {
            if(id!=null)
            {
                ViewBag.pagecurrent = page == 1 ? 1 : 1 + 4;
                var result = db.FactorDetails.Where(x => x.FactorMainId== id).ToList();
                
                ViewBag.ShomareFactor = db.FactorMains.Find(id).SaleReferenceId;         
                return View(result.ToPagedList((int)page,4));
            }
            else
            {
               return View();
            }
        
        }
        public ActionResult DetailFactorMain(int? id)
        {
            if (id != null)
            {               
                var result = db.FactorMains.FirstOrDefault(x => x.Id == id);               
                return View(result);
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region Tickets

        [Authorize(Roles = "User2,User3,User1")]
        public ActionResult TicketsIndex(int? page = 1)
        {
            ViewBag.pagecurrent = page == 1 ? 1 : 1 + 4;

            var userId = db.users.Where(x => x.Mobile == User.Identity.Name).FirstOrDefault().Id;
            var result = db.Ticket.Where(x => x.UserId == userId).ToList();
         
            return View(result.Where(x=>x.ParentId==null).OrderByDescending(x=>x.Id).ToPagedList((int)page, 2));

        }
        [Authorize(Roles = "User2,User3,User1")]
        public ActionResult TicketDetails(int? id)
        {
            var Ticket = db.Ticket;
            var result = id != null ? Ticket.Find(id) : null;
            return View(result == null ? null : result);
        }
        [Authorize(Roles = "User2,User3,User1")]
        [HttpGet]
        public ActionResult TicketsCreate()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "User2,User3,User1")]
        public ActionResult TicketsCreate(Ticket ticket)
        {
            if(ModelState.IsValid)
            {
                var userId = db.users.Where(x => x.Mobile == User.Identity.Name).FirstOrDefault().Id;
                ticket.UserId = userId;
                ticket.Createdate = ticket.Createdate +"_"+ DateTime.Now.ToString("hh:mm:dd");
                db.Ticket.Add(ticket);
                db.SaveChanges();
                return RedirectToAction(nameof(TicketsIndex));
            }
            else
            {
                return View();

            }
     
        }
        [Authorize(Roles = "User2,User3,User1")]

        [HttpGet]
        public ActionResult TicketsEdit(int? id)
        {
            var Ticket = db.Ticket;
            var result= id != null ? Ticket.Find(id) : null;
            return View(result==null? null: result);
        }
        [Authorize(Roles = "User2,User3,User1")]
        [HttpPost]
        public ActionResult TicketsEdit(Ticket ticket)
        {
            if(ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(nameof(TicketsIndex));
            }
            else
            {
                return View();
            }
         
        }

        #endregion

        #region Report
        public ActionResult ReportFactors(int? id)
        {
            if (id != null)
            {
                int typeSend = Convert.ToInt32(Request.Param("typeSend"));
                if (typeSend == 2)
                {
                    ViewBag.IsAsReportDay = "Ok";
                }
                var result = db.FactorDetails.Where(x => x.FactorMainId == id.Value).ToList();
                ViewBag.FactorMain = db.FactorMains.Find(id);
                return View(result);
            }

            return View();
        }

        //Print
        public ActionResult ReportFactorsPrint(int? id)
        {
            if (id != null)
            {
                var result = db.FactorDetails.Where(x => x.FactorMainId == id.Value).ToList();
                ViewBag.FactorMain = db.FactorMains.Find(id);

                return View(result);
            }

            return View();
        }
        #endregion

    }
}