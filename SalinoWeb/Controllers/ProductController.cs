using Salino.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using Salino.Models;
using Salino.ViewModels;
using System.Net;
using System.IO;

using Salino.Security;
using Salino.Convertor;
using System.Threading;

namespace SalinoMvc5.Controllers
{
    public class ProductController : SalinoController
    {
        SalinoContext db = new SalinoContext();

        #region Searches
        [HttpPost]
        public ActionResult Search(string search)
        {
            ViewBag.MonasebatId = new SelectList(db.Monasebats, "Id", "MonasebatTitle");
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "color");
            if (!String.IsNullOrWhiteSpace(search))
            {
                ViewBag.TextSearch = "متن جستجو: " + search;
                var searchConvert = search.ConvertToNumberPersioan();
                var Product = db.Products.Where(x => x.GroupProduct.GroupTitle.Contains(search) || x.CodeKala.Contains(searchConvert) || x.Color.color.Contains(search) || x.Description.Contains(search) || x.Name.Contains(search)).ToList();
                if (Product.Count() != 0)
                {
                    Session["ProductForSearchText"] = Product;
                    var list = GetData(Product,null, pageNumber: 0);
                    return View(list);
                   
                }
                else
                {
                    ViewBag.TextSearch = "متن جستجو: " + "خالی";
                    var result = db.Products.Where(x => x.IsShow == true).ToList();
                    Session["ProductForSearchText"] = result;
                    var list = GetData(result, null, pageNumber: 0);
                    return View(list);
                  
                }

            }

