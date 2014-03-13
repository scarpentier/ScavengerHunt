using System.Configuration;

using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace ScavengerHunt.Web
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // 3rd party providers
            var microsoftSecret = ConfigurationManager.AppSettings["MicrosoftSecret"];
            if (microsoftSecret != null)
                app.UseMicrosoftAccountAuthentication("000000004011751A", microsoftSecret);

            var twitterSecret = ConfigurationManager.AppSettings["TwitterSecret"];
            if (twitterSecret != null)
                app.UseTwitterAuthentication("e86J3077crgzh8Cqmq5TQ", twitterSecret);

            var facebookSecret = ConfigurationManager.AppSettings["FacebookSecret"];
            if (facebookSecret != null)
                app.UseFacebookAuthentication("1399907303609567", facebookSecret);

            app.UseGoogleAuthentication();
        }
    }
}