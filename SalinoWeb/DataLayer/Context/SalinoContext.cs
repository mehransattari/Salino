using Salino.Models;

namespace Salino.DataLayer
{
    public class SalinoContext : DbContext
    {
        /// <summary>Set once at startup from configuration (ConnectionStrings:SalinoContext).</summary>
        public static string ConnectionString { get; set; }

        // The legacy code (controllers, views, utilities) creates contexts with "new SalinoContext()".
        public SalinoContext() { }

        public SalinoContext(DbContextOptions<SalinoContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (string.IsNullOrEmpty(ConnectionString))
                    throw new InvalidOperationException("SalinoContext.ConnectionString has not been configured.");
                optionsBuilder.UseSqlServer(ConnectionString);
            }
            // EF6 lazy loading was on by default and the code depends on it (e.g. user.Roles.Id).
            optionsBuilder.UseLazyLoadingProxies();
        }

        //_________________________________________________________________________________
        #region Users
        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Entegadat> Entegadats { get; set; }
        public DbSet<Mojavez> Mojavezs { get; set; }
        #endregion

        #region Products
        public DbSet<Product> Products { get; set; }
        public DbSet<ColorsProduct> ColorsProducts { get; set; }
        public DbSet<GroupProduct> groupProducts { get; set; }
        public DbSet<FarbicTypeProducts> FarbicTypeProducts { get; set; }
        public DbSet<FarbicType> FarbicTypes { get; set; }
        public DbSet<MonasebatProducts> MonasebatProducts { get; set; }
        public DbSet<Monasebat> Monasebats { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        #endregion

        #region Factors
        public DbSet<TypeSendPost> TypeSendPosts { get; set; }
        public DbSet<FactorMain> FactorMains { get; set; }
        public DbSet<FactorDetail> FactorDetails { get; set; }
        #endregion

        #region Others
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Ostan> Ostans { get; set; }
        #endregion

        #region Images
        public DbSet<ImageMain> ImageMains { get; set; }
        public DbSet<ImageOffer> ImageOffers { get; set; }
        public DbSet<ImageAdver> imgadvr { get; set; }
        #endregion

        #region Offers
        public DbSet<SendPrice> SendPrices { get; set; }
        public DbSet<giftcard> giftcards { get; set; }
        public DbSet<gifcardDate> gifcardDates { get; set; }
        public DbSet<OfferPrice> OfferPrices { get; set; }
        public DbSet<MajorShop> MajorShops { get; set; }
        public DbSet<SendForMajor> SendForMajors { get; set; }
        public DbSet<OfferFirstShop> OfferFirstShops { get; set; }
        public DbSet<OfferNumberShop> OfferNumberShops { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Table names exactly as EF6 created them (pluralised entity names, dbo schema).
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Ticket>().ToTable("Tickets");
            modelBuilder.Entity<Entegadat>().ToTable("Entegadats");
            modelBuilder.Entity<Mojavez>().ToTable("Mojavezs");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<ColorsProduct>().ToTable("ColorsProducts");
            modelBuilder.Entity<GroupProduct>().ToTable("GroupProducts");
            modelBuilder.Entity<FarbicTypeProducts>().ToTable("FarbicTypeProducts");
            modelBuilder.Entity<FarbicType>().ToTable("FarbicTypes");
            modelBuilder.Entity<MonasebatProducts>().ToTable("MonasebatProducts");
            modelBuilder.Entity<Monasebat>().ToTable("Monasebats");
            modelBuilder.Entity<Color>().ToTable("Colors");
            modelBuilder.Entity<Gallery>().ToTable("Galleries");
            modelBuilder.Entity<TypeSendPost>().ToTable("TypeSendPosts");
            modelBuilder.Entity<FactorMain>().ToTable("FactorMains");
            modelBuilder.Entity<FactorDetail>().ToTable("FactorDetails");
            modelBuilder.Entity<Setting>().ToTable("Settings");
            modelBuilder.Entity<Social>().ToTable("Socials");
            modelBuilder.Entity<Comments>().ToTable("Comments");
            modelBuilder.Entity<Ostan>().ToTable("Ostans");
            modelBuilder.Entity<ImageMain>().ToTable("ImageMains");
            modelBuilder.Entity<ImageOffer>().ToTable("ImageOffers");
            modelBuilder.Entity<ImageAdver>().ToTable("ImageAdvers");
            modelBuilder.Entity<SendPrice>().ToTable("SendPrices");
            modelBuilder.Entity<giftcard>().ToTable("giftcards");
            modelBuilder.Entity<gifcardDate>().ToTable("gifcardDates");
            modelBuilder.Entity<OfferPrice>().ToTable("OfferPrices");
            modelBuilder.Entity<MajorShop>().ToTable("MajorShops");
            modelBuilder.Entity<SendForMajor>().ToTable("SendForMajors");
            modelBuilder.Entity<OfferFirstShop>().ToTable("OfferFirstShops");
            modelBuilder.Entity<OfferNumberShop>().ToTable("OfferNumberShops");

            // EF6 used datetime (not datetime2) for DateTime columns.
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties())
                         .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?)))
                property.SetColumnType("datetime");

            // Relationships, matching the EF6 associations and delete rules.
            modelBuilder.Entity<ColorsProduct>().HasOne(x => x.color).WithMany(x => x.ColorsProduct)
                .HasForeignKey(x => x.ColorId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>().HasOne(x => x.Color).WithMany()
                .HasForeignKey(x => x.ColorId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Product>().HasOne(x => x.GroupProduct).WithMany(x => x.Products)
                .HasForeignKey(x => x.GroupId).OnDelete(DeleteBehavior.Cascade);

            // EF6 implicit many-to-many join tables.
            modelBuilder.Entity<Product>().HasMany(x => x.FarbicType).WithMany(x => x.Products)
                .UsingEntity<Dictionary<string, object>>("FarbicTypeProduct",
                    r => r.HasOne<FarbicType>().WithMany().HasForeignKey("FarbicType_Id").OnDelete(DeleteBehavior.Cascade),
                    l => l.HasOne<Product>().WithMany().HasForeignKey("Product_Id").OnDelete(DeleteBehavior.Cascade),
                    j => { j.ToTable("FarbicTypeProducts1"); j.HasKey("FarbicType_Id", "Product_Id"); });
            modelBuilder.Entity<Product>().HasMany(x => x.Monasebats).WithMany(x => x.Products)
                .UsingEntity<Dictionary<string, object>>("MonasebatProduct",
                    r => r.HasOne<Monasebat>().WithMany().HasForeignKey("Monasebat_Id").OnDelete(DeleteBehavior.Cascade),
                    l => l.HasOne<Product>().WithMany().HasForeignKey("Product_Id").OnDelete(DeleteBehavior.Cascade),
                    j => { j.ToTable("MonasebatProducts1"); j.HasKey("Monasebat_Id", "Product_Id"); });

            modelBuilder.Entity<Comments>().HasOne(x => x.product).WithMany()
                .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Comments>().HasOne(x => x.Parent).WithMany()
                .HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Entegadat>().HasOne(x => x.Parent).WithMany()
                .HasForeignKey(x => x.Parentid).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FactorDetail>().HasOne(x => x.FactorMain).WithMany(x => x.FactorDetails)
                .HasForeignKey(x => x.FactorMainId).OnDelete(DeleteBehavior.Cascade);
            // FactorDetails.ProductId intentionally has no FK (dropped by migration 202107291739164_asas).

            modelBuilder.Entity<FarbicTypeProducts>().HasOne(x => x.FarbicTypes).WithMany()
                .HasForeignKey(x => x.FarbicTypeId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<FarbicTypeProducts>().HasOne(x => x.Products).WithMany()
                .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MonasebatProducts>().HasOne(x => x.Monasebats).WithMany()
                .HasForeignKey(x => x.MonasebatId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<MonasebatProducts>().HasOne(x => x.Products).WithMany()
                .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Gallery>().HasOne(x => x.Product).WithMany()
                .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MajorShop>().HasOne(x => x.FarbicType).WithMany()
                .HasForeignKey(x => x.FarbicId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasMany(x => x.FactorMain).WithOne()
                .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasOne(x => x.Ostans).WithMany(x => x.User)
                .HasForeignKey(x => x.OstanId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasOne(x => x.Roles).WithMany()
                .HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>().HasOne(x => x.User).WithMany()
                .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Ticket>().HasOne(x => x.Parent).WithMany()
                .HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);

            SeedData.Apply(modelBuilder);
        }
    }
}
