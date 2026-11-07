using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Salino.DataLayer;

namespace SalinoMvc5.Compat
{
    /// <summary>
    /// Cookie-authentication replacement for System.Web.Security.FormsAuthentication.
    /// </summary>
    public static class FormsAuthentication
    {
        /// <summary>
        /// Same output as FormsAuthentication.HashPasswordForStoringInConfigFile(value, "MD5"):
        /// uppercase hex of MD5(UTF-8). Kept so existing user passwords keep working.
        /// </summary>
        public static string HashPasswordForStoringInConfigFile(string password, string format)
        {
            if (!string.Equals(format, "MD5", StringComparison.OrdinalIgnoreCase))
                throw new NotSupportedException("Only MD5 is used by this application.");
            return Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes(password)));
        }

        public static void SetAuthCookie(this Controller controller, string userName, bool createPersistentCookie)
        {
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userName) },
                CookieAuthenticationDefaults.AuthenticationScheme);
            controller.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties { IsPersistent = createPersistentCookie }).GetAwaiter().GetResult();
        }

        public static void SignOut(this Controller controller)
        {
            controller.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).GetAwaiter().GetResult();
        }
    }

    /// <summary>
    /// Replaces the custom RoleProvider: roles are read from the database on every request,
    /// exactly like SalinoMvc5.SalinoUtilities.Authentication.GetRolesForUser did.
    /// </summary>
    public sealed class DatabaseRoleClaimsTransformation : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity is not ClaimsIdentity { IsAuthenticated: true } identity
                || identity.HasClaim(c => c.Type == ClaimTypes.Role))
                return Task.FromResult(principal);

            using var db = new SalinoContext();
            var roles = (from u in db.users
                         join r in db.roles on u.RoleId equals r.Id
                         where u.Mobile == identity.Name
                         select r.RoleName).ToArray();

            var clone = new ClaimsIdentity(identity);
            foreach (var role in roles) clone.AddClaim(new Claim(ClaimTypes.Role, role));
            return Task.FromResult(new ClaimsPrincipal(clone));
        }
    }
}
