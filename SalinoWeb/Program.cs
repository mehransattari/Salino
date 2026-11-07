using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Salino.DataLayer;
using SalinoMvc5.SalinoUtilities;

var builder = WebApplication.CreateBuilder(args);

// Legacy code creates "new SalinoContext()" everywhere, so the connection string is static.
SalinoContext.ConnectionString = builder.Configuration.GetConnectionString("SalinoContext");
PaymentOptions.Current = builder.Configuration.GetSection("Payment").Get<PaymentOptions>() ?? new PaymentOptions();
SmsService.Options = builder.Configuration.GetSection("SmsIr").Get<SmsIrOptions>() ?? new SmsIrOptions();

// Local development only: "dotnet run -- --seed-demo" fills an empty database with demo data and exits.
if (args.Contains("--seed-demo"))
{
    Salino.DataLayer.DemoData.DemoDataSeeder.Run();
    return;
}

builder.Services.AddDbContext<SalinoContext>(o => o.UseSqlServer(SalinoContext.ConnectionString));

builder.Services
    .AddControllersWithViews()
    .AddNewtonsoftJson(o =>
    {
        // Same JSON shape as MVC5 / Web API 2: PascalCase property names, loops ignored.
        o.SerializerSettings.ContractResolver = new DefaultContractResolver();
        o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

// MVC5 emitted Persian text as-is; ASP.NET Core would entity-encode every non-Latin character.
builder.Services.AddWebEncoders(o => o.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All));

builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(o =>
{
    o.IdleTimeout = SessionStore.IdleTimeout;
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
});

// Replaces <authentication mode="Forms"> + the custom RoleProvider.
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.Cookie.Name = "myshop";
        o.Cookie.HttpOnly = true;
        o.LoginPath = "/User/login";
        o.AccessDeniedPath = "/User/login";
        o.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
        o.SlidingExpiration = true;
    });
builder.Services.AddScoped<IClaimsTransformation, DatabaseRoleClaimsTransformation>();
builder.Services.AddAuthorization();

var app = builder.Build();

WebImage.WebRootPath = app.Environment.WebRootPath;

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // attribute-routed API controllers (api/...)
app.MapAreaControllerRoute(
    name: "Admin_default",
    areaName: "Admin",
    pattern: "Admin/{controller}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
