using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AddressBook
{
    public static class Utilities
    {
        /// <summary>
        /// Gets  the current logged in user id
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public static Guid GetUserId()
        {
            var claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            return Guid.Parse(userIdClaim.Value);
        }
        /// <summary>
        /// Returns the current websites domain
        /// </summary>
        /// <returns></returns>
        public static string GetSiteDomain()
        {
            // Return variable declaration.
            var appPath = string.Empty;
            // Getting the current context of HTTP request.
            var context = HttpContext.Current;
            // Checking the current context content.
            if (context != null)
            {
                // Formatting the fully qualified website url/name.
                appPath = string.Format(
                    "{0}://{1}{2}",
                    context.Request.Url.Scheme,
                    context.Request.Url.Host,
                    context.Request.Url.Port == 80 ? string.Empty : ":" + context.Request.Url.Port
                );
            }
            if (!appPath.EndsWith("/", StringComparison.Ordinal))
                appPath += "/";
            return appPath;
        }

        public static string GetSiteRoot()
        {
            // Return variable declaration.
            var appPath = string.Empty;
            // Getting the current context of HTTP request.
            var context = HttpContext.Current;
            // Checking the current context content.
            if (context != null)
            {
                // Formatting the fully qualified website url/name.
                appPath = string.Format(
                    "{0}://{1}{2}{3}",
                    context.Request.Url.Scheme,
                    context.Request.Url.Host,
                    context.Request.Url.Port == 80 ? string.Empty : ":" + context.Request.Url.Port,
                    context.Request.ApplicationPath
                );
            }
            if (!appPath.EndsWith("/", StringComparison.Ordinal))
                appPath += "/";

            return appPath;
        }

        public static string Base64Encoding(string value)
        {
            if (value == null) return null;
            var bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        public static string Base64Decoding(string value)
        {
            if (value == null) return null;
            try
            {
                var bytes = Convert.FromBase64String(value);
                return Encoding.UTF8.GetString(bytes);
            }
            catch (Exception ex)
            {
                ex.Log();
                return value;
            }
        }
    }
}

namespace System.Linq
{
    public static class Extensions
    {

        /// <summary> Default side-effect style enumeration </summary>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var element in source)
                action(element);
        }
        /// <summary>
        /// Pages IEnumerable data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="en"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IEnumerable<T> Page<T>(this IEnumerable<T> en, int pageSize, int page)
        {
            return en.Skip(page * pageSize).Take(pageSize);
        }
        /// <summary>
        /// Pages IQueriable data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="en"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IQueryable<T> Page<T>(this IQueryable<T> en, int pageSize, int page)
        {
            return en.Skip(page * pageSize).Take(pageSize);
        }
    }
}