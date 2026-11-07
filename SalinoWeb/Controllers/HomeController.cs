using Salino.DataLayer;
using Salino.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Salino.ViewModels;
namespace SalinoMvc5.Controllers
{
    public class HomeController : SalinoController
    {
        SalinoContext db = new SalinoContext();
        public ActionResult Index()
        {
            return View();

        }
      
        public ActionResult About()
        {
          

            return View(db.Settings.FirstOrDefault());
        }

        public ActionResult Contact()
        {
           

            return View(db.Settings.FirstOrDefault());
        }
        public ActionResult Qustion()
        {


            return View(db.Settings.FirstOrDefault());
        }
        public ActionResult ImageAdvr()
        {
           return PartialView(db.imgadvr.FirstOrDefault());
        }
        public ActionResult ImageOffer()
        {
            return PartialView(db.ImageOffers.FirstOrDefault());
        }
        public ActionResult Social()
        {
            return PartialView(db.Socials.ToList());
        }
        public ActionResult Menu()
        {
            List<ShopCartItem> ShopCartItem = Session["shopcart"] as List<ShopCartItem>;
            
            return PartialView();

        }
        public ActionResult Gavanin()
        {

            return View();
        }
        public ActionResult Mojavez()
        {

            return View(db.Mojavezs.FirstOrDefault());
        }
        public ActionResult AddEntegadat()
        {
            return View();


        }
        [HttpPost]
        public ActionResult AddEntegadat(string Name,string Email,string TextComment)
        {


            if (ModelState.IsValid)
            {


                db.Entegadats.Add(new Entegadat() {
                    Name=Name,Email=Email,TextComment=TextComment
                });
                db.SaveChanges();
                ViewBag.message = " نظر شما ثبت وپاسخ شما به " + Email + "فرستاده خواهد شد.";
                //return RedirectToAction("ShowMessageEntegad",new { @id = model.Id});
                return View();
            }

            else
            {
                ViewBag.errormsg = "ایمیل شما نادرست است .";
                return View();
            }


        }
        public ActionResult TestVueJs()
        {
            return View();
        }
    }
}