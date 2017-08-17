using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScavengerHunt.Web.Controllers
{
    public class CultureController : BaseController
    {
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SetCulture()
        {
            HttpCookie aCookie = new HttpCookie("culture");

            if (aCookie.Value == "en")
            {
                aCookie.Value = "fr";
            }
            else if (aCookie.Value == "fr")
            {
                aCookie.Value = "en";
            }
            //default back to english if something went horribly wrong
            else
            {
                aCookie.Value = "en";
            }
            return RedirectToAction("Index", "Home");
        }
    }
}