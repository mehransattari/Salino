using Salino.Models;
using SalinoMvc5.Compat;

namespace Salino.DataLayer
{
    /// <summary>
    /// Port of the EF6 Migrations.Configuration.Seed() data, applied through migrations (HasData).
    /// </summary>
    internal static class SeedData
    {
        public static void Apply(ModelBuilder b)
        {
            b.Entity<Role>().HasData(
                new { Id = 1, RoleName = "Admin1", RoleTitle = "مدیرکل" },
                new { Id = 2, RoleName = "Admin2", RoleTitle = "مدیرفاکتور" },
                new { Id = 3, RoleName = "User1", RoleTitle = "کاربرعادی" },
                new { Id = 4, RoleName = "User2", RoleTitle = "کاربرعمده" },
                new { Id = 5, RoleName = "User3", RoleTitle = "کاربرنماینده" });

            var ostans = new[]
            {
                "تهران", "گیلان", "آذربایجان شرقی", "خوزستان", "فارس", "اصفهان", "خراسان رضوی", "قزوین",
                "سمنان", "قم", "مرکزی", "زنجان", "مازندران", "گلستان", "اردبیل", "آذربایجان غربی", "همدان",
                "کردستان", "کرمانشاه", "لرستان", "بوشهر", "کرمان", "هرمزگان", "چهارمحال و بختیاری", "یزد",
                "سیستان و بلوچستان", "ایلام", "کهگلویه و بویراحمد", "خراسان شمالی", "خراسان جنوبی", "البرز",
            };
            b.Entity<Ostan>().HasData(ostans.Select((name, i) => (object)new { Id = i + 1, OstanCode = i + 1, ostanname = name }).ToArray());

            b.Entity<User>().HasData(new
            {
                Id = 1,
                RoleId = 1,
                OstanId = 1,
                ostanname = "azari",
                IsActive = true,
                UserPassword = FormsAuthentication.HashPasswordForStoringInConfigFile("09146143251", "MD5"),
                CodeActivate = "1234",
                UserName = "salino",
                cityname = "تبریز",
                PostalCode = "515666666",
                StreetAddress = "tabriz-valiasr",
                Mobile = "09146143251",
                Createdate = "1400/05/08",
            });

            b.Entity<OfferPrice>().HasData(new { Id = 1, Price = 0, AsNumber = 0, Createdate = new DateTime(2020, 1, 20, 13, 14, 11, 977) });
            b.Entity<OfferFirstShop>().HasData(new { Id = 1, PriceDiscount = 0 });
            b.Entity<OfferNumberShop>().HasData(new { Id = 1, PriceDiscount = 0, ToNumber = 0 });
            b.Entity<SendPrice>().HasData(new { Id = 1, AsNumber = 0 });
            b.Entity<TypeSendPost>().HasData(new { Id = 1, PriceHaml = "0", NameHaml = "0" });
            b.Entity<SendForMajor>().HasData(new { Id = 1, PriceHaml = "0", NameHaml = "0" });
            b.Entity<FarbicType>().HasData(new { Id = 1, IsShow = true, PriceMain = 1L, tiltle = "نخی" });
        }
    }
}
