using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using Salino.DataLayer;
using Salino.Models;

namespace SalinoMvc5.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin1")]
    [Area("Admin")]
    [Authorize(Roles = "Admin1")]
    public class ProductsController : SalinoController
    {
        private SalinoContext db = new SalinoContext();

   
        public ActionResult Index(string search, int? page= 1)
        {

            if (!string.IsNullOrEmpty(search))
            {
                Session["pagecurrent"] = page;
                if (page==1)   ViewBag.counter = 1;
                else          ViewBag.counter = ((page-1) * 14)+1;

                var products = db.Products.Where(x => x.Name.Contains(search) || x.GroupProduct.GroupTitle.Contains(search) || x.CreateDate.Contains(search) || x.CreateTime.Contains(search) || x.Color.color.Contains(search) || x.CodeKala.Contains(search)).Include(p => p.GroupProduct).OrderByDescending(x => x.CreateDateEnglish).ToList();
                return View(products.ToPagedList((int)page, 14));
            

            }
            
            else
            {
                var page1 = page == 0 ? 1 : page;
                Session["pagecurrent"] = page1;
                if (page == 1) ViewBag.counter = 1;
                else ViewBag.counter = ((page1 - 1) * 14) + 1;
                var products = db.Products.Include(p => p.GroupProduct).OrderByDescending(x=>x.CreateDateEnglish).ToList();

                return View(products.ToPagedList((int)page1, 14));
            }


        }
        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            var colors = db.ColorsProducts.Where(x => x.ProductId == id).ToList();
            List<string> colorsNames = new List<string>();
            foreach (var item in colors)
            {
                string colorName = db.Colors.Find(item.ColorId).color;
                colorsNames.Add(colorName);
            }
            ViewBag.colors = colorsNames;
            int currentPage = Convert.ToInt32(Session["pagecurrent"]);
            ViewBag.currentPage = currentPage;
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            int currentPage = Convert.ToInt32(Session["pagecurrent"]);
            ViewBag.currentPage = currentPage;
            ViewBag.GroupId = new SelectList(db.groupProducts, "Id", "GroupTitle");
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "color");
            ViewBag.SelectColorId = db.Colors.ToList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Product product, HttpPostedFileBase filemain, HttpPostedFileBase filedetail, HttpPostedFileBase filemob, HttpPostedFileBase filedetailmob)
        {
            if (ModelState.IsValid)
            {
                if (filemain != null)
                {
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = product.Name;
                    WebImage img = new WebImage(filemain.OpenReadStream());
                    string imgsrc3 = "Product" + "-" + imgname + "-" + imgcode.ToString() + "-" + filemain.FileName;

                    img.Save("~/Content/imgpro/" + imgsrc3);
                    product.ImgMain = imgsrc3;
                }
                if (filedetail != null)
                {
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = product.Name;
                    WebImage img = new WebImage(filedetail.OpenReadStream());
                    string imgsrc3 = "Product" + "-" + imgname + "-" + imgcode.ToString() + "-" + filedetail.FileName;

                    img.Save("~/Content/imgpro/" + imgsrc3);
                    product.Imgdetail = imgsrc3;
                }
                if (filemob != null)
                {
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = product.Name;
                    WebImage img = new WebImage(filemob.OpenReadStream());
                    string imgsrc3 = "Product" + "-" + imgname + "-" + imgcode.ToString() + "-" + filemob.FileName;

                    img.Save("~/Content/imgpro/" + imgsrc3);
                    product.ImgMainMob = imgsrc3;
                }
                if (filedetailmob != null)
                {
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = product.Name;
                    WebImage img = new WebImage(filedetailmob.OpenReadStream());
                    string imgsrc3 = "Product" + "-" + imgname + "-" + imgcode.ToString() + "-" + filedetailmob.FileName;

                    img.Save("~/Content/imgpro/" + imgsrc3);
                    product.ImgdetailMob = imgsrc3;
                }

                db.Products.Add(product);
                db.SaveChanges();

                string[] colors = Session["colorsSelected"] as string[];
                foreach (string item in colors)
                {
                    int itemColorId = Convert.ToInt32(item);
                    var color = db.Colors.Find(itemColorId);
                    db.ColorsProducts.Add(new ColorsProduct()
                    {
                        ColorId = itemColorId,
                        ProductId = product.Id

                    });
                    db.SaveChanges();

                }
               


                #region Add To FarbictypeProduct

            
                foreach (var item in db.FarbicTypes.ToList())
                {
                    List<FarbicTypeProducts> FarbicTypeproducts = new List<Salino.Models.FarbicTypeProducts>()
                  {
                    new FarbicTypeProducts()
                    {
                        FarbicTypeId = item.Id,
                        ProductId = product.Id
                    }

                  };
                    foreach (var item1 in FarbicTypeproducts)
                    {
                        FarbicTypeProducts model = new FarbicTypeProducts();
                        model.ProductId = item1.ProductId;
                        model.FarbicTypeId = item1.FarbicTypeId;
                        db.FarbicTypeProducts.Add(model);
                        db.SaveChanges();
                    }
                }
                #endregion
                int currentPage = Convert.ToInt32(Session["pagecurrent"]);
                return RedirectToAction("Index",new { page= currentPage });
            }
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "color", product.ColorId);
            ViewBag.GroupId = new SelectList(db.groupProducts, "Id", "GroupTitle", product.GroupId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            int currentPage = Convert.ToInt32(Session["pagecurrent"]);
            ViewBag.currentPage = currentPage;


            var colorsProduct = db.ColorsProducts.Where(x => x.ProductId == product.Id).ToList();
            List<Salino.Models.Color> listColors = new List<Salino.Models.Color>();
            listColors = db.Colors.ToList();
            List<Salino.Models.Color> listColorschecked = new List<Salino.Models.Color>();
            List<Salino.Models.Color> listColorsNoChecked = new List<Salino.Models.Color>();

            foreach (var item1 in db.Colors.ToList())
            {
                foreach (var item2 in colorsProduct)
                {
                    if (item1.Id == item2.ColorId)
                    {

                        listColorschecked.Add(item1);
                        listColors.Remove(item1);
                    }
                }
           
            }
            listColorsNoChecked = listColors;
            ViewBag.listColorschecked = listColorschecked;
                ViewBag.listColorsNoChecked = listColorsNoChecked;
          
                if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "color", product.ColorId);
            ViewBag.GroupId = new SelectList(db.groupProducts, "Id", "GroupTitle", product.GroupId);
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Product product, HttpPostedFileBase filemain, HttpPostedFileBase filedetail, HttpPostedFileBase filemob, HttpPostedFileBase filedetailmob)
        {
            if (ModelState.IsValid)
            {
                if (filemain != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/imgpro/" + product.ImgMain)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/imgpro/" + product.ImgMain));
                    }
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = product.Name;
                    WebImage img = new WebImage(filemain.OpenReadStream());
                    string imgsrc3 = "Product" + "-" + imgname + "-" + imgcode.ToString() + "-" + filemain.FileName;

                    img.Save("~/Content/imgpro/" + imgsrc3);
                    product.ImgMain = imgsrc3;
                }
                if (filedetail != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/imgpro/" + product.Imgdetail)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/imgpro/" + product.Imgdetail));
                    }
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = product.Name;
                    WebImage img = new WebImage(filedetail.OpenReadStream());
                    string imgsrc3 = "Product" + "-" + imgname + "-" + imgcode.ToString() + "-" + filedetail.FileName;

                    img.Save("~/Content/imgpro/" + imgsrc3);
                    product.Imgdetail = imgsrc3;
                }
                if (filemob != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/imgpro/" + product.ImgMainMob)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/imgpro/" + product.ImgMainMob));
                    }
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = product.Name;
                    WebImage img = new WebImage(filemob.OpenReadStream());
                    string imgsrc3 = "Product" + "-" + imgname + "-" + imgcode.ToString() + "-" + filemob.FileName;

                    img.Save("~/Content/imgpro/" + imgsrc3);
                    product.ImgMainMob = imgsrc3;
                }
                if (filedetailmob != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/imgpro/" + product.ImgdetailMob)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/imgpro/" + product.ImgdetailMob));
                    }
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999000).ToString();
                    string imgname = product.Name;
                    WebImage img = new WebImage(filedetailmob.OpenReadStream());
                    string imgsrc3 = "Product" + "-" + imgname + "-" + imgcode.ToString() + "-" + filedetailmob.FileName;

                    img.Save("~/Content/imgpro/" + imgsrc3);
                    product.ImgdetailMob = imgsrc3;
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
               if(Session["colorsSelected"]!=null)
                {             
                    string[] colors = Session["colorsSelected"] as string[];
                    foreach (string item in colors)
                    {
                        int itemColorId = Convert.ToInt32(item);
                        var color = db.Colors.Find(itemColorId);
                        var IscolorProducts = db.ColorsProducts.Where(x => x.ProductId == product.Id && x.ColorId == itemColorId).FirstOrDefault();
                        if (IscolorProducts == null)
                        {
                            db.ColorsProducts.Add(new ColorsProduct()
                            {
                                ColorId = itemColorId,
                                ProductId = product.Id

                            });
                            db.SaveChanges();
                        }
                     
                    }
                }
               if(Session["DeleteColorProduct"]!=null)
                {
                    string[] colors = Session["DeleteColorProduct"] as string[];
                    foreach (string item in colors)
                    {
                        int itemColorId = Convert.ToInt32(item);
                        var color = db.Colors.Find(itemColorId);
                        var IscolorProducts = db.ColorsProducts.Where(x => x.ProductId == product.Id && x.ColorId == itemColorId).FirstOrDefault();
                        if (IscolorProducts != null)
                        {
                           
                            db.ColorsProducts.Remove(IscolorProducts);
                            db.SaveChanges();
                        }

                    }
                }
                int currentPage = Convert.ToInt32(Session["pagecurrent"]);
                return RedirectToAction("Index", new { page = currentPage });
            }
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "color", product.ColorId);
            ViewBag.GroupId = new SelectList(db.groupProducts, "Id", "GroupTitle", product.GroupId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            int currentPage = Convert.ToInt32(Session["pagecurrent"]);
            ViewBag.currentPage = currentPage;
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            if (System.IO.File.Exists(Server.MapPath("~/Content/imgpro/" + product.ImgMain)))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/imgpro/" + product.ImgMain));
            }
            if (System.IO.File.Exists(Server.MapPath("~/Content/imgpro/" + product.Imgdetail)))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/imgpro/" + product.Imgdetail));
            }
            if (System.IO.File.Exists(Server.MapPath("~/Content/imgpro/" + product.ImgMainMob)))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/imgpro/" + product.ImgMainMob));
            }

            #region Delete As FarbictypeProduct


            foreach (var item in db.FarbicTypeProducts.Where(x=>x.ProductId== id).ToList())
            {

                db.FarbicTypeProducts.Remove(item);
                db.SaveChanges();

            }
            foreach (var item in db.ColorsProducts.Where(x => x.ProductId == id).ToList())
            {

                db.ColorsProducts.Remove(item);
                db.SaveChanges();

            }
            #endregion
            db.Products.Remove(product);
            db.SaveChanges();
            int currentPage = Convert.ToInt32(Session["pagecurrent"]);
            return RedirectToAction("Index", new { page = currentPage });
        }

        #region AddToMonasebats
        public int AddToMonasebat(string[] checkedNames,int? id)
        {
            foreach (string item in checkedNames)
            {
                int itemProductId = Convert.ToInt32(item);
                var IsMonasebatPro = db.MonasebatProducts.Where(x => x.ProductId == itemProductId && x.MonasebatId == id.Value).ToList();
                if (IsMonasebatPro.Count() == 0)
                {
                    db.MonasebatProducts.Add(new MonasebatProducts
                    {
                        MonasebatId = id.Value,
                        ProductId = itemProductId
                    });
                    db.SaveChanges();
                }             

            }
            return 1;
        }
        #endregion

        #region AddColorsProduct

        public int AddColorsProduct(string[] colorsSelected)
        {

            Session["colorsSelected"] = colorsSelected;                            
            return 1;
        }

        #endregion

        #region DeleteColorProduct

        public int DeleteColorProduct(string[] colorsSelected)
        {

            Session["DeleteColorProduct"] = colorsSelected;
            return 1;
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
