using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Routing;

namespace SalinoMvc5.Compat
{
    /// <summary>
    /// Replacement for Rotativa.MVC's ActionAsPdf: wkhtmltopdf requests the given action over HTTP
    /// (forwarding the caller's cookies, so auth and session carry over) and the PDF is returned.
    /// The wkhtmltopdf binaries live in &lt;content root&gt;/Rotativa.
    /// </summary>
    public class ActionAsPdf : ActionResult
    {
        private readonly string _action;
        private readonly object _routeValues;

        public ActionAsPdf(string action, object routeValues)
        {
            _action = action;
            _routeValues = routeValues;
        }

        public string FileName { get; set; }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            var http = context.HttpContext;
            var env = http.RequestServices.GetRequiredService<IWebHostEnvironment>();
            var url = http.RequestServices.GetRequiredService<IUrlHelperFactory>().GetUrlHelper(context)
                .Action(_action, null, _routeValues, http.Request.Scheme, http.Request.Host.ToString());

            var exe = Path.Combine(env.ContentRootPath, "Rotativa", "wkhtmltopdf.exe");
            var psi = new ProcessStartInfo(exe)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            psi.ArgumentList.Add("-q");
            foreach (var cookie in http.Request.Cookies)
            {
                psi.ArgumentList.Add("--cookie");
                psi.ArgumentList.Add(cookie.Key);
                psi.ArgumentList.Add(Uri.EscapeDataString(cookie.Value));
            }
            psi.ArgumentList.Add(url);
            psi.ArgumentList.Add("-");

            using var process = Process.Start(psi)!;
            using var pdf = new MemoryStream();
            var copy = process.StandardOutput.BaseStream.CopyToAsync(pdf);
            var errors = process.StandardError.ReadToEndAsync();
            await Task.WhenAll(copy, errors, process.WaitForExitAsync());

            if (pdf.Length == 0)
                throw new InvalidOperationException("wkhtmltopdf produced no output: " + await errors);

            http.Response.ContentType = "application/pdf";
            if (!string.IsNullOrEmpty(FileName))
                http.Response.Headers.ContentDisposition = $"attachment; filename=\"{FileName}\"";
            pdf.Position = 0;
            await pdf.CopyToAsync(http.Response.Body);
        }
    }
}
