using Hach.Library.Extensions;
using System.Web.Mvc;

namespace ParkingFinder.Controllers
{
    /// <summary>
    /// Cookie Controller to handle all functionality regarding Cookies
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class CookieController : Controller
    {
        
        /// <summary>
        /// Index Function
        /// </summary>
        /// <returns>Index view if cookie has to be shown, otherwise nothing</returns>
        public ActionResult Index()
        {
            return !this.CheckIfCookieIsSet(Labels.Keys.Cookie)
                ? this.PartialView()
                : null;
        }

        /// <summary>
        /// Checks if a specific cookie is set
        /// </summary>
        /// <param name="cookieKey">cookie key</param>
        /// <returns>true, if the cookie is set</returns>
        private bool CheckIfCookieIsSet(string cookieKey)
        {
            return Request.Cookies[cookieKey] != null && !Request.Cookies[cookieKey].Value.IsNullOrEmpty();
        }

        /// <summary>
        /// Static Labels
        /// </summary>
        public static class Labels
        {
            /// <summary>
            /// Keys
            /// </summary>
            public static class Keys
            {
                /// <summary>
                /// The Cookie Module Cookie Kes
                /// </summary>
                public const string Cookie = "CookieHint";
            }
        }
    }
}