

using Salino.Convertor;
using Salino.DataLayer;
using Salino.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using System.Drawing.Imaging;
using System.Drawing;

using Salino.ToShamsi;

namespace SalinoMvc5.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin1")]
    public class ReportController : SalinoController
    {
        private SalinoContext db = new SalinoContext();
     
        public ActionResult Index()
        {
            return View();
        }

        #region ReportsDays
        public ActionResult ReportsDays(string strSrch,string AsDate="", int? page = 1)
        {

            if (page == 1) ViewBag.counter = 1;
            else ViewBag.counter = ((page - 1) * 12) + 1;
            if (!string.IsNullOrEmpty(strSrch))
            {
                Session["pagecurrent"] = page;
               
                long factorNumber = Convert.ToInt64(strSrch);
                var strSearchSaleReferenceId = strSrch.ConvertToNumberEnglish();
                var factorMains = db.FactorMains.
                         Where(x => x.Paymentstatus == "100" &&
                         (x.AddressUser.Contains(strSrch) || x.PaymentNumber.Contains(strSrch) ||
                         x.SaleReferenceId.Contains(strSearchSaleReferenceId) || 
                         x.UserMobile.Contains(strSrch) || x.Username.Contains(strSrch) || x.FactorNumber == factorNumber)).OrderByDescending(x => x.DateCreateDatetime).ToList();
             
         


                return View(factorMains.ToPagedList((int)page, 12));
          

            }
            if(AsDate!="")
            {
                int sumallpriceFactors = 0;
                ViewBag.AsDate = AsDate;
                string asDate = ConvertDate.ConvertToEnglish(AsDate);
                var factorMains = db.FactorMains.Where(x => x.Paymentstatus == "100" && x.DateCreate == asDate).OrderByDescending(x => x.DateCreateDatetime).ToList();
                ViewBag.countFactorsToAsDate = factorMains.Count;
                factorMains.ForEach(x => sumallpriceFactors += x.SumAllPrice);
                ViewBag.sumAllPricesFactorsToAsDate = sumallpriceFactors;
                return View(factorMains.ToPagedList((int)page, 12));
            }
            var factorMains1 = db.FactorMains.Where(x => x.Paymentstatus == "100").OrderByDescending(x => x.DateCreateDatetime).ToList();
                      return View(factorMains1.ToPagedList((int)page, 12));
        }
        [HttpPost]
        public ActionResult ReportsDays( StatusAllTypeFactorsEnum statusAllTypeFactorsEnum=0,StatusAllTypeFactorsEnum change_statusAllTypeFactorsEnum=0, string AsDate = "", string srch = "", int? page = 1)
        {
            int sumallpriceFactors = 0;
            ViewBag.AsDate = AsDate;
            if (page == 1) ViewBag.counter = 1;
            else ViewBag.counter = ((page - 1) * 12) + 1;
         
          
            if (srch != "")
            {
                long srchInt = Convert.ToInt64(srch);
                if (AsDate != "")
                {
                  

                    string asDate = ConvertDate.ConvertToEnglish(AsDate);
                    var factorMains1 = db.FactorMains.
                       Where(x => (x.Paymentstatus == "100" && x.DateCreate == asDate)||x.FactorNumber==srchInt ||x.Amount == srchInt || x.DateCreate.Contains(srch) || x.Discount == srchInt || x.FactorNumber == srchInt || x.PaymentNumber.Contains(srch) || x.Paymentstatus.Contains(srch) || x.SaleReferenceId.Contains(srch) ||  x.Time.Contains(srch) || x.TypeSendPostName.Contains(srch) || x.UserMobile.Contains(srch) || x.Username.Contains(srch)).ToList();
                    ViewBag.AsDate = AsDate;
                    return View(factorMains1.ToPagedList((int)page, 12));
                }             
                var result = db.FactorMains.Where(x => x.Paymentstatus == "100" &&
                x.Amount == srchInt || 
                x.FactorNumber == srchInt ||
                x.DateCreate.Contains(srch) || 
                x.Discount == srchInt ||
                x.FactorNumber == srchInt || 
                x.PaymentNumber.Contains(srch) || 
                x.Paymentstatus.Contains(srch) || 
                x.SaleReferenceId.Contains(srch) ||
                x.Time.Contains(srch) ||
                x.TypeSendPostName.Contains(srch) ||
                x.UserMobile.Contains(srch) ||
                x.Username.Contains(srch)).OrderByDescending(x => x.DateCreate).ToList();


                return View(result.ToPagedList((int)page, 12));
            }
    
            else if (AsDate != "")
            {
                if (statusAllTypeFactorsEnum != 0)
                {
                    string asDate1 = ConvertDate.ConvertToEnglish(AsDate);
                    var factorMains2 = db.FactorMains.
                     Where(x => x.Paymentstatus == "100" && x.DateCreate == asDate1&&x.StatusAll== statusAllTypeFactorsEnum).OrderByDescending(x => x.DateCreate).ToList();
                    ViewBag.statusAllTypeFactorsEnum = (StatusAllTypeFactorsEnum)statusAllTypeFactorsEnum;
                    if (change_statusAllTypeFactorsEnum != 0)
                    {
                        List<FactorMain> factormain_Edit = new List<FactorMain>();
                        foreach (var item in factorMains2)
                        {
                            var factor = db.FactorMains.Where(x => x.Id == item.Id).FirstOrDefault();
                            factor.StatusAll = change_statusAllTypeFactorsEnum;
                            db.Entry(factor).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                      
                    }
                    return View(factorMains2.ToPagedList((int)page, 12));
                }
                string asDate = ConvertDate.ConvertToEnglish(AsDate);
                var factorMains1 = db.FactorMains.
                   Where(x => x.Paymentstatus == "100" && x.DateCreate == asDate).OrderByDescending(x => x.DateCreate).ToList();
                ViewBag.AsDate = AsDate;
                if (factorMains1 == null)
                {
                    ViewBag.mesasgeSearch = "نتیجه جستجو خالی هست";
                    return View();
                }

                ViewBag.countFactorsToAsDate = factorMains1.Count;           
                factorMains1.ForEach(x=> sumallpriceFactors+=x.SumAllPrice);
                ViewBag.sumAllPricesFactorsToAsDate = sumallpriceFactors;

                return View(factorMains1.ToPagedList((int)page, 12));
            }
            var factorMains = db.FactorMains.
                      Where(x => x.Paymentstatus == "100").OrderByDescending(x => x.DateCreate).ToList();
            ViewBag.countFactorsToAsDate = factorMains.Count;        
            factorMains.ForEach(x => sumallpriceFactors += x.SumAllPrice);
            ViewBag.sumAllPricesFactorsToAsDate = sumallpriceFactors;
            return View(factorMains.ToPagedList((int)page, 12));
        }

        public ActionResult ReportsDaysDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FactorMain factorMain = db.FactorMains.Find(id);
            if (factorMain == null)
            {
                return HttpNotFound();
            }
            return View(factorMain);
        }
        public ActionResult ReportsDaysPrint(StatusAllTypeFactorsEnum statusAllTypeFactorsEnum = 0,string AsDate = "")
        {

            if (AsDate != "")
            {
                if (statusAllTypeFactorsEnum != 0)
                {
                    string asDate1 = ConvertDate.ConvertToEnglish(AsDate);
                    var factorMains2 = db.FactorMains.
                Where(x => x.Paymentstatus == "100" && x.DateCreate == asDate1 && x.StatusAll == statusAllTypeFactorsEnum).OrderByDescending(x => x.DateCreate).Include(x => x.FactorDetails).ToList();
                    ViewBag.statusAllTypeFactorsEnum = statusAllTypeFactorsEnum;

                    return View(factorMains2);
                }
                string Asdate = AsDate.ConvertToNumberEnglish();
                var factorMains1 = db.FactorMains.
                   Where(x => x.Paymentstatus == "100" && x.DateCreate == Asdate).Include(x=>x.FactorDetails).OrderByDescending(x => x.DateCreate).ToList();
                ViewBag.AsDate = AsDate;
        
                return View(factorMains1);
            }
            var factorMains = db.FactorMains.
                      Where(x => x.Paymentstatus == "100").Include(x => x.FactorDetails).OrderByDescending(x => x.DateCreate).ToList();
            return View(factorMains);
        }

      

        #endregion

        #region ReportMonth

        public ActionResult ReportMonth(string AsDate = "", string ToDate = "", int? page = 1)
        {
            int sumallpriceFactors = 0;
            if (page == 1) ViewBag.counter = 1;
            else ViewBag.counter = ((page - 1) * 12) + 1;
            if (AsDate != "" && ToDate != "")
            {
                string AsDateEn = AsDate.ConvertToNumberEnglish();
                string ToDateEn = ToDate.ConvertToNumberEnglish();
                DateTime asDate = AsDateEn.PersianDateStringToDateTime();
                DateTime toDate = ToDateEn.PersianDateStringToDateTime();
                ViewBag.AsDate = AsDate;
                ViewBag.ToDate = ToDate;
                List<FactorMain> ListAdding = new List<FactorMain>();

                for (DateTime asDate1 = asDate; asDate1 <= toDate; asDate1 = asDate1.AddDays(1))
                {
                    string asdateShamsi = asDate1.ToShortDateString().ToShamsi();
                    var result1 = db.FactorMains.Where(x => x.Paymentstatus == "100" && (x.DateCreate == asdateShamsi)).ToList();
                    if (result1.Count() != 0)
                    {
                        result1.ForEach(x => ListAdding.Add(x));
                        Session["ListAdding"] = ListAdding as List<FactorMain>;
                    }
                }
                ViewBag.countFactorsToAsDate = ListAdding.Count;
                ListAdding.ForEach(x => sumallpriceFactors += x.SumAllPrice);
                ViewBag.sumAllPricesFactorsToAsDate = sumallpriceFactors;
            }
                var factorMains = db.FactorMains.
                      Where(x => x.Paymentstatus == "100").OrderByDescending(x=>x.DateCreate).ToList();
         
            return View(factorMains.ToPagedList((int)page, 12));
        }
        [HttpPost]
        public ActionResult ReportMonth(StatusAllTypeFactorsEnum statusAllTypeFactorsEnum = 0, string AsDate = "", string ToDate="", string srch = "", int? page = 1)
        {
          
            int sumallpriceFactors = 0;
            if (page == 1) ViewBag.counter = 1;
            else ViewBag.counter = ((page - 1) * 12) + 1;
            if (srch != "")
            {
                long srchInt = Convert.ToInt64(srch);

                if (AsDate != "" && ToDate != "")
                {
                    string AsDateEn = AsDate.ConvertToNumberEnglish();
                    string ToDateEn = ToDate.ConvertToNumberEnglish();
                    DateTime asDate = AsDateEn.PersianDateStringToDateTime();
                    DateTime toDate = ToDateEn.PersianDateStringToDateTime();
                    ViewBag.AsDate = AsDate;
                    ViewBag.ToDate = ToDate;
                    if (statusAllTypeFactorsEnum != 0)
                    {
                        ViewBag.statusAllTypeFactorsEnum = statusAllTypeFactorsEnum;

                        for (DateTime _asDate = asDate; _asDate <= toDate; _asDate = _asDate.AddDays(1))
                        {
                            List<FactorMain> ListAdding = new List<FactorMain>();
                            string asdateShamsi = _asDate.ToShortDateString().ToShamsi();
                            var result1 = db.FactorMains.Where(x => x.Paymentstatus == "100"&&x.StatusAll== statusAllTypeFactorsEnum && (x.DateCreate == asdateShamsi)).ToList();
                            if (result1.Count() != 0)
                            {
                                result1.ForEach(x => ListAdding.Add(x));
                                Session["ListAdding"] = ListAdding as List<FactorMain>;
                            }

                        }
                        List<FactorMain> factor1 = Session["ListAdding"] as List<FactorMain>;
                        Session.Remove("ListAdding");
                        return View(factor1.ToPagedList((int)page, 12));
                    }
                  
                    for (DateTime asDate1 = asDate; asDate1 <= toDate; asDate1 = asDate1.AddDays(1))
                    {

                        List<FactorMain> ListAdding = new List<FactorMain>();                       
                        string asdateShamsi = asDate1.ToShortDateString().ToShamsi();
                        var result1 = db.FactorMains.Where(x => x.Paymentstatus == "100" && (x.DateCreate == asdateShamsi)).ToList();
                        if (result1.Count() != 0)
                        {
                            result1.ForEach(x => ListAdding.Add(x));
                            Session["ListAdding"] = ListAdding as List<FactorMain>;
                        }

                    }
                    List<FactorMain> factor = Session["ListAdding"] as List<FactorMain>;
                    Session.Remove("ListAdding");
                    ViewBag.countFactorsToAsDate = factor.Count;
                    factor.ForEach(x => sumallpriceFactors += x.SumAllPrice);
                    ViewBag.sumAllPricesFactorsToAsDate = sumallpriceFactors;
                    return View(factor.ToPagedList((int)page, 12));

                }
                var result = db.FactorMains.Where(x => x.Paymentstatus == "100"&& x.Amount == srchInt || x.FactorNumber == srchInt || x.DateCreate.Contains(srch) || x.Discount == srchInt || x.FactorNumber == srchInt || x.PaymentNumber.Contains(srch) || x.Paymentstatus.Contains(srch) || x.SaleReferenceId.Contains(srch) || x.Time.Contains(srch) || x.TypeSendPostName.Contains(srch) || x.UserMobile.Contains(srch) || x.Username.Contains(srch)).OrderByDescending(x => x.DateCreate).ToList();
                ViewBag.countFactorsToAsDate = result.Count;
                result.ForEach(x => sumallpriceFactors += x.SumAllPrice);
                ViewBag.sumAllPricesFactorsToAsDate = sumallpriceFactors;
                return View(result.ToPagedList((int)page, 12));
            }
            else if (AsDate != "" && ToDate != "")
            {
                string AsDateEn = AsDate.ConvertToNumberEnglish();
                string ToDateEn = ToDate.ConvertToNumberEnglish();
                DateTime asDate = AsDateEn.PersianDateStringToDateTime();
                DateTime toDate = ToDateEn.PersianDateStringToDateTime();
                ViewBag.AsDate = AsDate;
                ViewBag.ToDate = ToDate;
                if (statusAllTypeFactorsEnum != 0)
                {
                    ViewBag.statusAllTypeFactorsEnum = statusAllTypeFactorsEnum;

                    for (DateTime _asDate = asDate; _asDate <= toDate; _asDate = _asDate.AddDays(1))
                    {
                        List<FactorMain> ListAdding1 = new List<FactorMain>();
                        string asdateShamsi = _asDate.ToShortDateString().ToShamsi();
                        var result1 = db.FactorMains.Where(x => x.Paymentstatus == "100" && x.StatusAll == statusAllTypeFactorsEnum && (x.DateCreate == asdateShamsi)).ToList();
                        if (result1.Count() != 0)
                        {    result1.ForEach(x => ListAdding1.Add(x));
                            Session["ListAdding"] = ListAdding1 as List<FactorMain>;
                        }

                    }
                    List<FactorMain> factor1 = Session["ListAdding"] as List<FactorMain>;
                    Session.Remove("ListAdding");
                    ViewBag.countFactorsToAsDate = factor1.Count;
                    factor1.ForEach(x => sumallpriceFactors += x.SumAllPrice);
                    ViewBag.sumAllPricesFactorsToAsDate = sumallpriceFactors;
                    return View(factor1.ToPagedList((int)page, 12));
                }

                List<FactorMain> ListAdding = new List<FactorMain>();
                for (DateTime asDate1 = asDate; asDate1 <= toDate; asDate1 = asDate1.AddDays(1))
                {
                    var date = DateTime.Now;
                    string asdateShamsi = asDate1.ToShortDateString().ToShamsi();
                    var result1 = db.FactorMains.Where(x => x.Paymentstatus == "100" && (x.DateCreate == asdateShamsi )).ToList();
                    if (result1.Count() != 0)
                    {
                        if(result1.Count() ==1)
                        {
                            ListAdding.Add(result1.FirstOrDefault());
                        }
                        else
                        {
                            result1.ForEach(x => ListAdding.Add(x));
                        }
                     
                        Session["ListAdding"] = ListAdding as List<FactorMain>;
                    }

                }
                List<FactorMain> factor = Session["ListAdding"] as List<FactorMain>;
                if (factor == null)
                {
                    ViewBag.mesasgeSearch = "نتیجه جستجو خالی هست";
                    return View();
                }
                Session.Remove("ListAdding");
                ViewBag.countFactorsToAsDate = factor.Count;
                factor.ForEach(x => sumallpriceFactors += x.SumAllPrice);
                ViewBag.sumAllPricesFactorsToAsDate = sumallpriceFactors;
                return View(factor.ToPagedList((int)page, 12));

            }
            var factorMains = db.FactorMains.
                      Where(x => x.Paymentstatus == "100").OrderByDescending(x => x.DateCreate).ToList();
              return View(factorMains.ToPagedList((int)page, 12));
        }
        public ActionResult ReportMonthPrint(StatusAllTypeFactorsEnum statusAllTypeFactorsEnum = 0, string AsDate = "", string ToDate="", int? page = 1)
        {

            if (AsDate != "" && ToDate != "")
            {
                if (page == 1) ViewBag.counter = 1;
                else ViewBag.counter = ((page - 1) * 12) + 1;


                string AsDateEn = AsDate.ConvertToNumberEnglish();
                string ToDateEn = ToDate.ConvertToNumberEnglish();
                DateTime asDate = AsDateEn.PersianDateStringToDateTime();
                DateTime toDate = ToDateEn.PersianDateStringToDateTime();
                ViewBag.AsDate = AsDate;
                ViewBag.ToDate = ToDate;
                if (statusAllTypeFactorsEnum != 0)
                {
                    ViewBag.statusAllTypeFactorsEnum = statusAllTypeFactorsEnum;

                    List<FactorMain> ListAdding1 = new List<FactorMain>();
                    for (DateTime asDate1 = asDate; asDate1 <= toDate; asDate1 = asDate1.AddDays(1))
                    {
                        string asdateShamsi = asDate1.ToShortDateString().ToShamsi();
                        var result1 = db.FactorMains.Where(x => x.Paymentstatus == "100" &&
                        x.StatusAll== statusAllTypeFactorsEnum && 
                        (x.DateCreate == asdateShamsi)).Include(x => x.FactorDetails).ToList();
                        if (result1.Count() != 0)
                            result1.ForEach(x => ListAdding1.Add(x));                   
                    }
                    return View(ListAdding1);
                }


                List<FactorMain> ListAdding = new List<FactorMain>();
                for (DateTime asDate1 = asDate; asDate1 <= toDate; asDate1 = asDate1.AddDays(1))
                {                              
                    string asdateShamsi = asDate1.ToShortDateString().ToShamsi();
                    var result1 = db.FactorMains.Where(x => x.Paymentstatus == "100" && (x.DateCreate == asdateShamsi)).Include(x => x.FactorDetails).ToList();
                    if (result1.Count() != 0)
                    {
                        result1.ForEach(x => ListAdding.Add(x));
                                            
                    }

                }
                
                return View(ListAdding);

            }
            var factorMains = db.FactorMains.
                      Where(x => x.Paymentstatus == "100").Include(x => x.FactorDetails).OrderByDescending(x => x.DateCreate).ToList();
            return View(factorMains.ToPagedList((int)page, 12));
        }


        #endregion

    }
}