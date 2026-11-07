using Salino.DataLayer;
using Salino.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Salino.Utilities;
using System.Net;
using Newtonsoft.Json;
using Salino.ViewModels;
using Salino.Convertor;
using SalinoMvc5.SalinoUtilities;
using System.Threading.Tasks;

namespace SalinoMvc5.Controllers
{
    public class UserController : SalinoController
    {
        SalinoContext db = new SalinoContext();
        //______________________________________________________________________________
        #region register

        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            //recatpcha domain site key :6LdCOcgUAAAAAJKXe57k7qno7pI_CO08yVeqzC7-  ,secret key:6LdCOcgUAAAAAKzQBizSMCHemIjZWv_CFrEVFVue
            //recatpcha localhost site key :6LdnOcgUAAAAAGm0Q4kf3noZI8Wjck4Fc-AvWJ05,secret key:6LdnOcgUAAAAAPNYWyaeRoh_-fisNL68nXUx4oan
        

            if (this.IsCaptchaValid("لطفا کد امنیتی راوارد کنید!"))
            {
                if (ModelState.IsValid)
                {
                    string Mobile = user.Mobile.ConvertToNumberEnglish();
                    var username = db.users.Where(x => x.Mobile == Mobile).FirstOrDefault();
                    if (username == null)
                    {
                        if (user.ostanname != "0" && user.ostanname != null && user.cityname != "0" && user.cityname != null)
                        {
                            if (user.RoleId != 0)
                            {

                                string pass = user.UserPassword.ToHashPassword();
                                user.CodeActivate = RandomNumber.RandomMake().ToString();
                                user.UserPassword = pass;
                                user.RoleId = user.RoleId;
                                user.OstanId = int.Parse(user.ostanname);

                                db.users.Add(user);
                                db.SaveChanges();

                                int result = SmsService.SendSms(user.Id);
                                if (result == 1)
                                {
                                    return RedirectToAction(nameof(Activate), new { userid = user.Id });
                                }
                                else
                                {
                                    ViewBag.msg = " .درارسال کدفعالسازی خطا رخ داده است ";
                                    return View();
                                }



                            }
                            else
                            {
                                ViewBag.msg = "  نقش خود را  وارد نمائید.";
                                return View();
                            }

                        }
                        else
                        {
                            ViewBag.msg = "لطفا استان وشهر خود را  وارد نمائید.";
                            return View();
                        }

                    }
                    else
                    {
                        ViewBag.msg = "شماره همراه  قبلا ثبت شده است.";
                        return View();
                    }

                }
                return View();
            }
            else
            {
                ViewBag.msgCaptcha = "لطفا کد امنیتی راتایید کنید!";
                return View();
            }






        }



        public ActionResult RegisterDisposable()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterDisposable(ViewModels.RegisterDisposableViewModel user)
        {
          
                if (ModelState.IsValid)
                {
                    string Mobile = user.RegisterDtoDisposable_Mobile.ConvertToNumberEnglish();
                    var username = db.users.Where(x => x.Mobile == Mobile).FirstOrDefault();
                    if (username == null)
                    {

                                   var user1 = new User(); 
                                user1.CodeActivate = RandomNumber.RandomMake().ToString();
                              
                                user1.RoleId = 2;
                    user1.UserPassword = user1.CodeActivate.ToHashPassword();

                                db.users.Add(user1);
                                db.SaveChanges();

                                int result = SmsService.SendSms(user1.Id);
                                if (result == 1)
                                {
                                    return RedirectToAction(nameof(loginDisposable), new { userid = user1.Id });
                                }
                                else
                                {
                                    ViewBag.msg = " .درارسال کدفعالسازی خطا رخ داده است ";
                                    return View();
                                }



                           
                           

                    }
                    else
                    {

                    username.CodeActivate = RandomNumber.RandomMake().ToString();

                    username.UserPassword = username.CodeActivate.ToHashPassword();
                    db.Entry(username).State = EntityState.Modified;
                    db.SaveChanges();
                    int result = SmsService.SendSms(username.Id);
                    if (result == 1)
                    {
                        return RedirectToAction(nameof(loginDisposable), new { userid = username.Id });
                    }
                    else
                    {
                        ViewBag.msg = " .درارسال کدفعالسازی خطا رخ داده است ";
                        return View();
                    }
                }

                }
                return View();
         





        }
        #endregion
        //______________________________________________________________________________
        #region login

