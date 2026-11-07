using Salino.Convertor;
using Salino.DataLayer;
using Salino.Models;
using Salino.ToShamsi;
using Salino.Utilities;
using Salino.ViewModels;
using SalinoMvc5.ParsianSale;
using SalinoMvc5.SalinoUtilities;

using System;
using System.Collections.Generic;
using System.Linq;


namespace SalinoMvc5.Controllers
{
    public class PaymentController : SalinoController
    {
        SalinoContext db = new SalinoContext();

        #region Payment

        public ActionResult Payment(string gifcart, string countProduct, string sumAllPrices, string sendtypeId)
        {
            var SumAllPrices_Without_SendPrice =Convert.ToInt32( Session["SumAllPrices"].ToString());
            if(!string.IsNullOrEmpty(sendtypeId))
            {
                int _sendTypeId = int.Parse(sendtypeId);
                var PriceHaml = db.TypeSendPosts.FirstOrDefault(x => x.Id == _sendTypeId)?.PriceHaml;
                if (PriceHaml != null)
                {
                    sumAllPrices = ((SumAllPrices_Without_SendPrice) + int.Parse(PriceHaml)).ToString();
                }
                else
                {
                    return View("/");
                }
            }
            else
            {                
                sumAllPrices = (SumAllPrices_Without_SendPrice).ToString() ;               
            }


          
            if (User.Identity.IsAuthenticated)
            {
                var user = db.users.Where(x => x.Mobile == User.Identity.Name).FirstOrDefault();
                ViewBag.NotCompleteUser = 0;
                if (!String.IsNullOrEmpty(user.UserName)&&
                    !String.IsNullOrEmpty(user.PostalCode)&&
                    !String.IsNullOrEmpty(user.StreetAddress)&&
                     !String.IsNullOrEmpty(user.cityname) )                   
                {
                    ViewBag.NotCompleteUser = 1;
                }
                if (gifcart != "" && gifcart != "0" && gifcart != null)
                {
                    ViewBag.gifcart = gifcart;
                     Session["gifcart"] = gifcart;
                }


                if (countProduct != "" && countProduct != "0" && countProduct != null)
                {
                    ViewBag.countProduct = countProduct;
                    TempData["countProduct"] = countProduct;
                }


                if (sumAllPrices != "" && sumAllPrices != "0" && sumAllPrices != null)
                {
                    ViewBag.sumAllPrices = sumAllPrices;
                    TempData["sumAllPrices"] = sumAllPrices;
                    Session["sumAll"] = int.Parse(sumAllPrices);
                }


                if (sendtypeId != "" && sendtypeId != "0" && sendtypeId != null)
                {
                    ViewBag.sendtypeId = sendtypeId;
                    TempData["sendtypeId"] = sendtypeId;
                }


                if (Session["shopcartMajor"] == null)
                {
                    //محاسبه جشنواره خرید اولی ها
                    if (db.OfferFirstShops.Where(x => x.PriceDiscount != 0).Count() >= 1)
                    {

                        if (db.FactorMains.Where(x => x.UserMobile == User.Identity.Name).Count() == 0)
                        {
                            int OfferFirstShopsPrice = db.OfferFirstShops.OrderByDescending(x => x.Id).FirstOrDefault().PriceDiscount;
                            ViewBag.OfferFirstShopsPrice = OfferFirstShopsPrice;
                            int SumAllPrices = Convert.ToInt32(ViewBag.sumAllPrices);
                            TempData["sumAllPrices"] = SumAllPrices - OfferFirstShopsPrice;
                            ViewBag.OfferFirstShop = "شمابه جشنواره خرید اولی با تخفیف  " + OfferFirstShopsPrice.ToString("#,0تومان") + " " + "وارد شدید.";

                        }
                    }

                    //محاسبه جشنواره خرید مشتریان وفادار
                    if (db.OfferNumberShops.Where(x => x.PriceDiscount != 0).Count() >= 1)
                    {
                        //4
                        int factorcount = db.FactorMains.Where(x => x.UserMobile == User.Identity.Name).Count();
                        //4
                        int offernumber = db.OfferNumberShops.OrderByDescending(x => x.Id).FirstOrDefault().ToNumber;
                        int length = factorcount / offernumber;
                        for (int i = 1; i <= length; i++)
                        {
                            if (offernumber * i == factorcount)
                            {
                                int OfferNumberShopsPrice = db.OfferNumberShops.OrderByDescending(x => x.Id).FirstOrDefault().PriceDiscount;
                                ViewBag.OfferNumberShopsPrice = OfferNumberShopsPrice;
                                int SumAllPrices1 = Convert.ToInt32(TempData["sumAllPrices"]);
                                TempData["sumAllPrices"] = SumAllPrices1 - OfferNumberShopsPrice;
                                ViewBag.OfferNumberShopsPriceText = "شمابه جشنواره مشتریان وفادار با تخفیف  " + OfferNumberShopsPrice.ToString("#,0تومان") + " " + "وارد شدید.";
                            }
                        }
                    }

                }




                return View(user);
            }
            else
            {
                return Redirect("/Account/login");
            }

        }
        #endregion

