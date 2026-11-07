using Salino.DataLayer;

namespace SalinoMvc5.ViewComponents
{
    // ASP.NET Core has no child actions (@Html.Action). These view components replace
    // HomeController.ImageAdvr/ImageOffer/Social/Menu and ProductController.Comment(int)
    // and render the same partial views.

    public class ImageAdvrViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            using var db = new SalinoContext();
            return View("~/Views/Home/ImageAdvr.cshtml", db.imgadvr.AsNoTracking().FirstOrDefault());
        }
    }

    public class ImageOfferViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            using var db = new SalinoContext();
            return View("~/Views/Home/ImageOffer.cshtml", db.ImageOffers.AsNoTracking().FirstOrDefault());
        }
    }

    public class SocialViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            using var db = new SalinoContext();
            return View("~/Views/Home/Social.cshtml", db.Socials.AsNoTracking().ToList());
        }
    }

    public class MenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View<object>("~/Views/Home/Menu.cshtml", null);
    }

    public class CommentViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int id)
        {
            SessionStore.For(HttpContext)["ProductId"] = id;
            ViewBag.proid = id;
            return View<Salino.Models.Comments>("~/Views/Product/Comment.cshtml", null);
        }
    }
}