        public ActionResult login()
        {

            return View();
        }
  
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult login(User user)
        {


            if (this.IsCaptchaValid("لطفا کد امنیتی راوارد کنید!"))
            {
                if (user.Mobile != null && user.UserPassword != null)
                {
                    string pass = user.UserPassword.ToHashPassword();
                    string Mobile = user.Mobile.ConvertToNumberEnglish();
                    var IsUser = db.users.Where(x => x.Mobile == Mobile && x.UserPassword == pass).FirstOrDefault();

                    if (IsUser != null)
                    {

                        IsUser.CodeActivate = RandomNumber.RandomMake().ToString();
                        db.Entry(IsUser).State = EntityState.Modified;
                        db.SaveChanges();

                        if (IsUser.IsActive == true)
                        {
                            this.SetAuthCookie(IsUser.Mobile, true);
                            ViewBag.msgSuccess = "true";
                            if (IsUser.Roles.Id == 1)
                            {

                                return RedirectToAction("Index", "Home");
                            }
                            else if (IsUser.Roles.Id == 2)
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else if (IsUser.Roles.Id == 3)
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else if (IsUser.Roles.Id == 5)
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {




                                return View();
                            }



                        }
                        else
                        {


                            int result = SmsService.SendSms(IsUser.Id);
                            if (result == 1)
                            {
                                return RedirectToAction(nameof(Activate), new { userid = IsUser.Id });
                            }
                            else
                            {
                                ViewBag.msg = " .درارسال کدفعالسازی خطا رخ داده است ";
                                return View();
                            }





                        }
                    }

                    else
                    {
                        ViewBag.MsgPassLogin = "شماره همراه یا رمزعبور شمااشتباه است .";
                        return View("login");
                    }

                }
                return View();
            }
            else
            {
                ViewBag.msgCaptcha = "لطفا کد امنیتی راتایید کنید!";
                return View("login");
            }

        }




