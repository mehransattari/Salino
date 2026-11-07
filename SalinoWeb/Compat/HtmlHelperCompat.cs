using Microsoft.AspNetCore.Html;

namespace SalinoMvc5.Compat
{
    public static class HtmlHelperCompat
    {
        /// <summary>MVC5 overload Html.Label(expression, htmlAttributes) that ASP.NET Core does not have.</summary>
        public static IHtmlContent Label(this IHtmlHelper html, string expression, object htmlAttributes)
            => html.Label(expression, null, htmlAttributes);
    }

    public static class HttpRequestCompat
    {
        /// <summary>Equivalent of HttpRequest.Params[key] (query string first, then form).</summary>
        public static string Param(this HttpRequest request, string key)
            => request.Query[key].FirstOrDefault()
               ?? (request.HasFormContentType ? request.Form[key].FirstOrDefault() : null);
    }
}
