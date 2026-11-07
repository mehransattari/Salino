using Salino.Convertor;
using Salino.Models;
using Salino.ToShamsi;

namespace Salino.DataLayer.DemoData
{
    /// <summary>
    /// Fills an empty database with demo data for local development:
    ///   dotnet run -- --seed-demo
    /// Images live in wwwroot/Content/* and are named demo-*. Runs only when there are no products yet.
    /// </summary>
    public static class DemoDataSeeder
    {
        public static void Run()
        {
            using var db = new SalinoContext();
            if (db.Products.Any())
            {
                Console.WriteLine("Products already exist; demo data was not added.");
                return;
            }

            using var tx = db.Database.BeginTransaction();
            var now = DateTime.Now;
            string Shamsi(DateTime d) => d.ToShortDateString().ToShamsi();

            // ---------------- settings / shop info ----------------
            db.Settings.Add(new Setting
            {
                DecAboutMe = "<p>فروشگاه <strong>سالینو</strong> از سال ۱۳۹۵ در تبریز فعالیت خود را در زمینه طراحی و تولید شال، روسری و کوسن با چاپ دیجیتال آغاز کرد.</p>"
                           + "<p>تمام محصولات سالینو با پارچه‌های درجه یک (نخی، ابریشم، نخ‌ابریشم و موهر) و با طرح‌های اختصاصی تولید می‌شوند و با ضمانت کیفیت و امکان مرجوعی ۷ روزه به سراسر ایران ارسال می‌گردند.</p>",
                DecContactUs = "<p>برای ثبت سفارش عمده، پیگیری سفارش یا هرگونه سوال، از ساعت ۹ تا ۲۱ با ما در تماس باشید.</p><p>پاسخگویی در واتس‌اپ و تلگرام نیز انجام می‌شود.</p>",
                Questions = "<h4>زمان ارسال سفارش چقدر است؟</h4><p>سفارش‌ها ۳ تا ۵ روز کاری پس از پرداخت ارسال می‌شوند.</p>"
                          + "<h4>آیا امکان مرجوع کردن کالا وجود دارد؟</h4><p>بله، تا ۷ روز پس از تحویل در صورت عدم استفاده و سالم بودن بسته‌بندی.</p>"
                          + "<h4>خرید عمده چگونه است؟</h4><p>پس از ثبت‌نام به عنوان همکار، از بخش «خرید عمده» حداقل ۶ طرح انتخاب کنید تا قیمت عمده اعمال شود.</p>",
                Address = "تبریز، خیابان ولیعصر، پاساژ ستاره، طبقه اول، واحد ۱۲",
                Tell = "041-33333333",
                Mobile = "0914-614-3251",
                Email = "info@salinotbz.ir",
                imageSrc = "Group-setting-263669-Layer4.png",
                imageSrcMain = "Group-setting-987197-Layer6.png",
            });

            db.Socials.AddRange(
                new Social { SocialName = "اینستاگرام", SocialIcon = "imgsocial--191537-insta-135559-instagram-logo-png_95 (1).png", SocialLink = "https://instagram.com/salinotbz", SocialOrder = 1 },
                new Social { SocialName = "تلگرام", SocialIcon = "imgsocial--442971-TLEGRAM-544471-1200px-Telegram_logo.svg.png", SocialLink = "https://t.me/salinotbz", SocialOrder = 2 },
                new Social { SocialName = "سروش", SocialIcon = "imgsocial--659227-sorosh-777461-soroush-logo.png", SocialLink = "https://sapp.ir/salinotbz", SocialOrder = 3 });

            db.Mojavezs.Add(new Mojavez
            {
                ListKhadamat = "<ul><li>ارسال به سراسر کشور با پست پیشتاز و تیپاکس</li><li>ضمانت بازگشت کالا تا ۷ روز</li><li>پرداخت امن از طریق درگاه بانکی</li><li>فروش عمده به همکاران</li></ul>",
                Img1 = "mojavez-mojavez-173227-Layer11.png",
                Img2 = "mojavez-mojavez-379217-Layer13.png",
                Img3 = "mojavez-mojavez-447153-Layer15.png",
            });

            // ---------------- prices, shipping, offers (update the rows created by the initial seed) ----------------
            var nakhi = db.FarbicTypes.Find(1);
            nakhi.tiltle = "نخی"; nakhi.PriceMain = 185000; nakhi.IsShow = true;
            var abrisham = new FarbicType { tiltle = "ابریشم", PriceMain = 345000, IsShow = true };
            var nakhAbrisham = new FarbicType { tiltle = "نخ‌ابریشم", PriceMain = 265000, IsShow = true };
            var mohair = new FarbicType { tiltle = "موهر", PriceMain = 225000, IsShow = true };
            var makhmal = new FarbicType { tiltle = "مخمل (کوسن)", PriceMain = 160000, IsShow = true };
            db.FarbicTypes.AddRange(abrisham, nakhAbrisham, mohair, makhmal);

            var post = db.TypeSendPosts.Find(1);
            post.NameHaml = "پست پیشتاز"; post.PriceHaml = "35000";
            db.TypeSendPosts.AddRange(
                new TypeSendPost { NameHaml = "تیپاکس", PriceHaml = "55000" },
                new TypeSendPost { NameHaml = "پیک (فقط تبریز)", PriceHaml = "25000" });

            var majorPost = db.SendForMajors.Find(1);
            majorPost.NameHaml = "باربری"; majorPost.PriceHaml = "90000";
            db.SendForMajors.Add(new SendForMajor { NameHaml = "تیپاکس عمده", PriceHaml = "120000" });

            db.SendPrices.Find(1).AsNumber = 4;                                   // free shipping from the 4th item
            db.OfferFirstShops.Find(1).PriceDiscount = 15000;                     // first-purchase discount
            var loyal = db.OfferNumberShops.Find(1); loyal.ToNumber = 5; loyal.PriceDiscount = 25000; // every 5th order
            db.OfferPrices.Add(new OfferPrice { Price = 10000, AsNumber = 3, Createdate = now });     // discount from the 3rd item
            db.MajorShops.Add(new MajorShop { MinSelectedProduct = 6, Count = 3, FarbicId = 1, PriceMajor = 150000, Createdate = now });
            db.MajorShops.Add(new MajorShop { MinSelectedProduct = 6, Count = 3, PriceMajor = 290000, Createdate = now, FarbicType = abrisham });

            db.giftcards.AddRange(
                new giftcard { Code = "1404", Amount = 20000, NumberUse = 50 },
                new giftcard { Code = "7777", Amount = 35000, NumberUse = 10 });
            db.gifcardDates.Add(new gifcardDate
            {
                Code = "YALDA", Amount = 30000, NumberUse = 100,
                ExpirationDate = now.AddDays(30), CreateDateShamsi = Shamsi(now), ExpirationDateShamsi = Shamsi(now.AddDays(30)),
            });

            // ---------------- catalogue ----------------
            var rusari = new GroupProduct { GroupTitle = "روسری", imagepath = "demo-group-rusari.jpg" };
            var shal = new GroupProduct { GroupTitle = "شال", imagepath = "demo-group-shal.jpg" };
            var scarf = new GroupProduct { GroupTitle = "مینی اسکارف", imagepath = "demo-group-scarf.jpg" };
            var kousan = new GroupProduct { GroupTitle = "کوسن", imagepath = "demo-group-kousan.jpg" };
            db.groupProducts.AddRange(rusari, shal, scarf, kousan);

            var colors = new[] { "سفید", "مشکی", "سرمه‌ای", "قرمز", "صورتی", "بنفش", "آبی", "کرم", "چندرنگ" }
                .Select(c => new Color { color = c }).ToArray();
            db.Colors.AddRange(colors);
            Color C(string name) => colors.First(c => c.color == name);

            var yalda = new Monasebat { MonasebatTitle = "شب یلدا" };
            var nowruz = new Monasebat { MonasebatTitle = "نوروز" };
            var madar = new Monasebat { MonasebatTitle = "روز مادر" };
            var valentine = new Monasebat { MonasebatTitle = "ولنتاین" };
            db.Monasebats.AddRange(yalda, nowruz, madar, valentine);
            db.SaveChanges();

            var catalogue = new (string code, string name, GroupProduct group, string img, string[] gallery, FarbicType[] fabrics, string[] colors, Monasebat[] occasions, string desc, int seen)[]
            {
                ("SR-101", "روسری پروانه‌های رنگی", rusari, "demo-p01", new[] { "demo-g01", "demo-g02" }, new[] { nakhi, abrisham, nakhAbrisham }, new[] { "چندرنگ", "آبی" }, new[] { nowruz }, "روسری با طرح پروانه و گل‌های آبرنگی، چاپ دیجیتال با ثبات رنگ بالا. ابعاد ۱۴۰×۱۴۰ سانتی‌متر، دور دوزی دستی.", 412),
                ("SR-102", "روسری شکوفه گیلاس", rusari, "demo-p02", new[] { "demo-g03" }, new[] { abrisham, nakhAbrisham }, new[] { "سفید", "صورتی" }, new[] { nowruz, madar }, "طرح شکوفه‌های گیلاس روی زمینه سفید؛ مناسب بهار و هدیه روز مادر. ابعاد ۱۴۰×۱۴۰.", 655),
                ("SR-103", "روسری کاشی هفت‌رنگ", rusari, "demo-p03", new[] { "demo-g04" }, new[] { nakhi, abrisham }, new[] { "چندرنگ" }, new[] { yalda }, "الهام گرفته از کاشی‌کاری هفت‌رنگ ایرانی، با رنگ‌بندی گرم و شاد. ابعاد ۱۳۵×۱۳۵.", 380),
                ("SR-104", "روسری چهل‌تکه", rusari, "demo-p04", new[] { "demo-g05" }, new[] { nakhi, nakhAbrisham, mohair }, new[] { "قرمز", "بنفش" }, new[] { yalda, valentine }, "طرح هندسی چهل‌تکه در ترکیب سرخابی، یشمی و بنفش. ابعاد ۱۴۰×۱۴۰.", 298),
                ("SR-105", "روسری گل‌های سرخ", rusari, "demo-p11", Array.Empty<string>(), new[] { nakhi, nakhAbrisham }, new[] { "قرمز", "کرم" }, new[] { valentine }, "زمینه کرم با گل‌های درشت سرخ، مناسب مهمانی. ابعاد ۱۳۵×۱۳۵.", 167),
                ("SH-201", "شال موج رنگی", shal, "demo-p05", new[] { "demo-g06" }, new[] { nakhi, mohair }, new[] { "چندرنگ", "سفید" }, new[] { nowruz }, "شال نخی سبک با طرح موج‌های رنگی، مناسب استفاده روزمره. ابعاد ۷۰×۲۰۰.", 521),
                ("SH-202", "شال ابریشم بنفش سلطنتی", shal, "demo-p06", Array.Empty<string>(), new[] { abrisham, nakhAbrisham }, new[] { "بنفش" }, new[] { yalda, madar }, "شال ابریشم با نقوش اسلیمی در طیف بنفش و فیروزه‌ای. ابعاد ۷۰×۲۰۰.", 734),
                ("SH-203", "شال طرح باغ ایرانی", shal, "demo-p12", Array.Empty<string>(), new[] { nakhAbrisham, mohair }, new[] { "بنفش", "آبی" }, new[] { nowruz }, "نقش گل و مرغ با رنگ‌های بنفش و طلایی. ابعاد ۷۵×۱۹۰.", 203),
                ("SH-204", "شال شکوفه‌ها", shal, "demo-p09", Array.Empty<string>(), new[] { nakhi, nakhAbrisham }, new[] { "سفید", "صورتی" }, new[] { madar }, "شال سبک با طرح شکوفه و پروانه؛ هدیه‌ای خاص برای روز مادر. ابعاد ۷۰×۱۹۰.", 145),
                ("MS-301", "مینی اسکارف ابریشمی پادشاهی", scarf, "demo-p08", new[] { "demo-g07" }, new[] { abrisham }, new[] { "بنفش", "قرمز" }, new[] { valentine }, "اسکارف ابریشم با طرح کلاسیک اروپایی، قابل استفاده به عنوان دستمال گردن یا روی کیف. ابعاد ۹۰×۹۰.", 276),
                ("MS-302", "مینی اسکارف کاتالان", scarf, "demo-p10", Array.Empty<string>(), new[] { abrisham, nakhAbrisham }, new[] { "سرمه‌ای", "قرمز" }, new[] { yalda }, "اسکارف کوچک با رنگ‌های جیغ و طرح هندسی. ابعاد ۷۰×۷۰.", 98),
                ("KO-401", "کوسن دوچرخه‌سوار", kousan, "demo-p07", new[] { "demo-g08" }, new[] { makhmal }, new[] { "سفید", "قرمز" }, Array.Empty<Monasebat>(), "کوسن مخمل با چاپ دیجیتال طرح دوچرخه‌سوار، ابعاد ۴۵×۴۵ با الیاف داخل.", 189),
            };

            var products = new List<Product>();
            for (var i = 0; i < catalogue.Length; i++)
            {
                var c = catalogue[i];
                var created = now.AddDays(-(catalogue.Length - i) * 3);   // spread over the last weeks; newest last
                var p = new Product
                {
                    CodeKala = c.code, Name = c.name, GroupProduct = c.group,
                    ImgMain = c.img + ".jpg", Imgdetail = c.img + ".jpg", ImgMainMob = c.img + "-mob.jpg", ImgdetailMob = c.img + "-mob.jpg",
                    Description = "<p>" + c.desc + "</p><p>شستشو با آب سرد و شوینده ملایم، اتو با حرارت کم از پشت پارچه.</p>",
                    Seen = c.seen, IsShow = true, Color = C(c.colors[0]),
                    CreateDate = Shamsi(created), CreateTime = created.ToShortTimeString(), CreateDateEnglish = created,
                };
                db.Products.Add(p);
                db.SaveChanges();
                products.Add(p);

                foreach (var f in c.fabrics) db.FarbicTypeProducts.Add(new FarbicTypeProducts { FarbicTypeId = f.Id, ProductId = p.Id });
                foreach (var col in c.colors) db.ColorsProducts.Add(new ColorsProduct { ColorId = C(col).Id, ProductId = p.Id });
                foreach (var m in c.occasions) db.MonasebatProducts.Add(new MonasebatProducts { MonasebatId = m.Id, ProductId = p.Id });
                db.Galleries.Add(new Gallery { ProductId = p.Id, ImgPath = c.img + ".jpg" });
                foreach (var g in c.gallery) db.Galleries.Add(new Gallery { ProductId = p.Id, ImgPath = g + ".jpg" });
            }
            db.SaveChanges();

            // ---------------- home page banners ----------------
            db.ImageMains.Add(new ImageMain { SliderTitle = "انواع شال و روسری سالینو", SliderIMG = "demo-slider.jpg", SliderIMGMob = "demo-slider-mob.jpg" });
            db.imgadvr.Add(new ImageAdver
            {
                // Img3/Img2 render in the tall (500px) slots, Img1/Img4 in the short (250px) ones.
                Img1 = "demo-advr-kousan.jpg", GroupIdTopRight = kousan.Id,
                Img2 = "demo-advr-rusari.jpg", GroupIdBottomRight = rusari.Id,
                Img3 = "demo-advr-shal.jpg", GroupIdTopLeft = shal.Id,
                Img4 = "demo-advr-delivery.jpg", GroupIdBottomLeft = scarf.Id,
            });
            db.ImageOffers.Add(new ImageOffer { SliderTitle = "تخفیف ویژه", SliderIMG = "demo-offer.jpg", SliderIMGMob = "demo-offer-mob.jpg" });

            // ---------------- customers ----------------
            var tehran = db.Ostans.First(o => o.ostanname == "تهران");
            var azarSharghi = db.Ostans.First(o => o.ostanname == "آذربایجان شرقی");
            var esfahan = db.Ostans.First(o => o.ostanname == "اصفهان");
            string Pass(string p) => SalinoMvc5.Compat.FormsAuthentication.HashPasswordForStoringInConfigFile(p, "MD5");
            var customers = new[]
            {
                new User { RoleId = 3, UserName = "مریم احمدی", Mobile = "09121234567", UserPassword = Pass("123456"), Ostans = tehran, ostanname = tehran.ostanname, cityname = "تهران", PostalCode = "1563874512", StreetAddress = "خیابان شریعتی، کوچه یاس، پلاک ۸، واحد ۳", IsActive = true, CodeActivate = "48213" },
                new User { RoleId = 3, UserName = "سارا رضایی", Mobile = "09141112233", UserPassword = Pass("123456"), Ostans = azarSharghi, ostanname = azarSharghi.ostanname, cityname = "تبریز", PostalCode = "5153714651", StreetAddress = "ولیعصر، خیابان فارابی، پلاک ۲۴", IsActive = true, CodeActivate = "73920" },
                new User { RoleId = 3, UserName = "نگار کریمی", Mobile = "09131234567", UserPassword = Pass("123456"), Ostans = esfahan, ostanname = esfahan.ostanname, cityname = "اصفهان", PostalCode = "8174673111", StreetAddress = "خیابان چهارباغ بالا، کوچه ۱۲، پلاک ۵", IsActive = true, CodeActivate = "19384" },
                new User { RoleId = 4, UserName = "بوتیک رز (همکار)", Mobile = "09147778899", UserPassword = Pass("123456"), Ostans = azarSharghi, ostanname = azarSharghi.ostanname, cityname = "مراغه", PostalCode = "5513845761", StreetAddress = "بازار سرپوشیده، راسته شال‌فروشان، غرفه ۱۸", IsActive = true, CodeActivate = "55210" },
                new User { RoleId = 3, UserName = "زهرا محمدی", Mobile = "09351234567", UserPassword = Pass("123456"), Ostans = tehran, ostanname = tehran.ostanname, cityname = "کرج", PostalCode = "3134567890", StreetAddress = "عظیمیه، میدان مهران، پلاک ۴۲", IsActive = false, CodeActivate = "66104" },
            };
            db.users.AddRange(customers);
            db.SaveChanges();

            // ---------------- orders (paid factors with details) ----------------
            var sendTypes = db.TypeSendPosts.OrderBy(x => x.Id).ToList();
            var orders = new (User user, int daysAgo, StatusAllTypeFactorsEnum status, (int product, FarbicType fabric, int count)[] items, int sendType, int discount)[]
            {
                (customers[0], 0, StatusAllTypeFactorsEnum.Pending,            new[] { (0, abrisham, 1), (1, nakhAbrisham, 1) }, 0, 15000),
                (customers[1], 0, StatusAllTypeFactorsEnum.Preparing,          new[] { (6, abrisham, 2) }, 2, 0),
                (customers[2], 1, StatusAllTypeFactorsEnum.Sending,            new[] { (2, nakhi, 1), (5, nakhi, 1), (11, makhmal, 2) }, 1, 0),
                (customers[0], 3, StatusAllTypeFactorsEnum.CompleteSubmission, new[] { (3, mohair, 1) }, 0, 20000),
                (customers[3], 4, StatusAllTypeFactorsEnum.CompleteSubmission, new[] { (0, nakhi, 3), (2, nakhi, 3), (4, nakhi, 3), (5, nakhi, 3), (8, nakhi, 3), (3, nakhi, 3) }, 0, 0),
                (customers[1], 9, StatusAllTypeFactorsEnum.CompleteSubmission, new[] { (9, abrisham, 1), (7, nakhAbrisham, 1) }, 0, 0),
                (customers[2], 16, StatusAllTypeFactorsEnum.Returns,           new[] { (10, abrisham, 1) }, 1, 0),
                (customers[0], 24, StatusAllTypeFactorsEnum.CompleteSubmission, new[] { (1, abrisham, 2) }, 0, 0),
            };
            long factorNumber = 1000;
            var random = new Random(1405);
            foreach (var o in orders.OrderBy(o => -o.daysAgo))
            {
                var date = now.AddDays(-o.daysAgo).Date.AddHours(10 + random.Next(10)).AddMinutes(random.Next(60));
                var shamsi = Shamsi(date);
                var send = sendTypes[o.sendType];
                var itemsTotal = o.items.Sum(i => (int)i.fabric.PriceMain * i.count);
                var shipping = int.Parse(send.PriceHaml);
                var f = new FactorMain
                {
                    RoleId = o.user.RoleId, UserId = o.user.Id, Username = o.user.UserName, UserMobile = o.user.Mobile,
                    UserCodePosti = o.user.PostalCode, UserOstan = o.user.ostanname, UserCity = o.user.cityname, AddressUser = o.user.StreetAddress,
                    Amount = itemsTotal, Discount = o.discount, SumAllPrice = itemsTotal + shipping - o.discount,
                    IsPay = true, Paymentstatus = "100", PaymentNumber = (73000000 + random.Next(999999)).ToString(),
                    SaleReferenceId = "A00000000000000000000000000" + random.Next(100000000, 999999999), RRN = random.Next(100000000, 999999999).ToString(),
                    FactorNumber = ++factorNumber, StatusAll = o.status, TypeSendPostId = send.Id, TypeSendPostName = send.NameHaml,
                    DateCreate = shamsi, Time = date.ToShortTimeString(), DateCreateDatetime = date,
                    Year = shamsi.ConvertIntYear(), Month = shamsi.ConvertIntMonth(), Day = shamsi.ConvertIntDay(),
                    PostNumber = o.status is StatusAllTypeFactorsEnum.Sending or StatusAllTypeFactorsEnum.CompleteSubmission ? "1234567890" + random.Next(10000000, 99999999) : null,
                };
                db.FactorMains.Add(f);
                db.SaveChanges();
                foreach (var (productIndex, fabric, count) in o.items)
                {
                    var p = products[productIndex];
                    db.FactorDetails.Add(new FactorDetail
                    {
                        FactorMainId = f.Id, ProductId = p.Id, ProductName = p.Name, FarbicTypeId = fabric.Id, FarbicTypeName = fabric.tiltle,
                        DetailCount = count, DetailPrice = (int)fabric.PriceMain, SumPrice = (int)fabric.PriceMain * count,
                    });
                }
            }
            db.SaveChanges();

            // ---------------- comments (with admin answers), tickets, feedback ----------------
            void Comment(int productIndex, string name, string text, string answer)
            {
                var c = new Comments { ProductId = products[productIndex].Id, Name = name, TextComment = text, IsShow = true, OkAnswer = answer != null };
                db.Comments.Add(c);
                db.SaveChanges();
                if (answer != null)
                    db.Comments.Add(new Comments { ProductId = c.ProductId, ParentId = c.Id, Name = "مدیر", TextComment = answer, IsShow = true, OkAnswer = true });
            }
            Comment(0, "مریم", "کیفیت چاپ عالیه و رنگ‌ها بعد از شستشو هم ثابت موند. ممنون از بسته‌بندی شیکتون.", "ممنون از لطف شما مریم جان، خوشحالیم که راضی بودید.");
            Comment(1, "الهام", "جنس ابریشمش واقعا لطیفه. برای هدیه روز مادر گرفتم.", "انتخاب خیلی خوبیه، امیدواریم مادر عزیزتون لذت ببرن.");
            Comment(1, "فاطمه", "این طرح سایز بزرگترش هم موجود میشه؟", "سلام، ابعاد ۱۴۰×۱۴۰ بزرگترین سایز این طرحه.");
            Comment(6, "نرگس", "رنگ بنفشش از عکس هم قشنگ‌تره 😍", null);
            Comment(2, "سمیرا", "ارسال خیلی سریع بود، دو روزه رسید تهران.", "ممنون از همراهی شما.");
            Comment(11, "حسین", "کوسن رو برای مبل گرفتم، جنس مخملش خوبه.", null);

            var ticket = new Ticket { UserId = customers[0].Id, TitleTicket = "پیگیری سفارش", TextTicket = "سلام، سفارشم هنوز ارسال نشده. لطفا پیگیری کنید." };
            var ticket2 = new Ticket { UserId = customers[3].Id, TitleTicket = "خرید عمده", TextTicket = "برای خرید بیش از ۵۰ عدد تخفیف بیشتری دارید؟" };
            db.Ticket.AddRange(ticket, ticket2);
            db.SaveChanges();
            ticket.OkAnswer = true;
            db.Ticket.Add(new Ticket { UserId = ticket.UserId, ParentId = ticket.Id, TitleTicket = "پاسخ: پیگیری سفارش", TextTicket = "سلام، سفارش شما امروز تحویل پست شد و کد رهگیری در پنل کاربری ثبت شده است.", OkAnswer = true });

            var feedback = new Entegadat { Name = "رضا", Email = "reza@example.com", TextComment = "پیشنهاد می‌کنم امکان پرداخت در محل هم اضافه کنید." };
            db.Entegadats.AddRange(feedback, new Entegadat { Name = "لیلا", Email = "leila@example.com", TextComment = "سایت خیلی خوب و ساده است، فقط عکس‌های بیشتری از هر محصول بگذارید." });
            db.SaveChanges();
            feedback.OkAnswer = true;
            db.Entegadats.Add(new Entegadat { Parentid = feedback.Id, Name = "مدیر", Email = "info@salinotbz.ir", TextComment = "ممنون از پیشنهاد شما، در حال بررسی است.", OkAnswer = true });
            db.SaveChanges();

            tx.Commit();
            Console.WriteLine($"Demo data added: {products.Count} products, {customers.Length} customers, {orders.Length} orders.");
        }
    }
}
