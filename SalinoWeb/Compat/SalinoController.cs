using System.Net;
using Microsoft.AspNetCore.Hosting;

namespace SalinoMvc5.Compat
{
    /// <summary>
    /// Base controller exposing the System.Web.Mvc members the ported code relies on.
    /// </summary>
    public abstract class SalinoController : Controller
    {
        private SessionStore _session;
        private ServerUtility _server;

        public SessionStore Session => _session ??= SessionStore.For(HttpContext);

        public ServerUtility Server => _server ??= new ServerUtility(HttpContext);

        protected NotFoundResult HttpNotFound() => NotFound();

        protected NotFoundObjectResult HttpNotFound(string statusDescription) => NotFound(statusDescription);
    }

    /// <summary>Replacement for System.Web.Mvc.HttpStatusCodeResult.</summary>
    public class HttpStatusCodeResult : StatusCodeResult
    {
        public HttpStatusCodeResult(HttpStatusCode statusCode) : base((int)statusCode) { }
        public HttpStatusCodeResult(int statusCode) : base(statusCode) { }
        public HttpStatusCodeResult(HttpStatusCode statusCode, string statusDescription) : base((int)statusCode) { }
    }

    /// <summary>Replacement for HttpServerUtility.MapPath; "~/" maps to wwwroot.</summary>
    public sealed class ServerUtility
    {
        private readonly HttpContext _context;

        public ServerUtility(HttpContext context) => _context = context;

        public string MapPath(string virtualPath) => MapWebPath(_context.RequestServices.GetRequiredService<IWebHostEnvironment>(), virtualPath);

        public static string MapWebPath(IWebHostEnvironment env, string virtualPath)
        {
            var relative = (virtualPath ?? string.Empty).TrimStart('~').TrimStart('/', '\\')
                .Replace('/', Path.DirectorySeparatorChar);
            var root = Path.GetFullPath(env.WebRootPath);
            var full = Path.GetFullPath(Path.Combine(root, relative));
            if (!full.StartsWith(root, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Path escapes the web root: " + virtualPath);
            return full;
        }
    }
}
