using Microsoft.AspNetCore.Mvc.Razor;

namespace SalinoMvc5.Compat
{
    /// <summary>Razor base page that restores the WebViewPage.Session property used by the views.</summary>
    public abstract class SalinoRazorPage<TModel> : RazorPage<TModel>
    {
        public SessionStore Session => SessionStore.For(Context);
    }
}
