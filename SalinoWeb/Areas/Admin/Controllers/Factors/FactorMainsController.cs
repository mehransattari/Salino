using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using Salino.DataLayer;
using Salino.Models;
using Salino.Convertor;

namespace SalinoMvc5.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin1")]
    public class FactorMainsController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

 
            [Authorize(Roles = "Admin1")]
        public ActionResult Index(string strSrch,int? Pending, int? preparing, int? sending, int? sent,int? sentCustom,int? ShowOkpayment, int? page = 1)
        {

           

            if (Session["ShowOkpayment"] != null && ShowOkpayment == null)
            {
                ShowOkpayment = Convert.ToInt32(Session["ShowOkpayment"]);
            }
            ViewBag.ShowOkpayment = ShowOkpayment;
            try
            {
                if (ShowOkpayment != null && ShowOkpayment == 1)
                {
                    if (strSrch != "" && strSrch != null)
                    {
                        var page1 = page == 0 ? 1 : page;
                        Session["pagecurrent"] = page1;
                        Session["ShowOkpayment"] = ShowOkpayment;
                        if (page == 1) ViewBag.counter = 1;
                        else ViewBag.counter = ((page - 1) * 12) + 1;

                        long factorNumber = Convert.ToInt64(strSrch);
                        var strSearchSaleReferenceId = strSrch.ConvertToNumberEnglish();
                        var factorMains = db.FactorMains.
                           Where(x => x.Paymentstatus == "100" &&
                           (x.AddressUser.Contains(strSrch) || x.PaymentNumber.Contains(strSrch) ||
                           x.SaleReferenceId.Contains(strSearchSaleReferenceId) || 
                           x.UserMobile.Contains(strSrch) || x.Username.Contains(strSrch) || x.FactorNumber == factorNumber)).OrderByDescending(x => x.DateCreateDatetime).ToList();

                        //مشاهده وضعیت های ارسال 
                        #region StatusFactorsMains
                        if (Pending != null && Pending == 1)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.Pending).ToPagedList((int)page1, 12));
                        }
                        if (preparing != null && preparing == 2)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.Preparing).ToPagedList((int)page1, 12));
                        }
                        if (sending != null && sending == 3)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.Sending).ToPagedList((int)page1, 12));
                        }
                        if (sent != null && sent == 4)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.CompleteSubmission).ToPagedList((int)page1, 12));
                        }
                        #endregion

                        //مشاهده انواع مشتربان
                        #region RoleCustomer

                        if (sentCustom != null && sentCustom == 3)
                        {
                            return View(factorMains.Where(x => x.RoleId == 3).ToPagedList((int)page1, 12));
                        }
                        if (sentCustom != null && sentCustom == 4)
                        {
                            return View(factorMains.Where(x => x.RoleId == 4).ToPagedList((int)page1, 12));
                        }
                        if (sentCustom != null && sentCustom == 5)
                        {
                            return View(factorMains.Where(x => x.RoleId == 5).ToPagedList((int)page1, 12));
                        }

                        #endregion
                       
                        var r = factorMains.ToPagedList((int)page1, 12);
                        return View(r);

                    }
                    else
                    {
                        var page1 = page == 0 ? 1 : page;
                        Session["pagecurrent"] = page1;
                        Session["ShowOkpayment"] = ShowOkpayment;
                        if (page == 1) ViewBag.counter = 1;
                        else ViewBag.counter = ((page - 1) * 12) + 1;

                        var factorMains = db.FactorMains.Where(x => x.Paymentstatus == "100").OrderByDescending(x => x.DateCreateDatetime).ToList();
                        //مشاهده وضعیت های ارسال 
                        #region StatusFactorsMains
                        if (Pending != null && Pending == 1)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.Pending).ToPagedList((int)page1, 12));
                        }
                        if (preparing != null && preparing == 2)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.Preparing).ToPagedList((int)page1, 12));
                        }
                        if (sending != null && sending == 3)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.Sending).ToPagedList((int)page1, 12));
                        }
                        if (sent != null && sent == 4)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.CompleteSubmission).ToPagedList((int)page1, 12));
                        }
                        #endregion
                        //مشاهده انواع مشتربان
                        #region RoleCustomer

                        if (sentCustom != null && sentCustom == 3)
                        {
                            return View(factorMains.Where(x => x.RoleId == 3).ToPagedList((int)page1, 12));
                        }
                        if (sentCustom != null && sentCustom == 4)
                        {
                            return View(factorMains.Where(x => x.RoleId == 4).ToPagedList((int)page1, 12));
                        }
                        if (sentCustom != null && sentCustom == 5)
                        {
                            return View(factorMains.Where(x => x.RoleId == 5).ToPagedList((int)page1, 12));
                        }

                        #endregion
                   
                        var r = factorMains.ToPagedList((int)page1, 12);
                        return View(r);
                    }
                }
                else if (ShowOkpayment != null && ShowOkpayment == 2)
                {
                    if (strSrch != "" && strSrch != null)
                    {
                        var page1 = page == 0 ? 1 : page;
                        Session["pagecurrent"] = page1;
                        if (page == 1) ViewBag.counter = 1;
                        else ViewBag.counter = ((page - 1) * 12) + 1;

                        long factorNumber = Convert.ToInt64(strSrch);
                        var strSearchSaleReferenceId = strSrch.ConvertToNumberEnglish();
                        var factorMains = db.FactorMains.
                           Where(x => x.Paymentstatus == "100" &&
                           (x.AddressUser.Contains(strSrch) || x.PaymentNumber.Contains(strSrch) ||
                           x.SaleReferenceId.Contains(strSearchSaleReferenceId) || 
                           x.UserMobile.Contains(strSrch) || x.Username.Contains(strSrch) || x.FactorNumber == factorNumber)).ToPagedList((int)page1, 12);

                        //مشاهده وضعیت های ارسال 
                        #region StatusFactorsMains
                        if (Pending != null && Pending == 1)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.Pending).ToList());
                        }
                        if (preparing != null && preparing == 2)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.Preparing).ToList());
                        }
                        if (sending != null && sending == 3)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.Sending).ToList());
                        }
                        if (sent != null && sent == 4)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.CompleteSubmission).ToList());
                        }
                        #endregion
                        //مشاهده انواع مشتربان
                        #region RoleCustomer

                        if (sentCustom != null && sentCustom == 3)
                        {
                            return View(factorMains.Where(x => x.RoleId == 3).ToList());
                        }
                        if (sentCustom != null && sentCustom == 4)
                        {
                            return View(factorMains.Where(x => x.RoleId == 4).ToList());
                        }
                        if (sentCustom != null && sentCustom == 5)
                        {
                            return View(factorMains.Where(x => x.RoleId == 5).ToList());
                        }

                        #endregion
                       
                      
                        return View(factorMains.ToPagedList((int)page1, 12));

                    }
                    else
                    {
                        var page1 = page == 0 ? 1 : page;
                        Session["pagecurrent"] = page1;
                        Session["ShowOkpayment"] = ShowOkpayment;
                        if (page == 1) ViewBag.counter = 1;
                        else ViewBag.counter = ((page - 1) * 12) + 1;

                        var factorMains = db.FactorMains.Where(x => x.Paymentstatus != "100").OrderByDescending(x => x.DateCreateDatetime).ToList();
                        //مشاهده وضعیت های ارسال 
                        #region StatusFactorsMains
                        if (Pending != null && Pending == 1)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.Pending).ToPagedList((int)page1, 12));
                        }
                        if (preparing != null && preparing == 2)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.Preparing).ToPagedList((int)page1, 12));
                        }
                        if (sending != null && sending == 3)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.Sending).ToPagedList((int)page1, 12));
                        }
                        if (sent != null && sent == 4)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.CompleteSubmission).ToPagedList((int)page1, 12));
                        }
                        #endregion
                        //مشاهده انواع مشتربان
                        #region RoleCustomer

                        if (sentCustom != null && sentCustom == 3)
                        {
                            return View(factorMains.Where(x => x.RoleId == 3).ToPagedList((int)page1, 12));
                        }
                        if (sentCustom != null && sentCustom == 4)
                        {
                            return View(factorMains.Where(x => x.RoleId == 4).ToPagedList((int)page1, 12));
                        }
                        if (sentCustom != null && sentCustom == 5)
                        {
                            return View(factorMains.Where(x => x.RoleId == 5).ToPagedList((int)page1, 12));
                        }

                        #endregion
                      
                        var r = factorMains.ToPagedList((int)page1, 12);
                        return View(r);
                    }
                }
                else
                {
                    if (strSrch != "" && strSrch != null)
                    {
                        var page1 = page == 0 ? 1 : page;
                        Session["pagecurrent"] = page1;
                        if (page == 1) ViewBag.counter = 1;
                        else ViewBag.counter = ((page - 1) * 12) + 1;


                        long factorNumber = Convert.ToInt64(strSrch);
                        var strSearchSaleReferenceId = strSrch.ConvertToNumberEnglish();
                        var factorMains = db.FactorMains.
                            Where(x => x.Paymentstatus == "100" &&
                            (x.AddressUser.Contains(strSrch) || x.PaymentNumber.Contains(strSrch) ||
                            x.SaleReferenceId.Contains(strSearchSaleReferenceId) || 
                            x.UserMobile.Contains(strSrch) || x.Username.Contains(strSrch) || x.FactorNumber == factorNumber)).OrderByDescending(x => x.DateCreateDatetime).ToList();

                        //مشاهده وضعیت های ارسال 
                        #region StatusFactorsMains
                        if (Pending != null && Pending == 1)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.Pending).ToPagedList((int)page1, 12));
                        }
                        if (preparing != null && preparing == 2)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.Preparing).ToPagedList((int)page1, 12));
                        }
                        if (sending != null && sending == 3)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.Sending).ToPagedList((int)page1, 12));
                        }
                        if (sent != null && sent == 4)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.CompleteSubmission).ToPagedList((int)page1, 12));
                        }
                        #endregion
                        //مشاهده انواع مشتربان
                        #region RoleCustomer

                        if (sentCustom != null && sentCustom == 3)
                        {
                            return View(factorMains.Where(x => x.RoleId == 3).ToPagedList((int)page1, 12));
                        }
                        if (sentCustom != null && sentCustom == 4)
                        {
                            return View(factorMains.Where(x => x.RoleId == 4).ToPagedList((int)page1, 12));
                        }
                        if (sentCustom != null && sentCustom == 5)
                        {
                            return View(factorMains.Where(x => x.RoleId == 5).ToPagedList((int)page1, 12));
                        }

                        #endregion
                       
                        var r = factorMains.ToPagedList((int)page1, 12);
                        return View(r);

                    }
                    else
                    {
                        Session.Remove("ShowOkpayment");
                        var page1 = page == 0 ? 1 : page;
                        Session["pagecurrent"] = page1;
                        if (page == 1) ViewBag.counter = 1;
                        else ViewBag.counter = ((page - 1) * 12) + 1;

                        var factorMains = db.FactorMains.Where(x=>x.Paymentstatus=="100").OrderByDescending(x=>x.DateCreateDatetime).ToList(); 
                        //مشاهده وضعیت های ارسال 
                        #region StatusFactorsMains
                        if (Pending != null && Pending == 1)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.Pending).ToPagedList((int)page1, 12));
                        }
                        if (preparing != null && preparing == 2)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.Preparing).ToPagedList((int)page1, 12));
                        }
                        if (sending != null && sending == 3)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.Sending).ToPagedList((int)page1, 12));
                        }
                        if (sent != null && sent == 4)
                        {
                            return View(factorMains.Where(x => x.StatusAll == StatusAllTypeFactorsEnum.CompleteSubmission).ToPagedList((int)page1, 12));
                        }
                        #endregion
                        //مشاهده انواع مشتربان
                        #region RoleCustomer

                        if (sentCustom != null && sentCustom == 3)
                        {
                            return View(factorMains.Where(x => x.RoleId == 3).ToPagedList((int)page1, 12));
                        }
                        if (sentCustom != null && sentCustom == 4)
                        {
                            return View(factorMains.Where(x => x.RoleId == 4).ToPagedList((int)page1, 12));
                        }
                        if (sentCustom != null && sentCustom == 5)
                        {
                            return View(factorMains.Where(x => x.RoleId == 5).ToPagedList((int)page1, 12));
                        }

                        #endregion

                       
                       
                        return View(factorMains.ToPagedList((int)page1, 12));
                    }
                }

            }
            catch (Exception ex)
            {
             
              return Redirect("~/Home/Error");
              
            }

              
            }

        [Authorize(Roles = "Admin1")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FactorMain factorMain = db.FactorMains.Find(id);
            int currentPage = Convert.ToInt32(Session["pagecurrent"]);
            ViewBag.currentPage = currentPage;
            if (factorMain == null)
            {
                return HttpNotFound();
            }
            return View(factorMain);
        }

        [Authorize(Roles = "Admin1")]
        public ActionResult Create()
        {
            int currentPage = Convert.ToInt32(Session["pagecurrent"]);
            ViewBag.currentPage = currentPage;
            ViewBag.TypeSendPostId = new SelectList(db.TypeSendPosts, "Id", "NameHaml");
            return View();
        }

        [Authorize(Roles = "Admin1")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( FactorMain factorMain)
        {
            if (ModelState.IsValid)
            {

                factorMain.TypeSendPostName = factorMain.RoleId == 3 ?
                    db.TypeSendPosts.Find(factorMain.TypeSendPostId).NameHaml :
                    db.SendForMajors.Find(factorMain.TypeSendPostId).NameHaml;

                db.FactorMains.Add(factorMain);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TypeSendPostId = new SelectList(db.TypeSendPosts, "Id", "NameHaml", factorMain.TypeSendPostId);
            return View(factorMain);
        }



        #region StatusAll
        //درحال بررسی
        public int Pending(string[] checkedNames)
        {
            foreach (string item in checkedNames)
            {
                int FactorMainId = Convert.ToInt32(item);
                FactorMain factorMain = db.FactorMains.Find(FactorMainId);
                factorMain.StatusAll = StatusAllTypeFactorsEnum.Pending;
                db.Entry(factorMain).State = EntityState.Modified;
                db.SaveChanges();
               

            }
            return 1;
        }
      
        //درحال آماده سازی
        public int preparing(string[] checkedNames)
        {
            foreach (string item in checkedNames)
            {
                int FactorMainId = Convert.ToInt32(item);
                FactorMain factorMain = db.FactorMains.Find(FactorMainId);
                factorMain.StatusAll = StatusAllTypeFactorsEnum.Preparing;
                db.Entry(factorMain).State = EntityState.Modified;
                db.SaveChanges();
            

            }
            return 1;
        }
        //درحال ارسال
        public int sending(string[] checkedNames)
        {
            foreach (string item in checkedNames)
            {
                int FactorMainId = Convert.ToInt32(item);
                FactorMain factorMain = db.FactorMains.Find(FactorMainId);
                factorMain.StatusAll = StatusAllTypeFactorsEnum.Sending;
                db.Entry(factorMain).State = EntityState.Modified;
                db.SaveChanges();
          

            }
            return 1;
        }
        //ارسال شد
        public int Sent(string[] checkedNames)
        {
            foreach (string item in checkedNames)
            {
                int FactorMainId = Convert.ToInt32(item);
                FactorMain factorMain = db.FactorMains.Find(FactorMainId);
                factorMain.StatusAll = StatusAllTypeFactorsEnum.CompleteSubmission;
                db.Entry(factorMain).State = EntityState.Modified;
                db.SaveChanges();
    

            }
            return 1;
        }

        #endregion

        #region ReportFactors
        public ActionResult ReportFactors(int?id)
        {
            if(id!=null)
            {
                int typeSend =Convert.ToInt32( Request.Param("typesend"));
                int discount= Convert.ToInt32(Request.Param("Discount"));
                ViewBag.discount = discount;
                if (typeSend==2)
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

        //Convert To ScreenShot
        //public ActionResult ReportFactorsScreenShot(int? id)
        //{
        //    var screenShotJob = ScreenshotJobBuilder.Create("ReportFactorsPrint", "FactorMains", new { id = id })
        //    .SetTransfertRequestCookies(true); // forward session cookies to the capturing browser

        //    return screenShotJob.Freeze();

        //}

       
        //Convert To Pdf   

        public ActionResult ReportFactorsToPdf(int id)
        {
            var report = new ActionAsPdf("ReportFactorsPrint", new { id = id });
            return report;

            //return new ActionAsPdf("ReportFactorsPrint", new { id = id })
            //{ 
            //  RotativaOptions= new DriverOptions()
            //  {
            //    PageOrientation = Orientation.Landscape,
            //    PageSize = Size.A3,
            //    IsLowQuality = true,
            //    PageWidth=1020,
            //    MinimumFontSize=9,
                            
            //   }
            //};
         
        }
        #endregion
    


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
