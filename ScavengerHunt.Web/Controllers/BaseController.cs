using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

using Microsoft.Ajax.Utilities;

using ScavengerHunt.Web.Models;

namespace ScavengerHunt.Web.Controllers
{
    public class BaseController : Controller
    {
        protected ScavengerHuntContext db = new ScavengerHuntContext();

        protected string Language { get; private set; }

        protected StrongSettings Settings { get; private set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var cookieKeys = filterContext.RequestContext.HttpContext.Request.Cookies.AllKeys;

            if (cookieKeys.Contains("culture"))
            {
                //eat the cookie
                var theCultureCookie = filterContext.RequestContext.HttpContext.Request.Cookies["culture"];
                Language = theCultureCookie.Value;
                
            }
            else
            {
                Response.Cookies["culture"].Value = "en";
                //in case cookies are disabled, it will always default to english
                Language = "en";
            }

            ViewBag.language = Language;

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(Language);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Language);

            Settings = StrongSettings.GetSettings(db.Settings.ToList());
            ViewBag.settings = Settings;
        }
    }
}