        public ActionResult loginDisposable(int userid)
        {
            var username = db.users.Where(x => x.Id == userid).FirstOrDefault();
            ViewBag.usermobile = username.Mobile;
         
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult loginDisposable(ViewModels.LoginDisposableViewModel user)
        {


           
                    string pass = user.LoginDtoDisposable_Password.ToHashPassword();
                    string Mobile = user.LoginDtoDisposable_Mobile.ConvertToNumberEnglish();
                    var IsUser = db.users.Where(x => x.Mobile == Mobile && x.UserPassword == pass).FirstOrDefault();

                    if (IsUser != null)
                    {

                        IsUser.CodeActivate = RandomNumber.RandomMake().ToString();
                        db.Entry(IsUser).State = EntityState.Modified;
                        db.SaveChanges();

                        if (IsUser.IsActive == true)
                        {
                            this.SetAuthCookie(IsUser.Mobile, true);
                            ViewBag.msgSuccess = "true";
                            if (IsUser.Roles.Id == 1)
                            {

                                return RedirectToAction("Index", "Home");
                            }
                            else if (IsUser.Roles.Id == 2)
                            {
                                return RedirectToAction("PanelUser", "Index");
                            }
                            else if (IsUser.Roles.Id == 3)
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else if (IsUser.Roles.Id == 5)
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {

                                return View();
                            }
                        }
                        else
                        {


                            int result = SmsService.SendSms(IsUser.Id);
                            if (result == 1)
                            {
                                return RedirectToAction(nameof(Activate), new { userid = IsUser.Id });
                            }
                            else
                            {
                                ViewBag.msg = " .درارسال کدفعالسازی خطا رخ داده است ";
                                return View();
                            }





                        }
                    }

            return View();

             
              
          

        }

        #endregion
        //______________________________________________________________________________
        #region Signout
        public ActionResult Signout()
        {
            this.SignOut();
            Session.Remove("shopcart");
            Session.Remove("shopcartMajor");
            return RedirectToAction("login");
        }
        #endregion
        //______________________________________________________________________________
        #region ForgetPassword
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgetPassword(ForgetPasswordViewModel forgetPass)
        {
            if (ModelState.IsValid)
            {
                string mobile = forgetPass.Mobile.ConvertToNumberEnglish();
                var IsMobile = db.users.Where(x => x.Mobile == mobile).FirstOrDefault();
                if (IsMobile != null)
                {
                    int result = SmsService.SendSms(IsMobile.Id);
                    if (result == 1)
                    {
                        return RedirectToAction(nameof(NewPassword),new { userId= IsMobile .Id});
                    }
                    else
                    {
                        ViewBag.msg = " .درارسال کدفعالسازی خطا رخ داده است ";
                        return View();
                    }

                }
                else
                {
                    ViewBag.Error = " . شماره موبایل در سیستم موجود نیست ";
                    return View();
                }
             

            }
            return View(forgetPass);
        }
        #endregion
        //______________________________________________________________________________
        #region NewPassword
        public ActionResult NewPassword(int ? userId)
        {
            if (userId!=null)
            {
                ViewBag.mobile = db.users.Find(userId).Mobile;
            }
         
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewPassword(NewPasswordViewModel newpass)
        {
            if (ModelState.IsValid)
            {
                string codeactive = (newpass.CodeActivate.ToString().ConvertToNumberEnglish());
                var IsMobile = db.users.Where(x => x.CodeActivate == codeactive).FirstOrDefault();
                if (IsMobile != null)
                {
                    string pass = newpass.UserPassword.ToHashPassword();
                    IsMobile.UserPassword = pass;
                    IsMobile.CodeActivate = RandomNumber.RandomMake().ToString();
                    db.Entry(IsMobile).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction(nameof(login));

                }
                else
                {
                    ViewBag.Error = " . شماره موبایل در سیستم موجود نیست ";
                    return View();
                }

            }
            return View(newpass);
        }
        #endregion
        //______________________________________________________________________________
        #region Activate

        public ActionResult Activate(int? userid)
        {
         
            if (userid != null)
            {
                var user = db.users.Find(userid);
                var userMobile = user.Mobile;
                ViewBag.mobile = userMobile;
                if (db.roles.Find(user.RoleId).RoleName == "User2" ||db. roles.Find(user.RoleId).RoleName == "User3")
                {
                    ViewBag.MessageForUser23 = "مشتری عمده محترم بعداز بررسی های لازم برای فعال کردن حساب خود با شما تماس میگیریم .";
                    return View();
                }
                else
                {
                  
                    return View();
                }
              
            }
            else
            {
                return View();
            }
           
        }
        [HttpPost]

        public ActionResult Activate(ActivateCodeViewModel activate)
        {
            if (ModelState.IsValid)
            {

                var userMe = db.users.Where(x => x.CodeActivate == activate.UserCode).FirstOrDefault();
                if (userMe != null)
                {
                    if (db.roles.Find(userMe.RoleId).RoleName == "User2" || db.roles.Find(userMe.RoleId).RoleName == "User3")
                    {
                        return Redirect("/Home/Index");
                    }
                    else
                    {
                        userMe.IsActive = true;
                        db.Entry(userMe).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction(nameof(login));
                    }
                  

                }
                else
                {
                    ModelState.AddModelError("UserCode", "کدتایید شما درست نیست.");
                }

            }
            return View();

        }

        #endregion
        //______________________________________________________________________________
        #region methodCaptcha

        //public static bool ValidateCaptcha(string response)
        //{
        //    //secret that was generated in key value pair  
        //    string secret = WebConfigurationManager.AppSettings["recaptchaPrivateKey"];

        //    var client = new WebClient();
        //    var reply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

        //    var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

        //    return Convert.ToBoolean(captchaResponse.Success);

        //}
        #endregion
        //______________________________________________________________________________
   
    }
}