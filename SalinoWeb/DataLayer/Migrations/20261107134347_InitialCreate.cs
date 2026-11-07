using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SalinoMvc5.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    color = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entegadats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Parentid = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OkAnswer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entegadats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entegadats_Entegadats_Parentid",
                        column: x => x.Parentid,
                        principalTable: "Entegadats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FarbicTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tiltle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PriceMain = table.Column<long>(type: "bigint", nullable: false),
                    IsShow = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarbicTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "gifcardDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    NumberUse = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreateDateShamsi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpirationDateShamsi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gifcardDates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "giftcards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    NumberUse = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_giftcards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GroupNotShow = table.Column<bool>(type: "bit", nullable: false),
                    imagepath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageAdvers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Img1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupIdTopRight = table.Column<int>(type: "int", nullable: false),
                    Img2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupIdBottomRight = table.Column<int>(type: "int", nullable: false),
                    Img3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupIdTopLeft = table.Column<int>(type: "int", nullable: false),
                    Img4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupIdBottomLeft = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageAdvers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageMains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SliderTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SliderIMG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SliderIMGMob = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageMains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SliderTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SliderIMG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SliderIMGMob = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageOffers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mojavezs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListKhadamat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mojavezs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Monasebats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonasebatTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monasebats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferFirstShops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceDiscount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferFirstShops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferNumberShops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToNumber = table.Column<int>(type: "int", nullable: false),
                    PriceDiscount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferNumberShops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    AsNumber = table.Column<int>(type: "int", nullable: false),
                    Createdate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferPrices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ostans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OstanCode = table.Column<int>(type: "int", nullable: false),
                    ostanname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ostans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RoleTitle = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SendForMajors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameHaml = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PriceHaml = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendForMajors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SendPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendPrices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DecAboutMe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DecContactUs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Questions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Tell = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    imageSrc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imageSrcMain = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Socials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SocialName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SocialIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialLink = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NotShow = table.Column<bool>(type: "bit", nullable: false),
                    SocialOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeSendPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameHaml = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PriceHaml = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeSendPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ColorsProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorsProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColorsProducts_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MajorShops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinSelectedProduct = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    FarbicId = table.Column<int>(type: "int", nullable: false),
                    PriceMajor = table.Column<int>(type: "int", nullable: false),
                    Createdate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MajorShops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MajorShops_FarbicTypes_FarbicId",
                        column: x => x.FarbicId,
                        principalTable: "FarbicTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeKala = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Imgdetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgdetailMob = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgMain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgMainMob = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seen = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsShow = table.Column<bool>(type: "bit", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: true),
                    CreateDateEnglish = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_GroupProducts_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserPassword_Disposable = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    cityname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ostanname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OstanId = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Createdate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeActivate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Ostans_OstanId",
                        column: x => x.OstanId,
                        principalTable: "Ostans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TextComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Createdate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsShow = table.Column<bool>(type: "bit", nullable: false),
                    OkAnswer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FarbicTypeProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FarbicTypeId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarbicTypeProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FarbicTypeProducts_FarbicTypes_FarbicTypeId",
                        column: x => x.FarbicTypeId,
                        principalTable: "FarbicTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FarbicTypeProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FarbicTypeProducts1",
                columns: table => new
                {
                    FarbicType_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarbicTypeProducts1", x => new { x.FarbicType_Id, x.Product_Id });
                    table.ForeignKey(
                        name: "FK_FarbicTypeProducts1_FarbicTypes_FarbicType_Id",
                        column: x => x.FarbicType_Id,
                        principalTable: "FarbicTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FarbicTypeProducts1_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Galleries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImgPath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Galleries_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonasebatProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonasebatId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonasebatProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonasebatProducts_Monasebats_MonasebatId",
                        column: x => x.MonasebatId,
                        principalTable: "Monasebats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonasebatProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonasebatProducts1",
                columns: table => new
                {
                    Monasebat_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonasebatProducts1", x => new { x.Monasebat_Id, x.Product_Id });
                    table.ForeignKey(
                        name: "FK_MonasebatProducts1_Monasebats_Monasebat_Id",
                        column: x => x.Monasebat_Id,
                        principalTable: "Monasebats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonasebatProducts1_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FactorMains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CodeGenerator = table.Column<long>(type: "bigint", nullable: false),
                    Paymentstatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FactorNumber = table.Column<long>(type: "bigint", nullable: false),
                    CardNumberMasked = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RRN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    SaleReferenceId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    SumAllPrice = table.Column<int>(type: "int", nullable: false),
                    IsPay = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCodePosti = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserOstan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreateDatetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Deliverytime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusAll = table.Column<int>(type: "int", nullable: false),
                    TypeSendPostId = table.Column<int>(type: "int", nullable: false),
                    TypeSendPostName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorMains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactorMains_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    TitleTicket = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TextTicket = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Createdate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OkAnswer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Tickets_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Tickets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FactorDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FactorMainId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FarbicTypeId = table.Column<int>(type: "int", nullable: false),
                    FarbicTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailCount = table.Column<int>(type: "int", nullable: false),
                    DetailPrice = table.Column<int>(type: "int", nullable: false),
                    SumPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactorDetails_FactorMains_FactorMainId",
                        column: x => x.FactorMainId,
                        principalTable: "FactorMains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FarbicTypes",
                columns: new[] { "Id", "IsShow", "PriceMain", "tiltle" },
                values: new object[] { 1, true, 1L, "نخی" });

            migrationBuilder.InsertData(
                table: "OfferFirstShops",
                columns: new[] { "Id", "PriceDiscount" },
                values: new object[] { 1, 0 });

            migrationBuilder.InsertData(
                table: "OfferNumberShops",
                columns: new[] { "Id", "PriceDiscount", "ToNumber" },
                values: new object[] { 1, 0, 0 });

            migrationBuilder.InsertData(
                table: "OfferPrices",
                columns: new[] { "Id", "AsNumber", "Createdate", "Price" },
                values: new object[] { 1, 0, new DateTime(2020, 1, 20, 13, 14, 11, 977, DateTimeKind.Unspecified), 0 });

            migrationBuilder.InsertData(
                table: "Ostans",
                columns: new[] { "Id", "OstanCode", "ostanname" },
                values: new object[,]
                {
                    { 1, 1, "تهران" },
                    { 2, 2, "گیلان" },
                    { 3, 3, "آذربایجان شرقی" },
                    { 4, 4, "خوزستان" },
                    { 5, 5, "فارس" },
                    { 6, 6, "اصفهان" },
                    { 7, 7, "خراسان رضوی" },
                    { 8, 8, "قزوین" },
                    { 9, 9, "سمنان" },
                    { 10, 10, "قم" },
                    { 11, 11, "مرکزی" },
                    { 12, 12, "زنجان" },
                    { 13, 13, "مازندران" },
                    { 14, 14, "گلستان" },
                    { 15, 15, "اردبیل" },
                    { 16, 16, "آذربایجان غربی" },
                    { 17, 17, "همدان" },
                    { 18, 18, "کردستان" },
                    { 19, 19, "کرمانشاه" },
                    { 20, 20, "لرستان" },
                    { 21, 21, "بوشهر" },
                    { 22, 22, "کرمان" },
                    { 23, 23, "هرمزگان" },
                    { 24, 24, "چهارمحال و بختیاری" },
                    { 25, 25, "یزد" },
                    { 26, 26, "سیستان و بلوچستان" },
                    { 27, 27, "ایلام" },
                    { 28, 28, "کهگلویه و بویراحمد" },
                    { 29, 29, "خراسان شمالی" },
                    { 30, 30, "خراسان جنوبی" },
                    { 31, 31, "البرز" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName", "RoleTitle" },
                values: new object[,]
                {
                    { 1, "Admin1", "مدیرکل" },
                    { 2, "Admin2", "مدیرفاکتور" },
                    { 3, "User1", "کاربرعادی" },
                    { 4, "User2", "کاربرعمده" },
                    { 5, "User3", "کاربرنماینده" }
                });

            migrationBuilder.InsertData(
                table: "SendForMajors",
                columns: new[] { "Id", "NameHaml", "PriceHaml" },
                values: new object[] { 1, "0", "0" });

            migrationBuilder.InsertData(
                table: "SendPrices",
                columns: new[] { "Id", "AsNumber" },
                values: new object[] { 1, 0 });

            migrationBuilder.InsertData(
                table: "TypeSendPosts",
                columns: new[] { "Id", "NameHaml", "PriceHaml" },
                values: new object[] { 1, "0", "0" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CodeActivate", "Createdate", "IsActive", "Mobile", "OstanId", "PostalCode", "RoleId", "StreetAddress", "UserName", "UserPassword", "UserPassword_Disposable", "cityname", "ostanname" },
                values: new object[] { 1, "1234", "1400/05/08", true, "09146143251", 1, "515666666", 1, "tabriz-valiasr", "salino", "871C416E52A7E20B4F9F9C682EEC725E", null, "تبریز", "azari" });

            migrationBuilder.CreateIndex(
                name: "IX_ColorsProducts_ColorId",
                table: "ColorsProducts",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentId",
                table: "Comments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductId",
                table: "Comments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Entegadats_Parentid",
                table: "Entegadats",
                column: "Parentid");

            migrationBuilder.CreateIndex(
                name: "IX_FactorDetails_FactorMainId",
                table: "FactorDetails",
                column: "FactorMainId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorMains_UserId",
                table: "FactorMains",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FarbicTypeProducts_FarbicTypeId",
                table: "FarbicTypeProducts",
                column: "FarbicTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FarbicTypeProducts_ProductId",
                table: "FarbicTypeProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_FarbicTypeProducts1_Product_Id",
                table: "FarbicTypeProducts1",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_ProductId",
                table: "Galleries",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MajorShops_FarbicId",
                table: "MajorShops",
                column: "FarbicId");

            migrationBuilder.CreateIndex(
                name: "IX_MonasebatProducts_MonasebatId",
                table: "MonasebatProducts",
                column: "MonasebatId");

            migrationBuilder.CreateIndex(
                name: "IX_MonasebatProducts_ProductId",
                table: "MonasebatProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MonasebatProducts1_Product_Id",
                table: "MonasebatProducts1",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ColorId",
                table: "Products",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_GroupId",
                table: "Products",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ParentId",
                table: "Tickets",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OstanId",
                table: "Users",
                column: "OstanId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColorsProducts");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Entegadats");

            migrationBuilder.DropTable(
                name: "FactorDetails");

            migrationBuilder.DropTable(
                name: "FarbicTypeProducts");

            migrationBuilder.DropTable(
                name: "FarbicTypeProducts1");

            migrationBuilder.DropTable(
                name: "Galleries");

            migrationBuilder.DropTable(
                name: "gifcardDates");

            migrationBuilder.DropTable(
                name: "giftcards");

            migrationBuilder.DropTable(
                name: "ImageAdvers");

            migrationBuilder.DropTable(
                name: "ImageMains");

            migrationBuilder.DropTable(
                name: "ImageOffers");

            migrationBuilder.DropTable(
                name: "MajorShops");

            migrationBuilder.DropTable(
                name: "Mojavezs");

            migrationBuilder.DropTable(
                name: "MonasebatProducts");

            migrationBuilder.DropTable(
                name: "MonasebatProducts1");

            migrationBuilder.DropTable(
                name: "OfferFirstShops");

            migrationBuilder.DropTable(
                name: "OfferNumberShops");

            migrationBuilder.DropTable(
                name: "OfferPrices");

            migrationBuilder.DropTable(
                name: "SendForMajors");

            migrationBuilder.DropTable(
                name: "SendPrices");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Socials");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "TypeSendPosts");

            migrationBuilder.DropTable(
                name: "FactorMains");

            migrationBuilder.DropTable(
                name: "FarbicTypes");

            migrationBuilder.DropTable(
                name: "Monasebats");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "GroupProducts");

            migrationBuilder.DropTable(
                name: "Ostans");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