        #region  ZarinPal  &  Parsian

        #region اکشن اتصال به درگاه
        // GET: Home
        public ActionResult Index(string SumAllPrices = "")
        {
            int senttypeId = 0;
            if (TempData["sendtypeId"]!=null)
            {
                 senttypeId = Convert.ToInt32(TempData["sendtypeId"]);
                if(senttypeId!=0)
                {
                    var SumAllPrices_Without_SendPrice = Convert.ToInt32(Session["SumAllPrices"].ToString());
                    int _sendTypeId = senttypeId;
                    var PriceHaml = db.TypeSendPosts.FirstOrDefault(x => x.Id == _sendTypeId)?.PriceHaml;

                    if (PriceHaml != null)
                    {
                        SumAllPrices = ((SumAllPrices_Without_SendPrice) + int.Parse(PriceHaml)).ToString();
                    }
                }
                else
                {
                    var SumAllPrices_Without_SendPrice = Convert.ToInt32(Session["SumAllPrices"].ToString());                 
                     SumAllPrices = (SumAllPrices_Without_SendPrice).ToString() ;                 
                }
              
            }
            else
            {
                var SumAllPrices_Without_SendPrice = Convert.ToInt32(Session["SumAllPrices"].ToString());
                SumAllPrices = (SumAllPrices_Without_SendPrice).ToString();
            }
          

            ViewBag.sum = Convert.ToInt32(SumAllPrices);
            TempData["sum"] = ViewBag.sum;

            if (senttypeId != 0)
            {
                if (Session["shopcartMajor"] != null)
                {
                    ViewBag.namehaml = db.SendForMajors.Where(x => x.Id == senttypeId).FirstOrDefault().NameHaml;
                    ViewBag.pricesend = db.SendForMajors.Where(x => x.Id == senttypeId).FirstOrDefault().PriceHaml;
                    TempData["TypeSendName"] = ViewBag.namehaml;
                    TempData["TypeSendPrice"] = ViewBag.pricesend;
                }
                else
                {
                    ViewBag.namehaml = db.TypeSendPosts.Where(x => x.Id == senttypeId).FirstOrDefault().NameHaml;
                    ViewBag.pricesend = db.TypeSendPosts.Where(x => x.Id == senttypeId).FirstOrDefault().PriceHaml;
                    TempData["TypeSendName"] = ViewBag.namehaml;
                    TempData["TypeSendPrice"] = ViewBag.pricesend;
                }

            }

            else
            {
                ViewBag.namehaml = "ارسال رایگان";
                ViewBag.pricesend = 0;
                TempData["TypeSendName"] = ViewBag.namehaml;
                TempData["TypeSendPrice"] = ViewBag.pricesend;
            }


            TempData["sendtypeId"] = senttypeId;

             Session["gifcart"] = Convert.ToInt32( Session["gifcart"]);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int price = 0, int typeBank = 0)
        {
            //بانک پارسیان
            if (typeBank == 1)
            {
                var user = db.users.Where(x => x.Mobile == User.Identity.Name).Include(x=>x.Roles).Include(x=>x.Ostans).FirstOrDefault();
                if (user != null)
                {
                    Session["gifcart"] =  Session["gifcart"];
                    int senttypeId = Convert.ToInt32(TempData["sendtypeId"] == null ? 0 : TempData["sendtypeId"]);
                    int discountCode = Convert.ToInt32( Session["gifcart"] == null ? 0 :  Session["gifcart"]);
                    var discount = db.giftcards.Where(x => x.Code == discountCode.ToString()).FirstOrDefault();
                    int PriceHaml = 0;
                    int PriceAll = Convert.ToInt32(price.ToString() + 0);
                    #region filling PriceHaml


                    if (Session["shopcartMajor"] == null)
                    {
                        if (senttypeId != 0)
                        {
                            PriceHaml = Convert.ToInt32(db.TypeSendPosts.Where(x => x.Id == senttypeId).FirstOrDefault().PriceHaml + 0);
                        }
                        else
                        {
                            PriceHaml = 0;
                        }
                    }
                    else
                    {
                        if (senttypeId != 0)
                        {
                            PriceHaml = Convert.ToInt32(db.SendForMajors.Where(x => x.Id == senttypeId).FirstOrDefault().PriceHaml + 0);
                        }
                        else
                        {
                            PriceHaml = 0;
                        }
                    }
                    #endregion
                    #region Filling out part of FactorMain
                    FactorMain FactorMain = new FactorMain();

                    FactorMain.Amount = 0;
                    FactorMain.SumAllPrice = (PriceAll+ PriceHaml)- discountCode;
                    FactorMain.UserId = user.Id;
                    FactorMain.RoleId = user.Roles.Id;
                    FactorMain.Username = user.UserName;
                    FactorMain.UserMobile = user.Mobile;
                    FactorMain.Discount = discountCode == 0 ? 0 : discount.Amount;
                    FactorMain.AddressUser = user.StreetAddress;
                    FactorMain.DateCreate = DateTime.Now.ToShortDateString().ToShamsi();
                    FactorMain.DateCreateDatetime = DateTime.Now;
                    FactorMain.Time = DateTime.Now.ToShortTimeString();
                    FactorMain.Year = FactorMain.DateCreate.ConvertIntYear();
                    FactorMain.Month = FactorMain.DateCreate.ConvertIntMonth();
                    FactorMain.Day = FactorMain.DateCreate.ConvertIntDay();
               
                    if (senttypeId != 0)
                    {
                        FactorMain.TypeSendPostId = Session["shopcartMajor"] == null ? senttypeId : db.SendForMajors.Where(x => x.Id == senttypeId).FirstOrDefault().Id;
                        FactorMain.TypeSendPostName = Session["shopcartMajor"] == null ? db.TypeSendPosts.Where(x => x.Id == senttypeId).FirstOrDefault().NameHaml : db.SendForMajors.Where(x => x.Id == senttypeId).FirstOrDefault().NameHaml;
                    }
                    else
                    {
                        FactorMain.TypeSendPostId = 1;
                        FactorMain.TypeSendPostName = "0";
                    }

                
                    db.FactorMains.Add(FactorMain);
                    db.SaveChanges();
                    #endregion
                    long token = 0;                   
                    ClientPaymentResponseDataBase responseData = null;           
                 
                    var parsian = PaymentOptions.Current.Parsian;
                    string redirectPage = parsian.CallbackBaseUrl.TrimEnd('/') + "/Payment/ReturnParsian/?factormainId=" + FactorMain.Id.ToString();
                    //string redirectPage = "http://localhost:1920/Payment/ReturnParsian/?factormainId=" + FactorMainId.ToString();
                    using (var service = parsian.CreateClient())
                    {
                        var saleRequest = new ClientSaleRequestData();
                        saleRequest.LoginAccount = parsian.LoginAccount;
                        saleRequest.CallBackUrl = redirectPage;
                        saleRequest.Amount = PriceAll;
                        saleRequest.AdditionalData = user.UserName + "" + user.Mobile + "" + user.StreetAddress;
                        saleRequest.Originator = user.Mobile;
                        saleRequest.OrderId = DateTime.Now.Ticks;
                        responseData = service.SalePaymentRequest(saleRequest);                      

                        if (responseData.Status == 0)
                        {
                            token = responseData.Token;
                            Session["Token"] = token;
                            Session["FactorMainId"] = FactorMain.Id;
                            var factor = db.FactorMains.Where(x => x.Id == FactorMain.Id).FirstOrDefault();
                            factor.Amount = ((PriceAll - PriceHaml) * 100) / 1000;
                            factor.SumAllPrice = (PriceAll * 100) / 1000;
                            factor.UserCodePosti = user.PostalCode;
                            factor.UserCity = user.cityname;
                            factor.UserOstan = user.Ostans.ostanname;
                            db.Entry(factor).State = EntityState.Modified;
                            db.SaveChanges();

                            if (token > 0L)
                            {
                                List<ShopCartItem> ShopCartItem = Session["shopcart"] as List<ShopCartItem>;
                                if (ShopCartItem != null)
                                {
                                    foreach (var item in ShopCartItem)
                                    {
                                        var proname = db.Products.Where(x => x.Id == item.ProductID).FirstOrDefault();
                                        FactorDetail Fdetail = new FactorDetail();
                                        Fdetail.FactorMainId = FactorMain.Id;
                                        Fdetail.DetailPrice = Convert.ToInt32(db.FarbicTypes.Find(item.FarbictypeId).PriceMain);
                                        Fdetail.DetailCount = item.Count;
                                        Fdetail.ProductId = item.ProductID;
                                        Fdetail.ProductName = proname.Name;
                                        Fdetail.SumPrice = item.sumproduct;
                                        Fdetail.FactorMainId = FactorMain.Id;
                                        Fdetail.FarbicTypeId = item.FarbictypeId;
                                        Fdetail.FarbicTypeName = db.FarbicTypes.Find(item.FarbictypeId).tiltle;
                                        db.FactorDetails.Add(Fdetail);
                                        db.SaveChanges();
                                    }

                                }
                                return Redirect(parsian.StartPayUrl + token);
                            }
                            
                        }
                        else
                        {
                            ViewBag.Message = responseData.Message.ToString();
                        }

                    }


                }

                return View();
            }

            //زرین پال
            else
            {
                var user = db.users.Where(x => x.Mobile == User.Identity.Name).FirstOrDefault();
                if (user != null)
                {
                    Session["gifcart"] =  Session["gifcart"];
                    int discount = Convert.ToInt32( Session["gifcart"] == null ? 0 :  Session["gifcart"]);
                    int PriceHaml = 0;              
                    int PriceAll = Convert.ToInt32(TempData["sum"]);

                    //نوع ارسال : TypeSendPostId
                    int senttypeId = Convert.ToInt32(TempData["sendtypeId"] == null ? 0 : TempData["sendtypeId"]);
                    if (senttypeId != 0)
                    {
                        PriceHaml = Convert.ToInt32(db.TypeSendPosts.Where(x => x.Id == senttypeId).FirstOrDefault().PriceHaml);
                    }
                    else
                    {
                        PriceHaml = 0;
                    }
                    //نام ارسال :  TypeSendPostName
                    string typeSendPostName = db.TypeSendPosts.Where(x=>x.Id== senttypeId).FirstOrDefault()?.NameHaml;

                    #region Filling out part of FactorMain
                    FactorMain FactorMain = new FactorMain();

                    FactorMain.Amount = PriceAll - PriceHaml;
                    FactorMain.SumAllPrice = PriceAll;
                    FactorMain.UserId = user.Id;
                    FactorMain.Username = user.UserName;
                    FactorMain.UserMobile = user.Mobile;
                    FactorMain.UserCodePosti = user.PostalCode;
                    FactorMain.Discount = discount == 0 ? 0 : db.giftcards.Where(x => x.Code == discount.ToString()).FirstOrDefault().Amount;
                    FactorMain.AddressUser = user.StreetAddress;
                    FactorMain.UserOstan = (db.Ostans.FirstOrDefault(x=>x.Id== user.OstanId)?.ostanname)??"***" ;
                    FactorMain.UserCity = user.cityname;
                    FactorMain.DateCreate = DateTime.Now.ToShortDateString().ToShamsi();
                    FactorMain.DateCreateDatetime = DateTime.Now;
                    FactorMain.Time = DateTime.Now.ToShortTimeString();
                    FactorMain.Year = FactorMain.DateCreate.ConvertIntYear();
                    FactorMain.Month = FactorMain.DateCreate.ConvertIntMonth();
                    FactorMain.Day = FactorMain.DateCreate.ConvertIntDay();
                    FactorMain.StatusAll = StatusAllTypeFactorsEnum.Pending;
                    FactorMain.TypeSendPostId = senttypeId;
                    FactorMain.TypeSendPostName = typeSendPostName??"بدون نام";
                    FactorMain.RoleId = user.RoleId;
                    db.FactorMains.Add(FactorMain);
                    db.SaveChanges();
                    #endregion
                    //_______________________________________________________________________________

                    #region Shared  Connect to port

                    // شماره پرداخت که همان آی دی جدول می باشد
                    int FactorMainId = FactorMain.Id;
                    //string FactormainPaymentNumber = db.FactorMains.Find(FactorMainId).PaymentNumber;

                    // ایجاد یک شی از وب سرویس اتصال به درگاه زرین پال 
                    var zarinPal = PaymentOptions.Current.ZarinPal;
                    var zp = zarinPal.CreateClient();
                    string authority;

                    // کد پذیرنده
                    string merchantCode = zarinPal.MerchantId;

                    // مبلغ به تومان
                    int newPrice = Convert.ToInt32(Convert.ToInt32(TempData["sum"]));
                    #endregion
                    //_______________________________________________________________________________
                    #region Connect to port LocalHost
                    // آدرس برگشت از درگاه              
                    string redirectPage = $"{Request.Scheme}://{Request.Host}/Payment/Return?saleOrderId=" + FactorMainId.ToString();

                    // ارسال اطلاعات به درگاه
                    int status = zp.PaymentRequest(merchantCode, newPrice, "خرید از سایت salinotbz.ir", "", "", redirectPage, out authority);

                    // بررسی وضعیت
                    if (status == 100)
                    {
                        // اتصال به درگاه                
                        return Redirect(zarinPal.StartPayUrl + authority);
                    }
                    else
                    {
                        // ویرایش اطلاعات ارسالی از زرین پال در صورت عدم اتصال
                        FactorMain editFactorMain = db.FactorMains.Find(FactorMainId);

                        if (editFactorMain != null)
                        {
                            // ویرایش خطای عدم اتصال به درگاه
                            editFactorMain.Paymentstatus = Convert.ToString(status);

                            db.Entry(editFactorMain).State = EntityState.Modified;
                            db.SaveChanges();

                        }
                        // نمایش خطا به کاربر
                        ViewBag.Message = ZarinpalResult.Status(status.ToString());
                    }

                    #endregion
                    //_______________________________________________________________________________
                    #region Connect to port SiteMain     


                    //// آدرس برگشت از درگاه
                    //string redirectPage = "http://salinotbz.ir/Payment/Return?saleOrderId=" + FactorMainId.ToString();

                    //// ارسال اطلاعات به درگاه             
                    //int status = zp.PaymentRequest(merchantCode, newPrice, "خرید از سایت salinotbz.ir", "", "", redirectPage, out authority);

                    //// بررسی وضعیت
                    //if (status == 100)
                    //{
                    //    // اتصال به درگاه   

                    //    Response.Redirect("https://www.zarinpal.com/pg/StartPay/" + authority);
                    //}
                    //else
                    //{
                    //    // ویرایش اطلاعات ارسالی از زرین پال در صورت عدم اتصال
                    //    FactorMain editFactorMain = db.FactorMains.Find(FactorMainId);

                    //    if (editFactorMain != null)
                    //    {
                    //        // ویرایش خطای عدم اتصال به درگاه
                    //        editFactorMain.Paymentstatus = Convert.ToString(status);

                    //        db.Entry(editFactorMain).State = EntityState.Modified;
                    //        db.SaveChanges();

                    //    }

                    //    // نمایش خطا به کاربر
                    //    ViewBag.Message = ZarinpalResult.Status(status.ToString());

                    //}

                    #endregion

                }

                return View();
            }

        }