            else
            {
                ViewBag.TextSearch = "متن جستجو: " + "خالی";
                var result = db.Products.Where(x => x.IsShow == true).ToList();
                Session["ProductForSearchText"] = result;
                var list = GetData(result, null, pageNumber: 0);
                return View(list);
            }

        }
        [HttpPost]
        public ActionResult SearchByCodeKala(string search)
        {
            ViewBag.MonasebatId = new SelectList(db.Monasebats, "Id", "MonasebatTitle");
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "color");
            if (!String.IsNullOrWhiteSpace(search))
            {
                ViewBag.TextSearch = "کدکالا: " + search;
                var searchConvert = search.ConvertToNumberPersioan();
                var Product = db.Products.Where(x => x.CodeKala.Contains(search) ).ToList();
                if (Product.Count() != 0)
                {
                    Session["ProductForSearchText"] = Product;
                    var list = GetData(Product, null, pageNumber: 0);
                    return View(nameof(Search),list);
                }
                else
                {
                    ViewBag.TextSearch = "کدکالا: " + "تائید نشده";
                    Session["ProductForSearchText"] = Product;
                    var list = GetData(Product, null, pageNumber: 0);
                    return View(nameof(Search),list);
              
                }

            }
            else
            {
                ViewBag.TextSearch = "متن جستجو: " + "خالی";
                var Product = db.Products.Where(x => x.IsShow == true).ToList();
                Session["ProductForSearchText"] = Product;
                var list = GetData(Product, null, pageNumber: 0);
                return View(nameof(Search), list);
            }

        }
        [HttpPost]
        public ActionResult SearchByColorKala(int? ColorId)
        {
            ViewBag.MonasebatId = new SelectList(db.Monasebats, "Id", "MonasebatTitle");
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "color");
            if (ColorId != 0 && ColorId != null)
            {
                string colorname = db.Colors.Find(ColorId).color;
                ViewBag.TextSearch = "رنگ کالا: " + colorname;
                var Product = db.Products.Where(x => x.Color.color.Contains(colorname) && x.IsShow == true).ToList();
                if (Product.Count() != 0)
                {
                    Session["ProductForSearchText"] = Product;
                    var list = GetData(Product, null, pageNumber: 0);
                    return View(nameof(Search), list);
                }
                else
                {
                    //ViewBag.TextSearch = "رنگ کالا: " + "تائید نشده";
                    Session["ProductForSearchText"] = Product;
                    var list = GetData(Product, null, pageNumber: 0);
                    return View(nameof(Search), list);
                }

            }
            else
            {
                ViewBag.TextSearch = "متن جستجو: " + "خالی";
                var Product = db.Products.Where(x => x.IsShow == true).ToList();
                Session["ProductForSearchText"] = Product;
                var list = GetData(Product, null, pageNumber: 0);
                return View(nameof(Search), list);
            }

        }
        [HttpPost]
        public ActionResult SearchByMonasebat(string search)
        {
            ViewBag.MonasebatId = new SelectList(db.Monasebats, "Id", "MonasebatTitle");
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "color");
            if (!String.IsNullOrWhiteSpace(search))
            {

                int mId = Convert.ToInt32(search);
                var monasebatId = mId;
                string monasebatTitle = db.Monasebats.Where(x => x.Id == mId).FirstOrDefault().MonasebatTitle;
                ViewBag.TextSearch = "مناسبت: " + monasebatTitle;
                var Product = db.MonasebatProducts.Where(x => x.MonasebatId == monasebatId).ToList();
                List<Product> products = new List<Salino.Models.Product>();
                foreach (var item in Product)
                {
                    Product productItem = db.Products.Where(x => x.Id == item.ProductId && x.IsShow == true).FirstOrDefault();
                    if(productItem!=null)
                    {
                        Session["ProductForSearchText"] = productItem;
                        products.Add(productItem);
                    }
                 
                }

                if (products.Count() != 0)
                {
                  
                    var list = GetData(products, null, pageNumber: 0);
                    Session["ProductForSearchText"] = products;
                    return View(nameof(Search), list);
                }
                else
                {
                    ViewBag.TextSearch = "مناسبت: " + "تائید نشده";
              
                    var list = GetData(db.Products.Where(x => x.IsShow == true).ToList(), null, pageNumber: 0);
                    Session["ProductForSearchText"] = db.Products.Where(x => x.IsShow == true).ToList();
                    return View(nameof(Search), list);
                }

            }
            else
            {
                ViewBag.TextSearch = "متن جستجو: " + "خالی";
                var Product = db.Products.Where(x => x.IsShow == true).ToList();
                Session["ProductForSearchText"] = Product;
                var list = GetData(Product, null, pageNumber: 0);
                return View(nameof(Search), list);
            }

        }
        #endregion

        #region ShowProducts


        #region ListProduct With Scroll Page


        public ActionResult ListProducts(int? Id,int page=1)
        {
            ViewBag.MonasebatId = new SelectList(db.Monasebats, "Id", "MonasebatTitle");
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "color");
         
            if (Id != 0)
            {             
                Session["GroupId"] = Id;            
            }          
            //آغاز کار با صفحه صفر است
           // var list = GetData(null,Id, pageNumber: 0);
            var Product = db.Products.Where(x => x.GroupId == Id && x.IsShow == true).OrderByDescending(x => x.CreateDateEnglish).ToList();
            ViewBag.CountProducts = Product.Count();
            ViewBag.pagecurrent = page;
            ViewBag.groupId = Id;
            return View(Product.ToPagedList(page,18)); //نمایش ابتدایی صفحه
        }
        [HttpPost]
        [AjaxOnly]
        public virtual ActionResult PagedIndex(int? page)
        {
            int groupId = Convert.ToInt32(Session["GroupId"]);
            var pageNumber = page ?? 0;
            if (Session["ProductForSearchText"]!=null)
            {
                var Products = Session["ProductForSearchText"] as List<Product>;
                var list1 = GetData(Products, groupId, pageNumber);
                if (list1 == null || !list1.Any())
                    return Content("no-more-info"); //این شرط ما است برای نمایش عدم یافتن رکوردها

                return PartialView(list1);
            }
 
            var list = GetData(null, groupId, pageNumber);
            if (list == null || !list.Any())
                return Content("no-more-info"); //این شرط ما است برای نمایش عدم یافتن رکوردها

            return PartialView(list);


        }
        [HttpGet]    
        public ActionResult Post(int? id)
        {
            if (id == null)
                return Redirect("/");

            //todo: show the content here
            return Content("Post " + id.Value);
        }

        public IList<Product> GetData(IList<Product> products,int? Id,int pageNumber, int recordsPerPage = 12)
        {
            var skipRecords = pageNumber * recordsPerPage;
            if(products!=null)
            {
                if (Id != null&&Id!=0)
                {
                    var Product = products.Where(x => x.IsShow == true).OrderByDescending(x => x.CreateDateEnglish).Skip(skipRecords).Take(recordsPerPage).ToList();
                    Session["GroupId"] = Id;
                    ViewBag.Grouptitle = db.groupProducts.Where(x => x.Id == Id && x.GroupNotShow == true).FirstOrDefault().GroupTitle;
                    return Product;
                }
                else
                {
                    var Product = products.Where(x => x.IsShow == true).OrderByDescending(x => x.CreateDateEnglish).Skip(skipRecords).Take(recordsPerPage).ToList();
                    return Product;
                }
            }
            else
            {
                if (Id != null)
                {

                    var Product = db.Products.Where(x => x.GroupId == Id&&x.IsShow==true).OrderByDescending(x => x.CreateDateEnglish).Skip(skipRecords).Take(recordsPerPage).ToList();

                    Session["GroupId"] = Id;
                    ViewBag.Grouptitle = db.groupProducts.Where(x => x.Id == Id && x.GroupNotShow == true ).FirstOrDefault().GroupTitle;
                    return Product;
                }
                else
                {
                    var Product = db.Products.Where(x => x.IsShow == true ).OrderByDescending(x => x.CreateDateEnglish).Skip(skipRecords).Take(recordsPerPage).ToList();
                    return Product;
                }
            }
        
          
        }
    
        #endregion

        public ActionResult DetailProduct(int id)
        {
            if (id != 0)
            {

                if (Session["AllShopCart"] != null)
                {
                    List<AllShopCart> AllShopCart = Session["AllShopCart"] as List<AllShopCart>;
                    ViewBag.CountAll = AllShopCart.FirstOrDefault().CountAllShopCart;
                }

                ViewBag.FarbicId = new SelectList(db.FarbicTypes.Where(x => x.IsShow == true), "Id", "tiltle");
                if (db.OfferPrices.Count() != 0)
                {
                    ViewBag.offerprice = "NotNull";
                }
                var product = db.Products.Where(x => x.Id == id && x.IsShow == true).Include(x=>x.GroupProduct).FirstOrDefault();
                using (db)
                {
                    product.Seen += 1;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return View("DetailProductFormModel", product);

            }

            return View("DetailProductFormModel");
        }

     


        #endregion

        #region Major
        [Authorize(Roles = "User2,User3")]
        public ActionResult Majorshopping()
        {



            List<ShopCartItem> ShopCartItem = Session["shopcartMajor"] as List<ShopCartItem>;
            return View(ShopCartItem);


        }
        [Authorize(Roles = "User2,User3")]
        [HttpPost]
        public ActionResult Majorshopping(int? GroupId, int? ProductId, int? FarbicId, int? countcart, string selectedColor,string colorName)
        {
            #region Header Search

            ViewBag.GroupId = new SelectList(db.groupProducts, "Id", "GroupTitle");

            long sumproduct = 0;
            int farbicId = 0;
            Int64 farbicPrice = 0;

            if (GroupId == null)
            {
                ViewBag.showproduct = false;
                ViewBag.showfarbic = false;
                ViewBag.showCount = false;
            }
            else if (GroupId != null && ProductId == null)
            {
                ViewBag.GroupId = new SelectList(db.groupProducts, "Id", "GroupTitle");
                ViewBag.ProductId = new SelectList(db.Products.Where(x => x.GroupId == GroupId && x.IsShow == true).ToList(), "Id", "CodeKala");
                ViewBag.showproduct = true;
                ViewBag.showfarbic = false;
                ViewBag.showCount = false;
            }
            else if (GroupId != null && ProductId != null && FarbicId == null)
            {
                #region Find FarbicType

                List<FarbicTypeProducts> ftypeProductList = new List<FarbicTypeProducts>();
                var ftp = db.FarbicTypeProducts.Where(x => x.ProductId == ProductId).ToList();
                foreach (var item in ftp)
                {
                    ftypeProductList.Add(item);

                }
                List<FarbicType> FarbicTypetList = new List<FarbicType>();
                foreach (var item in ftypeProductList)
                {
                    FarbicTypetList.Add(db.FarbicTypes.Where(x => x.Id == item.FarbicTypeId).FirstOrDefault());

                }

                #endregion


                ViewBag.GroupId = new SelectList(db.groupProducts, "Id", "GroupTitle");
                ViewBag.ProductId = new SelectList(db.Products.Where(x => x.GroupId == GroupId && x.IsShow == true).ToList(), "Id", "CodeKala");
                ViewBag.FarbicId = new SelectList(FarbicTypetList, "Id", "tiltle");
                ViewBag.showCount = false;
                ViewBag.showproduct = true;
                ViewBag.showfarbic = true;
            }
            else
            {
                #region Find FarbicType

                List<FarbicTypeProducts> ftypeProductList = new List<FarbicTypeProducts>();
                var ftp = db.FarbicTypeProducts.Where(x => x.ProductId == ProductId).ToList();
                foreach (var item in ftp)
                {
                    ftypeProductList.Add(item);

                }
                List<FarbicType> FarbicTypetList = new List<FarbicType>();
                foreach (var item in ftypeProductList)
                {
                    FarbicTypetList.Add(db.FarbicTypes.Where(x => x.Id == item.FarbicTypeId && x.IsShow == true).FirstOrDefault());

                }

                #endregion

                ViewBag.GroupId = new SelectList(db.groupProducts, "Id", "GroupTitle");
                ViewBag.ProductId = new SelectList(db.Products.Where(x => x.GroupId == GroupId && x.IsShow == true).ToList(), "Id", "CodeKala");
                ViewBag.FarbicId = new SelectList(FarbicTypetList, "Id", "tiltle");
                ViewBag.showproduct = true;
                ViewBag.showfarbic = true;
                ViewBag.showCount = true;
                farbicId = Convert.ToInt32(FarbicId);
                farbicPrice = db.FarbicTypes.Where(x => x.Id == farbicId).FirstOrDefault().PriceMain;
                sumproduct = farbicPrice * Convert.ToInt32(countcart);
            }

            #endregion

            #region Add to shopcart


            if (GroupId != null && ProductId != null && FarbicId != null && countcart != null)
            {
                List<ShopCartItem> ShopCartItemViewModel = new List<ShopCartItem>();
                List<AllShopCart> AllShopCart = new List<AllShopCart>();

                if (Session["shopcartMajor"] != null)
                {
                    ShopCartItemViewModel = Session["shopcartMajor"] as List<ShopCartItem>;
                }

                int FarbicTypeId = Convert.ToInt32(FarbicId);
                int productId = Convert.ToInt32(ProductId);
                string codeKala = db.Products.Where(x => x.Id == productId && x.IsShow == true).FirstOrDefault().CodeKala;
                int FarbicTypeProductId = db.FarbicTypeProducts.Where(x => x.FarbicTypeId == FarbicTypeId && x.ProductId == productId).FirstOrDefault().Id;
                var shopcart = ShopCartItemViewModel.Where(x => x.FarbictypeId == FarbicTypeId && x.GroupId == Convert.ToInt32(GroupId) && x.ProductID == Convert.ToInt32(productId) && x.FarbicTypeProductId == FarbicTypeProductId).FirstOrDefault();
                if (ShopCartItemViewModel.Any(p => p.FarbictypeId == FarbicTypeId) && ShopCartItemViewModel.Any(p => p.GroupId == Convert.ToInt32(GroupId)) && ShopCartItemViewModel.Any(p => p.CodeKala == codeKala) && ShopCartItemViewModel.Any(p => p.FarbicTypeProductId == FarbicTypeProductId))
                {


                    #region Cheked success Input
                    var major1 = db.MajorShops.OrderByDescending(x => x.Createdate).FirstOrDefault();
                    if (major1.Count <= countcart)
                    {
                        shopcart.Count = Convert.ToInt32(countcart);
                    }
                    else
                    {
                        shopcart.Count = major1.Count;
                    }

                    #endregion
                    shopcart.Count += Convert.ToInt32(countcart);
                    shopcart.sumproduct += Convert.ToInt32(sumproduct);
                }
                else
                {
                    #region Cheked success Input
                    var major1 = db.MajorShops.OrderByDescending(x => x.Createdate).FirstOrDefault();
                    int Count = 0;
                    if (major1.Count <= countcart)
                    {
                        Count = Convert.ToInt32(countcart);
                    }
                    else
                    {
                        Count = major1.Count;
                    }

                    #endregion
                    int colorId = db.Colors.Where(x => x.color == colorName).FirstOrDefault().Id;
                    ShopCartItemViewModel.Add(new ShopCartItem()
                    {
                        ProductID = productId,
                        Count = Count,
                        sumproduct = Convert.ToInt32(sumproduct),
                        Farbictypeprice = Convert.ToInt32(farbicPrice),
                        FarbictypeId = FarbicTypeId,
                        Imgpath = db.Products.Where(x => x.Id == productId && x.IsShow == true).FirstOrDefault().ImgMain,
                        GroupId = Convert.ToInt32(GroupId),
                        CodeKala = codeKala,
                        FarbicTypeProductId = FarbicTypeProductId,
                        colorId = colorId
                    });
                }

                Session["shopcartMajor"] = ShopCartItemViewModel;

                #region Update all ShopCart in Session["shopcart"]


                if (Session["shopcartMajor"] != null)
                {
                    var major = db.MajorShops.OrderByDescending(x => x.Createdate).ToList();
                    List<ShopCartItem> shopcartList1 = Session["shopcartMajor"] as List<ShopCartItem>;
                    foreach (var itemmajor in major)
                    {

                        int MinSelectedProduct = (itemmajor.MinSelectedProduct);
                        if (shopcartList1.Count() >= MinSelectedProduct)
                        {
                            foreach (var itemshop in shopcartList1.ToList())
                            {
                                if (itemmajor.FarbicId == itemshop.FarbictypeId)
                                {
                                    int majorPrice = itemmajor.PriceMajor;

                                    itemshop.Farbictypeprice = majorPrice;
                                    itemshop.sumproduct = majorPrice * itemshop.Count;
                                }
                                //int currentPrice = Convert.ToInt32(db.FarbicTypes.Where(x => x.Id == itemshop.FarbictypeId).FirstOrDefault().PriceMain);


                            }
                            Session["shopcartMajor"] = shopcartList1;
                        }
                    }

                }

                #endregion


                return RedirectToAction(nameof(Majorshopping));
            }

            #endregion

            List<ShopCartItem> ShopCartItem = Session["shopcartMajor"] as List<ShopCartItem>;

            return RedirectToAction(nameof(Majorshopping));
        }

        [Authorize(Roles = "User2,User3")]
        public ActionResult RefreshMajor(string Farbictypeprice, string Count, string sumproduct, string CodeKala, string FarbictypeId, string FarbicTypeProductId, string GroupId, string productId, string ImagPath)
        {

            List<ShopCartItem> shopcartList = Session["shopcartMajor"] as List<ShopCartItem>;
            var _shopcart = shopcartList.Where(x => x.FarbicTypeProductId == Convert.ToInt32(FarbicTypeProductId)).FirstOrDefault();
            int firstCount = _shopcart.Count;
            int nextCount = int.Parse(Count);
            int endCount = Math.Abs(nextCount - firstCount);
            var major = db.MajorShops.OrderByDescending(x => x.Createdate).FirstOrDefault();
            int farbicid = Convert.ToInt32(_shopcart.FarbictypeId);
            int currentFarbictypeprice = Convert.ToInt32(db.FarbicTypes.Where(x => x.Id == farbicid).FirstOrDefault().PriceMain);

            #region  Update count in Session["shopcart"]
            foreach (var item in shopcartList)
            {
                if (item.FarbicTypeProductId == int.Parse(FarbicTypeProductId))
                {
                    #region Cheked success Input

                    if (major.Count <= int.Parse(Count))
                    {
                        item.Count = int.Parse(Count);
                    }
                    else
                    {
                        item.Count = major.Count;
                    }

                    #endregion
                    item.Count = int.Parse(Count);
                }
                Session["shopcartMajor"] = shopcartList;
            }
            #endregion


            #region Update all ShopCart in Session["shopcart"]


            if (Session["shopcartMajor"] != null)
            {
                var major1 = db.MajorShops.OrderByDescending(x => x.Createdate).ToList();
                List<ShopCartItem> shopcartList1 = Session["shopcartMajor"] as List<ShopCartItem>;
                foreach (var itemmajor in major1)
                {

                    int MinSelectedProduct = (itemmajor.MinSelectedProduct);
                    if (shopcartList1.Count() >= MinSelectedProduct)
                    {
                        foreach (var itemshop in shopcartList1.ToList())
                        {
                            if (itemmajor.FarbicId == itemshop.FarbictypeId)
                            {
                                int majorPrice = itemmajor.PriceMajor;

                                itemshop.Farbictypeprice = majorPrice;
                                itemshop.sumproduct = majorPrice * itemshop.Count;
                            }

                        }
                        Session["shopcartMajor"] = shopcartList1;
                    }
                    else
                    {
                        foreach (var itemshop in shopcartList1.ToList())
                        {
                            if (itemmajor.FarbicId == itemshop.FarbictypeId)
                            {
                                int CurrentPrice = Convert.ToInt32(db.FarbicTypes.Find(itemshop.FarbictypeId).PriceMain);

                                itemshop.Farbictypeprice = CurrentPrice;
                                itemshop.sumproduct = CurrentPrice * itemshop.Count;
                            }

                        }
                        Session["shopcartMajor"] = shopcartList1;
                    }
                }

            }

            #endregion

            return RedirectToAction("Majorshopping");

        }
        [Authorize(Roles = "User2,User3")]
        public ActionResult DeleteFromMajor(int id)
        {
            #region Delete From ShopCartItem

            List<ShopCartItem> shopcartList = Session["shopcartMajor"] as List<ShopCartItem>;
            int ProductId = shopcartList.Where(x => x.FarbicTypeProductId == id).FirstOrDefault().ProductID;
            var _shopcart = shopcartList.Where(x => x.FarbicTypeProductId == id).FirstOrDefault();




            var offerdetail = db.OfferPrices.OrderByDescending(x => x.Createdate).FirstOrDefault();
            int farbicid = Convert.ToInt32(_shopcart.FarbictypeId);
            int currentFarbictypeprice = Convert.ToInt32(db.FarbicTypes.Where(x => x.Id == farbicid).FirstOrDefault().PriceMain);
            int index = shopcartList.FindIndex(p => p.FarbicTypeProductId == id);
            shopcartList.RemoveAt(index);
            Session["shopcartMajor"] = shopcartList;


            #endregion
            if (shopcartList.Count() == 0)
            {
                Session.Remove("shopcartMajor");
            }
            else
            {
                #region Update all ShopCart in Session["shopcart"]


                if (Session["shopcartMajor"] != null)
                {
                    var major = db.MajorShops.OrderByDescending(x => x.Createdate).ToList();
                    List<ShopCartItem> shopcartList1 = Session["shopcartMajor"] as List<ShopCartItem>;
                    foreach (var itemmajor in major)
                    {

                        int MinSelectedProduct = (itemmajor.MinSelectedProduct);
                        if (shopcartList1.Count() >= MinSelectedProduct)
                        {
                            foreach (var itemshop in shopcartList1.ToList())
                            {
                                if (itemmajor.FarbicId == itemshop.FarbictypeId)
                                {
                                    int majorPrice = itemmajor.PriceMajor;

                                    itemshop.Farbictypeprice = majorPrice;
                                    itemshop.sumproduct = majorPrice * itemshop.Count;
                                }

                            }
                            Session["shopcartMajor"] = shopcartList1;
                        }
                        else
                        {
                            foreach (var itemshop in shopcartList1.ToList())
                            {
                                if (itemmajor.FarbicId == itemshop.FarbictypeId)
                                {
                                    int CurrentPrice = Convert.ToInt32(db.FarbicTypes.Find(itemshop.FarbictypeId).PriceMain);

                                    itemshop.Farbictypeprice = CurrentPrice;
                                    itemshop.sumproduct = CurrentPrice * itemshop.Count;
                                }

                            }
                            Session["shopcartMajor"] = shopcartList1;
                        }
                    }

                }

                #endregion
            }


            return RedirectToAction(nameof(Majorshopping));

        }


        #endregion

        #region Comment
        public ActionResult Comment(int Id)
        {
            Session["ProductId"] = Id;
            ViewBag.proid = Id;
            return PartialView();

        }
        [HttpPost]
        public ActionResult Comment(Comments comment)
        {
          

            if (ModelState.IsValid)
            {

                comment.ProductId = Convert.ToInt32(Session["ProductId"]);
                comment.Createdate = comment.Createdate + "_" + DateTime.Now.ToString("HH:mm:dd");
                db.Comments.Add(comment);
                db.SaveChanges();
                TempData["successmsgComment"] = "نظر شما با موفقیت ثبت گردید.";
                ViewBag.success = "نظر شما با موفقیت ثبت گردید.";
                return RedirectToAction(nameof(DetailProduct), new { id = Convert.ToInt32(Session["ProductId"]) });
            }
            TempData["errormsgComment"] = "لطفا موارد مورد نیاز را وارد کنید.";
            ViewBag.error = "لطفا موارد مورد نیاز را وارد کنید.";
            return RedirectToAction(nameof(DetailProduct), new { id = Convert.ToInt32(Session["ProductId"]) });


        }
        #endregion

        #region ShopCart
        public void AddToShopCart(int id, string Farbictypeprice, string countproduct, string sumproduct, string ImagPath, string GroupId, string CodeKala,string selectedColor)
        {

            List<ShopCartItem> ShopCartItemViewModel = new List<ShopCartItem>();
            List<AllShopCart> AllShopCart = new List<AllShopCart>();
            if (Session["AllShopCart"] != null)
            {
                AllShopCart = Session["AllShopCart"] as List<AllShopCart>;
            }
            if (Session["shopcart"] != null)
            {
                ShopCartItemViewModel = Session["shopcart"] as List<ShopCartItem>;

            }
            int FarbictypePrice = Convert.ToInt32(Farbictypeprice);
            int FarbicTypeId = db.FarbicTypes.Where(x => x.PriceMain == FarbictypePrice).FirstOrDefault().Id;
            int FarbicTypeProductId = db.FarbicTypeProducts.Where(x => x.FarbicTypeId == FarbicTypeId && x.ProductId == id).FirstOrDefault().Id;
            if (
                ShopCartItemViewModel.Any(p => p.FarbictypeId == FarbicTypeId) && 
                ShopCartItemViewModel.Any(p => p.GroupId == Convert.ToInt32(GroupId)) && 
                ShopCartItemViewModel.Any(p => p.CodeKala == CodeKala) &&
                ShopCartItemViewModel.Any(p => p.FarbicTypeProductId == FarbicTypeProductId) &&
                ShopCartItemViewModel.Any(p => p.colorId == Convert.ToInt32(selectedColor))
                )
            {
                var shopcart = ShopCartItemViewModel.Where(x => x.FarbictypeId == FarbicTypeId && x.GroupId == Convert.ToInt32(GroupId) && x.ProductID == Convert.ToInt32(id) && x.FarbicTypeProductId == FarbicTypeProductId).FirstOrDefault();
                shopcart.Count += Convert.ToInt32(countproduct);
                shopcart.sumproduct += Convert.ToInt32(sumproduct);
            }
            else
            {
                ShopCartItemViewModel.Add(new ShopCartItem()
                {
                    ProductID = id,
                    Count = Convert.ToInt32(countproduct),
                    sumproduct = Convert.ToInt32(sumproduct),
                    Farbictypeprice = Convert.ToInt32(Farbictypeprice),
                    FarbictypeId = FarbicTypeId,
                    Imgpath = ImagPath,
                    GroupId = Convert.ToInt32(GroupId),
                    CodeKala = CodeKala,
                    FarbicTypeProductId = FarbicTypeProductId,
                    colorId=int.Parse(selectedColor)
                });

            }
            if (Session["AllShopCart"] == null)
            {
                AllShopCart.Add(new AllShopCart()
                {
                    sumAllPrices = Convert.ToInt32(sumproduct),
                    CountAllShopCart = Convert.ToInt32(countproduct)
                });
            }
            else
            {
                AllShopCart.FirstOrDefault().CountAllShopCart += Convert.ToInt32(countproduct);
                AllShopCart.FirstOrDefault().sumAllPrices += Convert.ToInt32(sumproduct);
            }


            Session["shopcart"] = ShopCartItemViewModel;
            Session["AllShopCart"] = AllShopCart;





        }
      
        public ActionResult AddShopCart(int id,int proid, int FarbictypeId, int countproduct,  string ImagPath, string GroupId, string CodeKala)
        {

            List<ShopCartItem> ShopCartItemViewModel = new List<ShopCartItem>();
            List<AllShopCart> AllShopCart = new List<AllShopCart>();
            if (Session["AllShopCart"] != null)
            {
                AllShopCart = Session["AllShopCart"] as List<AllShopCart>;
            }
            if (Session["shopcart"] != null)
            {
                ShopCartItemViewModel = Session["shopcart"] as List<ShopCartItem>;

            }
            int FarbicTypeProductId = db.FarbicTypeProducts.Where(x => x.FarbicTypeId == FarbictypeId && x.ProductId == id).FirstOrDefault().Id;
            long FarbictypePrice = db.FarbicTypes.Where(x=>x.Id==FarbictypeId).FirstOrDefault().PriceMain;

            if (
                     ShopCartItemViewModel.Any(p => p.FarbictypeId == FarbictypeId) &&
                     ShopCartItemViewModel.Any(p => p.GroupId == Convert.ToInt32(GroupId)) &&
                     ShopCartItemViewModel.Any(p => p.CodeKala == CodeKala) &&
                     ShopCartItemViewModel.Any(p => p.FarbicTypeProductId == FarbicTypeProductId)
             )
            
            {
                var shopcart = ShopCartItemViewModel.Where(x => x.FarbictypeId == FarbictypeId && x.GroupId == Convert.ToInt32(GroupId) && x.ProductID == Convert.ToInt32(id) && x.FarbicTypeProductId == FarbicTypeProductId).FirstOrDefault();
                shopcart.Count += countproduct;
                shopcart.sumproduct += countproduct*(int) FarbictypePrice;
            }
            else
            {
                ShopCartItemViewModel.Add(new ShopCartItem()
                {
                    ProductID = id,
                    Count = Convert.ToInt32(countproduct),
                    sumproduct = Convert.ToInt32(countproduct * (int)FarbictypePrice),
                    Farbictypeprice = Convert.ToInt32(FarbictypePrice),
                    FarbictypeId = FarbictypeId,
                    Imgpath = ImagPath,
                    GroupId = Convert.ToInt32(GroupId),
                    CodeKala = CodeKala,
                    FarbicTypeProductId = FarbicTypeProductId,
                    colorId = 0
                });

            }



            if (Session["AllShopCart"] == null)
            {
                AllShopCart.Add(new AllShopCart()
                {
                    sumAllPrices = Convert.ToInt32(countproduct * (int)FarbictypePrice),
                    CountAllShopCart = Convert.ToInt32(countproduct)
                });
            }
            else
            {
                AllShopCart.FirstOrDefault().CountAllShopCart += Convert.ToInt32(countproduct);
                AllShopCart.FirstOrDefault().sumAllPrices += Convert.ToInt32(countproduct * (int)FarbictypePrice);
            }


            Session["shopcart"] = ShopCartItemViewModel;
            Session["AllShopCart"] = AllShopCart;

            return RedirectToAction("DetailProduct", new { id = proid });


        }



        public ActionResult DeleteFromShopCart(int id)
        {
            #region Delete From ShopCartItem

            List<ShopCartItem> shopcartList = Session["shopcart"] as List<ShopCartItem>;
            int ProductId = shopcartList.Where(x => x.FarbicTypeProductId == id).FirstOrDefault().ProductID;
            var _shopcart = shopcartList.Where(x => x.FarbicTypeProductId == id).FirstOrDefault();

            #region Delete From AllShopCart

            List<AllShopCart> AllShopCartList = Session["AllShopCart"] as List<AllShopCart>;
            var allshopcart = AllShopCartList.FirstOrDefault();

            allshopcart.CountAllShopCart -= _shopcart.Count;
            allshopcart.sumAllPrices -= _shopcart.sumproduct;
            Session["AllShopCart"] = AllShopCartList;

            #endregion


            var offerdetail = db.OfferPrices.OrderByDescending(x => x.Createdate).FirstOrDefault();
            int farbicid = Convert.ToInt32(_shopcart.FarbictypeId);
            int currentFarbictypeprice = Convert.ToInt32(db.FarbicTypes.Where(x => x.Id == farbicid).FirstOrDefault().PriceMain);
            int index = shopcartList.FindIndex(p => p.FarbicTypeProductId == id);
            shopcartList.RemoveAt(index);
            Session["shopcart"] = shopcartList;

            #endregion
            if (shopcartList.Count() == 0)
            {
                Session.Remove("shopcart");
            }
            else
            {
                //متد تغغیر در سشن shopcart
                UpdateShopCart();
            }


            return RedirectToAction("ShopCart");

        }
        public ActionResult DeleteFromMenuShopCart(int id)
        {
            #region Delete From ShopCartItem

            List<ShopCartItem> shopcartList = Session["shopcart"] as List<ShopCartItem>;
            int ProductId = shopcartList.Where(x => x.FarbicTypeProductId == id).FirstOrDefault().ProductID;
            var _shopcart = shopcartList.Where(x => x.FarbicTypeProductId == id).FirstOrDefault();

            #region Delete From AllShopCart

            List<AllShopCart> AllShopCartList = Session["AllShopCart"] as List<AllShopCart>;
            var allshopcart = AllShopCartList.FirstOrDefault();

            allshopcart.CountAllShopCart -= _shopcart.Count;
            allshopcart.sumAllPrices -= _shopcart.sumproduct;
            Session["AllShopCart"] = AllShopCartList;
            if (AllShopCartList.Count() == 0)
            {
                Session.Remove("AllShopCart");
            }
            #endregion


            var offerdetail = db.OfferPrices.OrderByDescending(x => x.Createdate).FirstOrDefault();
            int farbicid = Convert.ToInt32(_shopcart.FarbictypeId);
            int currentFarbictypeprice = Convert.ToInt32(db.FarbicTypes.Where(x => x.Id == farbicid).FirstOrDefault().PriceMain);
            int index = shopcartList.FindIndex(p => p.FarbicTypeProductId == id);
            shopcartList.RemoveAt(index);
            Session["shopcart"] = shopcartList;

            #endregion
            if (shopcartList.Count() == 0)
            {
                Session.Remove("shopcart");
            }
            else
            {
                //متد تغغیر در سشن shopcart
                UpdateShopCart();

            }


            return RedirectToAction("Index", "Home");

        }
        public ActionResult ShopCart()
        {

            if (Session["shopcart"] == null)
            {
                return Redirect("~/Home/Index");
            }
            else
            {
                Session.Remove("SumAll");
                //متد تغغیر در سشن shopcart
                UpdateShopCart();
                List<ShopCartItem> ShopCartItem = Session["shopcart"] as List<ShopCartItem>;
                ViewBag.typeSendpost = new SelectList(db.TypeSendPosts, "Id", "NameHaml");
                return View(ShopCartItem);
            }

        }

        public ActionResult RefreshShopCart(string Farbictypeprice, string Count, string sumproduct, string CodeKala, string FarbictypeId, string FarbicTypeProductId, string GroupId, string productId, string ImagPath)
        {


            List<ShopCartItem> shopcartList = Session["shopcart"] as List<ShopCartItem>;
            //سطری از سشن جاری
            var _shopcart = shopcartList.Where(x => x.FarbicTypeProductId == Convert.ToInt32(FarbicTypeProductId)).FirstOrDefault();

            //تعداد فعلی  
            int firstCount = _shopcart.Count;

            //تعداد جدید
            int nextCount = int.Parse(Count);

            //ماباتفاوت تعداد
            int endCount = Math.Abs(nextCount - firstCount);

            //تغییرتعداد در سشن کل
            List<AllShopCart> AllShopCartList = Session["AllShopCart"] as List<AllShopCart>;
            var allshopcart = AllShopCartList.FirstOrDefault();
            if (nextCount - firstCount >= 1)
            {
                allshopcart.CountAllShopCart += endCount;
                Session["AllShopCart"] = AllShopCartList;
            }
            else
            {
                allshopcart.CountAllShopCart -= endCount;
                Session["AllShopCart"] = AllShopCartList;
            }

            //اعمال تغییر تعداد در سشن
            _shopcart.Count = int.Parse(Count);

           
            Session["shopcart"] = shopcartList;
            //متد تغغیر در سشن shopcart
            UpdateShopCart();

            return RedirectToAction("ShopCart");

        }
        public void UpdateShopCart()
        {
            #region Update all ShopCart in Session["shopcart"]
            var offerdetail = db.OfferPrices.OrderByDescending(x => x.Createdate).FirstOrDefault();
            List<ShopCartItem> shopcartList1 = Session["shopcart"] as List<ShopCartItem>;



            //ازاین تعداد به بعد شامل تخفیف شود
            int offerNumber = (offerdetail.AsNumber - 1);

            foreach (var item in shopcartList1)
            {
                //قیمت واقعی
                int currentPrice = Convert.ToInt32(db.FarbicTypes.Where(x => x.Id == item.FarbictypeId).FirstOrDefault().PriceMain);

                //قیمت تخفیفی
                int OfferfarbicPrice = item.Farbictypeprice = offerdetail.Price;

                if (offerNumber != 0)
                {
                    if (item.Count >= offerNumber)
                    {
                        item.sumproduct = ((offerNumber * currentPrice) + (item.Count - (offerNumber)) * (currentPrice - OfferfarbicPrice));
                        offerNumber = 0;
                    }
                    else
                    {
                        item.sumproduct = item.Count * currentPrice;
                        offerNumber -= item.Count;
                    }
                }
                else
                {
                    item.sumproduct = item.Count * (currentPrice - OfferfarbicPrice);
                }


                List<ShopCartItem> shopcartList2 = Session["shopcart"] as List<ShopCartItem>;
                var shop = shopcartList2.Where(x => x.FarbicTypeProductId == item.FarbicTypeProductId).FirstOrDefault();
                shop.Count = item.Count;
                shop.sumproduct = item.sumproduct;
                Session["shopcart"] = shopcartList2;

            }


            #endregion
        }
        #endregion

        #region AddCompare
        public int AddToCompare(int id)
        {
            List<AddCompare> AddToCompare = new List<AddCompare>();

            if (Session["Copmare"] != null)
            {
                AddToCompare = Session["Copmare"] as List<AddCompare>;

            }

            if (AddToCompare.Any(p => p.ProductID == id))
            {

            }
            else
            {
                AddToCompare.Add(new AddCompare()
                {
                    ProductID = id,

                });
            }

            Session["Copmare"] = AddToCompare;

            return AddToCompare.Count();

        }
        public ActionResult ShowCompare()
        {
            List<Compare> compare = new List<Compare>();

            if (Session["Copmare"] != null)
            {
                List<AddCompare> comparecart = Session["Copmare"] as List<AddCompare>;

                foreach (var item in comparecart)
                {
                    var product = db.Products.Find(item.ProductID);
                    compare.Add(new Salino.ViewModels.Compare
                    {
                        Product = product
                    });

                }
                Session["showCompare"] = compare;
            }
            List<Compare> showCompare = Session["showCompare"] as List<Compare>;
            return View(showCompare.ToList());
        }
        #endregion

        #region GifCart

        public int GifCart(string gifcartinput, int SumAllPrices)
        {

            var resultNoDate = db.giftcards.Where(x => x.Code == gifcartinput).FirstOrDefault();
            var resultWithDate = db.gifcardDates.Where(x => x.Code == gifcartinput && x.ExpirationDate >= DateTime.Now).FirstOrDefault();

            if (resultNoDate != null || resultWithDate != null)
            {
                if (resultNoDate != null)
                {

                    int GifSumAll = Convert.ToInt32(SumAllPrices - resultNoDate.Amount);
                    ViewBag.ChekUsedAsGifCart = "YeswChekUsedAsGifCart";
                    return GifSumAll;
                }
                else if (resultWithDate != null && Convert.ToInt32(Session["flagCheckUseCode"]) == 0)
                {

                    int GifSumAll = Convert.ToInt32(SumAllPrices - resultWithDate.Amount);
                    return GifSumAll;
                }
                else
                {
                    return 0;
                }


            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region SendType
        public int SendType(int sendTypeId, int SumAllPrices, int countProduct)
        {

            var result = db.TypeSendPosts.Where(x => x.Id == sendTypeId).FirstOrDefault();
            var sendpricefree = db.SendPrices.Where(x => x.AsNumber != 0).FirstOrDefault();
            if (result != null)
            {
                if (sendpricefree != null)
                {
                    if (sendpricefree.AsNumber <= countProduct)
                    {
                        int PriceHaml = 0;
                        int SumAll = Convert.ToInt32(SumAllPrices + PriceHaml);
                        return SumAll;

                    }
                }

                if (result != null)
                {
                    int PriceHaml = int.Parse(result.PriceHaml);
                    int SumAll = Convert.ToInt32(SumAllPrices + PriceHaml);
                    return SumAll;

                }
                else
                {
                    return 0;
                }
            }
            else
            {
                if (result != null)
                {
                    int PriceHaml = int.Parse(result.PriceHaml);
                    int SumAll = Convert.ToInt32(SumAllPrices + PriceHaml);
                    return SumAll;

                }
                else
                {
                    return 0;
                }
            }

        }
        public int SendTypeFoeMajor(int sendTypeId, int SumAllPrices, int countProduct)
        {

            var result = db.SendForMajors.Where(x => x.Id == sendTypeId).FirstOrDefault();

            if (result != null)
            {

                int PriceHaml =int.Parse( result.PriceHaml);
                int SumAll = Convert.ToInt32(SumAllPrices + PriceHaml);
                return SumAll;

            }
            else
            {
                return 0;

            }

        }
        #endregion



    }


}