using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ScavengerHunt.Web.Models
{
    public class Setting
    {
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }

    public class StrongSettings
    {
        public bool ShowKeyword { get; private set; }
        public bool AllowStuntRetry { get; private set; }
        public bool ShowTitle { get; private set; }
        public bool EnableUserRegistration { get; private set; }
        public bool EnableTeamRegistration { get; private set; }
        public bool EnableTeamJoining { get; private set; }
        public bool GuestStuntsVisible { get; private set; }
        public bool GuestTeamsVisible { get; private set; }
        public bool GuestSummaryVisible { get; private set; }
        public string GuideUrl { get; set; }

        public static StrongSettings GetSettings(List<Setting> settings)
        {
            var ss = new StrongSettings()
                         {
                             ShowKeyword = bool.Parse(settings.Find(x => x.Key == "ShowKeyword").Value ?? bool.TrueString),
                             ShowTitle = bool.Parse(settings.Find(x => x.Key == "ShowTitle").Value ?? bool.TrueString),
                             AllowStuntRetry = bool.Parse(settings.Find(x => x.Key == "AllowStuntRetry").Value ?? bool.TrueString),
                             EnableUserRegistration = bool.Parse(settings.Find(x => x.Key == "EnableUserRegistration").Value ?? bool.TrueString),
                             EnableTeamRegistration = bool.Parse(settings.Find(x => x.Key == "EnableTeamRegistration").Value ?? bool.TrueString),
                             EnableTeamJoining = bool.Parse(settings.Find(x => x.Key == "EnabledTeamJoining").Value ?? bool.TrueString),
                             GuestStuntsVisible = bool.Parse(settings.Find(x => x.Key == "GuestStuntsVisible").Value ?? bool.TrueString),
                             GuestTeamsVisible = bool.Parse(settings.Find(x => x.Key == "GuestTeamsVisible").Value ?? bool.TrueString),
                             GuestSummaryVisible = bool.Parse(settings.Find(x => x.Key == "GuestSummaryVisible").Value ?? bool.TrueString),
                             GuideUrl = settings.Find(x => x.Key == "GuideUrl").Value
            };

            return ss;
        }
    }
}