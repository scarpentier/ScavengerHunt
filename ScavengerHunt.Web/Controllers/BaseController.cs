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

            Language = Request.UserLanguages == null ? "en" : Request.UserLanguages[0];
            ViewBag.language = Language;

            Thread.CurrentThread.CurrentCulture = new CultureInfo(Language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Language);

            Settings = StrongSettings.GetSettings(db.Settings.ToList());
            ViewBag.settings = Settings;
        }
    }
}