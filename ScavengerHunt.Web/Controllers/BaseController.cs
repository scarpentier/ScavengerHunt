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
            
            if (Request.Cookies["culture"] == null)
            {
                //create cookie if it doesn't exist
                Response.Cookies["culture"].Value = "fr";
            }
         
            //eat the cookie
            Language = Request.Cookies["culture"].Value.ToString();

            if (String.IsNullOrEmpty(Language))
            {
                //set to english if cookie was not created
                Language = "fr";
            }
            ViewBag.language = Language;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(Language);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Language);

            Settings = StrongSettings.GetSettings(db.Settings.ToList());
            ViewBag.settings = Settings;
        }
    }
}