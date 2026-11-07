using Salino.Convertor;
using Salino.DataLayer;
using Salino.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Salino.ToShamsi;
using System.Drawing;

namespace SalinoMvc5.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin1")]
    [Area("Admin")]
    [Authorize(Roles = "Admin1")]
    public class DefaultController : SalinoController
    {


        private SalinoContext db = new SalinoContext();
        // GET: Admin/Default
        public ActionResult Index()
        {
            #region سفارش مشتریان عادی

            if (Session["OrdinaryNormalCustomerMonth"] == null)
            {
                //سفارش مشتریان عادی در ماه گذشته
                int NumberMonth = OrdinaryNormalCustomerMonth();
                Session["OrdinaryNormalCustomerMonth"] = NumberMonth;
            }

            if (Session["OrdinaryNormalCustomerWeek"] == null)
            {
                //سفارش مشتریان عادی در هفته گذشته
                int NumberWeek = OrdinaryNormalCustomerWeek();
                Session["OrdinaryNormalCustomerWeek"] = NumberWeek;
            }

            if (Session["OrdinaryNormalCustomerDay"] == null)
            {

                //سفارش مشتریان عادی در روز گذشته
                int NumberDay = OrdinaryNormalCustomerDay();
                Session["OrdinaryNormalCustomerDay"] = NumberDay;
            }

            #endregion

            #region سفارش مشتریان عمده

            if (Session["OrdinaryOmdehCustomerMonth"] == null)
            {
                //سفارش مشتریان عمده در ماه گذشته
                int NumberMonth = OrdinaryOmdehCustomerMonth();
                Session["OrdinaryOmdehCustomerMonth"] = NumberMonth;
            }

            if (Session["OrdinaryOmdehCustomerWeek"] == null)
            {
                //سفارش مشتریان عمده در هفته گذشته
                int NumberWeek = OrdinaryOmdehCustomerWeek();
                Session["OrdinaryOmdehCustomerWeek"] = NumberWeek;
            }

            if (Session["OrdinaryOmdehCustomerDay"] == null)
            {

                //سفارش مشتریان عمده در روز گذشته
                int NumberDay = OrdinaryOmdehCustomerDay();
                Session["OrdinaryOmdehCustomerDay"] = NumberDay;
            }


            #endregion

            return View();
        }

        #region ChangePassword

        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChgangePasswordViewModel change)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            string hashOldPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(change.UserOldPassword, "MD5");
            string hashNewPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(change.UserPassword, "MD5");
            var user = db.users.Where(x => x.Mobile == User.Identity.Name && x.UserPassword == hashOldPassword).FirstOrDefault();
            if (user != null)
            {
                user.UserPassword = hashNewPassword;
                db.SaveChanges();
                return RedirectToAction("index", "Users");
            }
            else
            {
                ModelState.AddModelError("UserPassword", "رمزعبورجاری شما صحیح نیست.");
            }
            return View();
        }

        #endregion

        #region Helper Site
        public ActionResult Help()
        {
            return View();
        }
        #endregion

        #region سفارش مشتریان عادی
        ////سفارش مشتریان عادی در سال گذشته
        //public int OrdinaryNormalCustomerYear()
        //{
        //    DateTime ToDate = DateTime.Now;
        //    string toDate = ToDate.ToShortDateString();
        //    int yr = Convert.ToInt32(toDate.Substring(6, 4));
        //    int year = (yr - 1);           
        //    string month = ToDate.ToShortDateString().Substring(0, 2);
        //    string day = ToDate.ToShortDateString().Substring(3, 2);
        //    string asdate = month + "-" + day + "-" + year; ;
        //    DateTime AsDate = Convert.ToDateTime(asdate);

        //        ViewBag.AsDate = AsDate;
        //        ViewBag.ToDate = ToDate;
        //    int counter = 0;
        //    for (DateTime asDate = AsDate; asDate <= ToDate; asDate= asDate.AddDays(1))
        //    {
        //        string asdateShamsi = asDate.ToShortDateString().ToShamsi();
        //        var result = db.FactorMains.Where(x => x.DateCreate== asdateShamsi).ToList();
        //        if(result.Count()!=0)
        //        {
        //            counter += result.Count();
        //        }
        //    }
        //    return counter;

        //}
        //سفارش مشتریان عادی در ماه گذشته
        public int OrdinaryNormalCustomerMonth()
        {
            DateTime ToDate = DateTime.Now;
            DateTime asdate = ToDate.AddDays(-30);
            //DateTime asdate = ToDate.AddDays(-30);
            //string yr = toDate.Substring(6, 4);
            //string Month = ToDate.ToShortDateString().Substring(0, 2);
            //int month = (Convert.ToInt32(Month)) - 1;
            //string day = ToDate.ToShortDateString().Substring(3, 2);
            //string asdate = month + "-" + day + "-" + yr;

            DateTime AsDate = asdate;

            ViewBag.AsDate = AsDate;
            ViewBag.ToDate = ToDate;

            int counter = 0;
            for (DateTime asDate = AsDate; asDate <= ToDate; asDate = asDate.AddDays(1))
            {
                string asdateShamsi = asDate.ToShortDateString().ToShamsi();
                var result = db.FactorMains.Where(x =>x.Paymentstatus=="100"&&( x.DateCreate == asdateShamsi && (x.RoleId == 3))).ToList();
                if (result.Count() != 0)
                {
                    counter += result.Count();
                }
            }
            return counter;

        }
        //سفارش مشتریان عادی در هفته گذشته
        public int OrdinaryNormalCustomerWeek()
        {
            DateTime ToDate = DateTime.Now;
            DateTime asdate = ToDate.AddDays(-7);
            //DateTime ToDate = DateTime.Now;
            //string toDate = ToDate.ToShortDateString();
            //string yr = toDate.Substring(6, 4);
            //string Month = ToDate.ToShortDateString().Substring(0, 2);
            //string day = ToDate.ToShortDateString().Substring(3, 2);
            //int Day = Convert.ToInt32(day) - 7;
            //string asdate = Month + "-" + Day + "-" + yr;
            DateTime AsDate = asdate;

            ViewBag.AsDate = AsDate;
            ViewBag.ToDate = ToDate;

            int counter = 0;
            for (DateTime asDate = AsDate; asDate <= ToDate; asDate = asDate.AddDays(1))
            {
                string asdateShamsi = asDate.ToShortDateString().ToShamsi();
                var result = db.FactorMains.Where(x => x.Paymentstatus == "100" &&( x.DateCreate == asdateShamsi && (x.RoleId == 3))).ToList();
                if (result.Count() != 0)
                {
                    counter += result.Count();
                }
            }
            return counter;

        }
        //سفارش مشتریان عادی در روز گذشته
        public int OrdinaryNormalCustomerDay()
        {
            DateTime ToDate = DateTime.Now;
            DateTime asdate = ToDate.AddDays(-1);
            //DateTime ToDate = DateTime.Now;
            //string toDate = ToDate.ToShortDateString();
            //string yr = toDate.Substring(6, 4);
            //string Month = ToDate.ToShortDateString().Substring(0, 2);       
            //string day = ToDate.ToShortDateString().Substring(3, 2);
            //int Day = Convert.ToInt32(day) - 1;
            //string asdate = Month + "-" + Day + "-" + yr; 
            DateTime AsDate = asdate;

            ViewBag.AsDate = AsDate;
            ViewBag.ToDate = ToDate;

            int counter = 0;
            for (DateTime asDate = AsDate; asDate <= ToDate; asDate = asDate.AddDays(1))
            {
                string asdateShamsi = asDate.ToShortDateString().ToShamsi();
                var result = db.FactorMains.Where(x => x.Paymentstatus == "100" &&( x.DateCreate == asdateShamsi && (x.RoleId == 3))).ToList();
                if (result.Count() != 0)
                {
                    counter += result.Count();
                }
            }
            return counter;

        }
        #endregion

        #region سفارش مشتریان عمده

        //سفارش مشتریان عمده در ماه گذشته
        public int OrdinaryOmdehCustomerMonth()
        {
            DateTime ToDate = DateTime.Now;
            DateTime asdate = ToDate.AddDays(-30);
            //DateTime ToDate = DateTime.Now;
            //string toDate = ToDate.ToShortDateString();
            //string yr = toDate.Substring(6, 4);
            //string Month = ToDate.ToShortDateString().Substring(0, 2);
            //int month = (Convert.ToInt32(Month)) - 1;
            //string day = ToDate.ToShortDateString().Substring(3, 2);
            //string asdate = month + "-" + day + "-" + yr;
            DateTime AsDate = asdate;

            ViewBag.AsDate = AsDate;
            ViewBag.ToDate = ToDate;

            int counter = 0;
            for (DateTime asDate = AsDate; asDate <= ToDate; asDate = asDate.AddDays(1))
            {
                string asdateShamsi = asDate.ToShortDateString().ToShamsi();
                var result = db.FactorMains.Where(x => x.Paymentstatus == "100" &&( x.DateCreate == asdateShamsi && (x.RoleId==4||x.RoleId==5)) ).ToList();
                if (result.Count() != 0)
                {
                    counter += result.Count();
                }
            }
            return counter;

        }
        //سفارش مشتریان عمده در هفته گذشته
        public int OrdinaryOmdehCustomerWeek()
        {
            DateTime ToDate = DateTime.Now;
            DateTime asdate = ToDate.AddDays(-7);
            //DateTime ToDate = DateTime.Now;
            //string toDate = ToDate.ToShortDateString();
            //string yr = toDate.Substring(6, 4);
            //string Month = ToDate.ToShortDateString().Substring(0, 2);
            //string day = ToDate.ToShortDateString().Substring(3, 2);
            //int Day = Convert.ToInt32(day) - 7;
            //string asdate = Month + "-" + Day + "-" + yr;
            DateTime AsDate = asdate;

            ViewBag.AsDate = AsDate;
            ViewBag.ToDate = ToDate;

            int counter = 0;
            for (DateTime asDate = AsDate; asDate <= ToDate; asDate = asDate.AddDays(1))
            {
                string asdateShamsi = asDate.ToShortDateString().ToShamsi();
                var result = db.FactorMains.Where(x => x.Paymentstatus == "100" &&( x.DateCreate == asdateShamsi && (x.RoleId == 4 || x.RoleId == 5))).ToList();
                if (result.Count() != 0)
                {
                    counter += result.Count();
                }
            }
            return counter;

        }
        //سفارش مشتریان عمده در روز گذشته
        public int OrdinaryOmdehCustomerDay()
        {
            DateTime ToDate = DateTime.Now;
            DateTime asdate = ToDate.AddDays(-1);
            //DateTime ToDate = DateTime.Now;
            //string toDate = ToDate.ToShortDateString();
            //string yr = toDate.Substring(6, 4);
            //string Month = ToDate.ToShortDateString().Substring(0, 2);
            //string day = ToDate.ToShortDateString().Substring(3, 2);
            //int Day = Convert.ToInt32(day) - 1;
            //string asdate = Month + "-" + Day + "-" + yr;
            DateTime AsDate = asdate;

            ViewBag.AsDate = AsDate;
            ViewBag.ToDate = ToDate;

            int counter = 0;
            for (DateTime asDate = AsDate; asDate <= ToDate; asDate = asDate.AddDays(1))
            {
                string asdateShamsi = asDate.ToShortDateString().ToShamsi();
                var result = db.FactorMains.Where(x => x.Paymentstatus == "100" &&( x.DateCreate == asdateShamsi && (x.RoleId == 4 || x.RoleId == 5))).ToList();
                if (result.Count() != 0)
                {
                    counter += result.Count();
                }
            }
            return counter;

        }
        #endregion

    }
}