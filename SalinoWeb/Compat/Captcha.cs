using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SalinoMvc5.Compat
{
    /// <summary>
    /// Minimal replacement for CaptchaMvc (which has no ASP.NET Core version).
    /// Keeps the same call shapes: @Html.Captcha(...) in views and this.IsCaptchaValid(...) in controllers.
    /// </summary>
    public static class CaptchaHelper
    {
        internal const string SessionKey = "__captcha_code";
        internal const string InputName = "CaptchaInputText";
        private const string Alphabet = "23456789ABCDEFGHJKLMNPQRSTUVWXYZ";

        public static IHtmlContent Captcha(this IHtmlHelper html, string refreshText, string inputText,
            int length, string requiredMessageText, bool addValidationSpan)
        {
            var refresh = WebUtility.HtmlEncode(refreshText);
            var input = WebUtility.HtmlEncode(inputText);
            var required = WebUtility.HtmlEncode(requiredMessageText);
            var src = $"/Captcha/Image?l={length}";
            var sb = new StringBuilder();
            sb.Append($"<img id=\"CaptchaImage\" src=\"{src}&r={Environment.TickCount64}\" alt=\"captcha\" />");
            sb.Append($"<a href=\"#\" onclick=\"document.getElementById('CaptchaImage').src='{src}&r='+Date.now();return false;\">{refresh}</a>");
            sb.Append($"<br/>{input}<br/>");
            sb.Append($"<input id=\"{InputName}\" name=\"{InputName}\" type=\"text\" autocomplete=\"off\" data-val=\"true\" data-val-required=\"{required}\" />");
            if (addValidationSpan)
                sb.Append($"<span class=\"field-validation-valid\" data-valmsg-for=\"{InputName}\" data-valmsg-replace=\"true\"></span>");
            return new HtmlString(sb.ToString());
        }

        public static bool IsCaptchaValid(this Controller controller, string errorText)
        {
            var session = SessionStore.For(controller.HttpContext);
            var expected = session[SessionKey] as string;
            session.Remove(SessionKey); // one attempt per image
            var actual = controller.Request.HasFormContentType ? controller.Request.Form[InputName].ToString() : null;

            var ok = !string.IsNullOrEmpty(expected) && !string.IsNullOrWhiteSpace(actual)
                     && string.Equals(expected, actual.Trim(), StringComparison.OrdinalIgnoreCase);
            if (!ok) controller.ModelState.AddModelError(InputName, errorText);
            return ok;
        }

        internal static string NewCode(int length)
        {
            length = Math.Clamp(length, 3, 8);
            var chars = new char[length];
            for (var i = 0; i < length; i++) chars[i] = Alphabet[RandomNumberGenerator.GetInt32(Alphabet.Length)];
            return new string(chars);
        }

        /// <summary>Raster (PNG) image so the code is not readable from the markup.</summary>
        [System.Runtime.Versioning.SupportedOSPlatform("windows")]
        internal static byte[] RenderPng(string code)
        {
            const int w = 140, h = 46;
            int R(int max) => RandomNumberGenerator.GetInt32(max);
            using var bmp = new System.Drawing.Bitmap(w, h);
            using (var g = System.Drawing.Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(System.Drawing.Color.FromArgb(242, 242, 242));
                for (var i = 0; i < 6; i++)
                {
                    using var pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(R(170), R(170), R(170)), 1.5f);
                    g.DrawLine(pen, R(w), R(h), R(w), R(h));
                }
                var step = (w - 20) / code.Length;
                using var font = new System.Drawing.Font("Verdana", 18, System.Drawing.FontStyle.Bold);
                for (var i = 0; i < code.Length; i++)
                {
                    var state = g.Save();
                    g.TranslateTransform(12 + i * step + R(6), 8 + R(10));
                    g.RotateTransform(R(40) - 20);
                    using var brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(R(100), R(100), R(100)));
                    g.DrawString(code[i].ToString(), font, brush, 0, 0);
                    g.Restore(state);
                }
                for (var i = 0; i < 40; i++)
                    bmp.SetPixel(R(w), R(h), System.Drawing.Color.Gray);
            }
            using var ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
    }

    public class CaptchaController : SalinoController
    {
        [HttpGet]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult Image(int l = 4)
        {
            var code = CaptchaHelper.NewCode(l);
            Session[CaptchaHelper.SessionKey] = code;
            return File(CaptchaHelper.RenderPng(code), "image/png");
        }
    }
}