        #endregion

        #region اکشن برگشت از درگاه
        public ActionResult ReturnParsian(int factormainId)
        {

            try
            {
                #region parameters
                int paymentId = Convert.ToInt32(factormainId);
                var token = Convert.ToInt64(Request.Form["Token"].FirstOrDefault());
                var orderId = Convert.ToInt64(Request.Form["OrderId"].FirstOrDefault());
                var terminalNumber = Convert.ToInt32(Request.Form["TerminalNo"].FirstOrDefault());
                var rrn = Convert.ToInt64(Request.Form["RRN"].FirstOrDefault());
                var status = Convert.ToInt16(Request.Form["Status"].FirstOrDefault());
                var amountAsString = Request.Form["Amount"].FirstOrDefault(); //amount is formatted as a currency string
                var DiscountAmount = Request.Form["SwAmount"].FirstOrDefault();
                var cardNumberHashed = Request.Form["HashCardNumber"].FirstOrDefault();
                var amount = Convert.ToInt32(amountAsString);
                #endregion



                ViewBag.paymentId = paymentId;
                ViewBag.token = token;
                ViewBag.orderId = orderId;
                ViewBag.terminalNumber = terminalNumber;               
                ViewBag.status = status;
                ViewBag.amountAsString = amountAsString;

      
                if (token != 0 && status == 0 && rrn != 0 && amount != 0 && terminalNumber != 0)
                {                  
                    if (status == 0)
                    { 
                        FactorMain FactorMain = db.FactorMains.Find(factormainId);
                        if (FactorMain != null)
                        {
                            #region Filling Continue FactorMains
                            FactorMain.Paymentstatus = "100";
                            FactorMain.IsPay = true;
                            FactorMain.StatusAll = StatusAllTypeFactorsEnum.Pending;
                            FactorMain.FactorNumber = FactorMain.FactorNumber + 1;
                            FactorMain.PaymentNumber = terminalNumber.ToString();
                            FactorMain.RRN = rrn.ToString();
                            FactorMain.CardNumberMasked = cardNumberHashed.ToString();

                            db.Entry(FactorMain).State = EntityState.Modified;
                            db.SaveChanges();

                            #endregion                       

                            #region FillFactorDetails
                            List<ShopCartItem> ShopCartItem = Session["shopcart"] as List<ShopCartItem>;
                            if (ShopCartItem != null)
                            {
                                foreach (var item in ShopCartItem)
                                {
                                    var proname = db.Products.Where(x => x.Id == item.ProductID).FirstOrDefault();
                                    FactorDetail Fdetail = new FactorDetail();
                                    Fdetail.FactorMainId = FactorMain.Id;
                                    Fdetail.DetailPrice = Convert.ToInt32(db.FarbicTypes.Find(item.FarbictypeId).PriceMain);
                                    Fdetail.DetailCount = item.Count;
                                    Fdetail.ProductId = item.ProductID;
                                    Fdetail.ProductName = proname.Name;
                                    Fdetail.SumPrice = (Fdetail.DetailPrice * item.Count);
                                    Fdetail.FactorMainId = FactorMain.Id;
                                    Fdetail.FarbicTypeId = item.FarbictypeId;
                                    Fdetail.FarbicTypeName = db.FarbicTypes.Find(item.FarbictypeId).tiltle;

                                    db.FactorDetails.Add(Fdetail);
                                }
                                db.SaveChanges();

                            }
                            #endregion

                            //شماره پرداخت  
                            ViewBag.SaleReferenceId = Request.Query["Authority"].FirstOrDefault();
                            //شماره پیگیری
                            ViewBag.PaymentNumber = terminalNumber.ToString();
                            ViewBag.Message = ZarinpalResult.Status(status.ToString());

                            ViewBag.RRN = rrn.ToString();

                            //درصورت وارد کردن کد تخفیف در اینجا ان را حذف میکنیم
                            if (Session["gifcart"] != null)
                            {
                                var codeGifCart = (Convert.ToInt32(Session["gifcart"])).ToString();
                                var gifcart = db.giftcards.Where(x => x.Code == codeGifCart).FirstOrDefault();


                                if (gifcart != null)
                                {
                                    int NumberUse = gifcart.NumberUse;
                                    for (int i = 1; i <= NumberUse; i++)
                                    {
                                        gifcart.NumberUse -= i;

                                    }
                                    db.Entry(gifcart).State = EntityState.Modified;
                                    if (gifcart.NumberUse == 0)
                                    {
                                        db.Entry(gifcart).State = EntityState.Deleted;
                                    }
                                    db.SaveChanges();
                                }

                                //end
                            }                         
                            ViewBag.Message = "موفق";
                        }
                        else
                        {
                            // در این قسمت می توانید اطلاعات دریافتی را در دیتابیس ذخیره کنید
                            FactorMain factorMain = db.FactorMains.Find(paymentId);

                            if (factorMain != null)
                            {
                                factorMain.PaymentNumber = "0";
                                factorMain.SaleReferenceId = Request.Query["Authority"].FirstOrDefault();
                                factorMain.Paymentstatus = Convert.ToString(status);
                                db.Entry(factorMain).State = EntityState.Modified;
                                db.SaveChanges();

                            }
                            //شماره پرداخت                                          
                            ViewBag.SaleReferenceId = "**************";
                            //شماره پیگیری
                            ViewBag.PaymentNumber = terminalNumber.ToString();
                            ViewBag.Message = ZarinpalResult.Status(status.ToString());
                            ViewBag.Message = "ناموفق";
                        }

                    }
                    else
                    {
                        // در این قسمت می توانید اطلاعات دریافتی را در دیتابیس ذخیره کنید
                        FactorMain factorMain = db.FactorMains.Find(paymentId);

                        if (factorMain != null)
                        {
                            factorMain.PaymentNumber = "0";
                            factorMain.SaleReferenceId = Request.Query["Authority"].FirstOrDefault();
                            factorMain.Paymentstatus = Convert.ToString(status);
                            db.Entry(factorMain).State = EntityState.Modified;
                            db.SaveChanges();

                        }
                        //شماره پرداخت                                          
                        ViewBag.SaleReferenceId = "**************";
                        //شماره پیگیری
                        ViewBag.PaymentNumber = terminalNumber.ToString();
                        ViewBag.Message = ZarinpalResult.Status(status.ToString());
                    }
                }

                else
                {
                    ViewBag.Message = "دسترسی امکانپذیر نیست";
                }

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return  View();
            }
           
        }
        public ActionResult Return(int saleOrderId)
        {
            // بررسی برگشت از درگاه
            if (Request.Query["saleOrderId"].FirstOrDefault() != "" && Request.Query["Status"].FirstOrDefault() != "" && Request.Query["Status"].FirstOrDefault() != null && Request.Query["Authority"].FirstOrDefault() != "" && Request.Query["Authority"].FirstOrDefault() != null)
            {
                #region اطلاعات دریافتی از درگاه
                var _saleOrderId = Request.Query["saleOrderId"].FirstOrDefault();
                var _status = Request.Query["Status"].FirstOrDefault();
                var _authority = Request.Query["Authority"].FirstOrDefault();
           

                #endregion

                // بررسی وضعیت پرداخت
                if (_status=="OK")
                {
                    int paymentId = Convert.ToInt32(saleOrderId);

                    // پیدا کردن مبلغ پرداختی از دیتابیس 
                    int amount = FindAmountPayment(paymentId);

                    // شماره پیگیری
                    long refId = 0;


                    // ایجاد یک شی از وب سرویس اتصال به درگاه زرین پال 
                    var zarinPal = PaymentOptions.Current.ZarinPal;
                    var zp = zarinPal.CreateClient();

                    // کد پذیرنده
                    string merchantCode = zarinPal.MerchantId;

                    // وری فای کردن اطلاعات
                    // int status = zp.PaymentVerification("YOUR - ZARINPAL - MERCHANT - CODE", Request.Query["Authority"].FirstOrDefault(), amount, out refId);
                    int status = zp.PaymentVerification(merchantCode, _authority.ToString(), amount, out refId);

                    // بررسی وضعیت
                    if (status == 100)
                    {
                        // در این قسمت می توانید اطلاعات دریافتی را در دیتابیس ذخیره کنید
                        FactorMain FactorMain = db.FactorMains.Find(paymentId);

                        if (FactorMain != null)
                        {
                            #region Filling Continue FactorMains
                            FactorMain.PaymentNumber = refId.ToString();
                            FactorMain.SaleReferenceId = _authority.ToString();
                            FactorMain.Paymentstatus = Convert.ToString(status);
                            FactorMain.IsPay = true;
                            FactorMain.RRN = _authority.ToString(); 
                            FactorMain.FactorNumber = db.FactorMains.Max(x=>x.FactorNumber)+1; 
                            db.Entry(FactorMain).State = EntityState.Modified;
                            db.SaveChanges();

                            #endregion

                            //درصورت وارد کردن کد تخفیف در اینجا ان را حذف میکنیم
                            if (Session["gifcart"] != null)
                            {
                                var codeGifCart = (Convert.ToInt32(Session["gifcart"])).ToString();
                                var gifcart = db.giftcards.Where(x => x.Code == codeGifCart).FirstOrDefault();


                                if (gifcart != null)
                                {
                                    int NumberUse = gifcart.NumberUse;
                                    for (int i = 1; i <= NumberUse; i++)
                                    {
                                        gifcart.NumberUse -= i;

                                    }
                                    db.Entry(gifcart).State = EntityState.Modified;
                                    if (gifcart.NumberUse == 0)
                                    {
                                        db.Entry(gifcart).State = EntityState.Deleted;
                                    }
                                    db.SaveChanges();
                                }

                                //end
                            }



                            if (Session["shopcartMajor"] != null)
                            {
                                List<ShopCartItem> ShopCartItem = Session["shopcartMajor"] as List<ShopCartItem>;
                                if (ShopCartItem != null)
                                {
                                    foreach (var item in ShopCartItem)
                                    {
                                        var proname = db.Products.Where(x => x.Id == item.ProductID).FirstOrDefault();
                                        FactorDetail Fdetail = new FactorDetail();
                                        Fdetail.FactorMainId = FactorMain.Id;
                                        Fdetail.DetailPrice = Convert.ToInt32(db.FarbicTypes.Find(item.FarbictypeId).PriceMain);
                                        Fdetail.DetailCount = item.Count;
                                        Fdetail.ProductId = item.ProductID;
                                        Fdetail.ProductName = proname.Name;
                                        Fdetail.SumPrice = item.sumproduct;
                                        Fdetail.FactorMainId = FactorMain.Id;
                                        Fdetail.FarbicTypeId = item.FarbictypeId;
                                        Fdetail.FarbicTypeName = db.FarbicTypes.Find(item.FarbictypeId).tiltle;
                                        db.FactorDetails.Add(Fdetail);
                                    }
                                    db.SaveChanges();

                                }
                            }
                            else
                            {
                                List<ShopCartItem> ShopCartItem = Session["shopcart"] as List<ShopCartItem>;
                                if (ShopCartItem != null)
                                {
                                    foreach (var item in ShopCartItem)
                                    {
                                        var proname = db.Products.Where(x => x.Id == item.ProductID).FirstOrDefault();
                                        FactorDetail Fdetail = new FactorDetail();
                                        Fdetail.FactorMainId = FactorMain.Id;
                                        Fdetail.DetailPrice = Convert.ToInt32(db.FarbicTypes.Find(item.FarbictypeId).PriceMain);
                                        Fdetail.DetailCount = item.Count;
                                        Fdetail.ProductId = item.ProductID;
                                        Fdetail.ProductName = proname.Name;
                                        Fdetail.SumPrice = (Fdetail.DetailPrice * item.Count);
                                        Fdetail.FactorMainId = FactorMain.Id;
                                        Fdetail.FarbicTypeId = item.FarbictypeId;
                                        Fdetail.FarbicTypeName = db.FarbicTypes.Find(item.FarbictypeId).tiltle;
                                     
                                        db.FactorDetails.Add(Fdetail);
                                    }
                                    db.SaveChanges();

                                }
                            }

                            //شماره پرداخت  
                            ViewBag.SaleReferenceId = Request.Query["Authority"].FirstOrDefault();
                            //شماره پیگیری
                            ViewBag.PaymentNumber = refId.ToString();
                            ViewBag.Message = ZarinpalResult.Status(status.ToString());




                        }
                        else
                        {
                            // در این قسمت می توانید اطلاعات دریافتی را در دیتابیس ذخیره کنید
                            FactorMain factorMain = db.FactorMains.Find(paymentId);

                            if (factorMain != null)
                            {
                                factorMain.PaymentNumber = "0";
                                factorMain.SaleReferenceId = Request.Query["Authority"].FirstOrDefault();
                                factorMain.Paymentstatus = Convert.ToString(status);
                                db.Entry(factorMain).State = EntityState.Modified;
                                db.SaveChanges();

                            }
                            //شماره پرداخت                                          
                            ViewBag.SaleReferenceId = "**************";
                            //شماره پیگیری
                            ViewBag.PaymentNumber = refId.ToString();
                            ViewBag.Message = ZarinpalResult.Status(status.ToString());
                        }

                    }
                    else
                    {
                        int paymentId1 = Convert.ToInt32(saleOrderId);

                        // در این قسمت می توانید اطلاعات دریافتی را در دیتابیس ذخیره کنید
                        FactorMain FactorMain = db.FactorMains.Find(paymentId1);

                        if (FactorMain != null)
                        {
                            FactorMain.PaymentNumber = "0";
                            FactorMain.SaleReferenceId = Request.Query["Authority"].FirstOrDefault();
                            FactorMain.Paymentstatus = Request.Query["Status"].FirstOrDefault();

                            db.Entry(FactorMain).State = EntityState.Modified;
                            db.SaveChanges();

                        }
                        ViewBag.Message = Request.Query["Status"].FirstOrDefault();
                        ViewBag.SaleReferenceId = "**************";
                        ViewBag.Image = "~/Content/Images/notaccept.png";
                    }
                }

                else
                {
                    ViewBag.Message = "دسترسی امکانپذیر نیست";
                }
            }

            return View();
        }
        #endregion

        #region پیدا کردن مبلغ خرید
        private int FindAmountPayment(long paymentId)
        {
            //int amount = (from p in db.FactorMains
            //              where p.Id == paymentId
            //              select p.SumAllPrice).FirstOrDefault();
            return db.FactorMains.Find((int)paymentId).SumAllPrice;
        }
        #endregion

        #region دیس پوز کردن کانکشن
        /// <summary>
        /// متد اورراید دیسپوز که در صورتی که پارامتر ورودی دیسپوز نشده باشد آنرا دیسپوز میکند
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #endregion

    }
}