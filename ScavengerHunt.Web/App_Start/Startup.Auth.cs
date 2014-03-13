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
            var microsoftAppId = ConfigurationManager.AppSettings["MicrosoftAppId"];
            var microsoftAppSecret = ConfigurationManager.AppSettings["MicrosoftAppSecret"];
            if (microsoftAppId != null && microsoftAppSecret != null)
                app.UseMicrosoftAccountAuthentication(microsoftAppId, microsoftAppSecret);

            var twitterAppId = ConfigurationManager.AppSettings["TwitterAppId"];
            var twitterAppSecret = ConfigurationManager.AppSettings["TwitterAppSecret"];
            if (twitterAppId != null && twitterAppSecret != null)
                app.UseTwitterAuthentication(twitterAppId, twitterAppSecret);

            var facebookAppId = ConfigurationManager.AppSettings["FacebookAppId"];
            var facebookAppSecret = ConfigurationManager.AppSettings["FacebookAppSecret"];
            if (facebookAppId != null && facebookAppSecret != null)
                app.UseFacebookAuthentication(facebookAppId, facebookAppSecret);

            app.UseGoogleAuthentication();
        }
    }
}