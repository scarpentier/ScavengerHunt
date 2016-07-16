using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ScavengerHunt.Web;

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
        public string ScavengerHuntTitle { get; private set; }
        public string ScavengerHuntTagline { get; private set; }
        public bool DisplayCurrentRankings { get; private set; }
        public bool DisplayStunts { get; private set; }

        public static StrongSettings GetSettings(List<Setting> settings)
        {
            Setting defaultEmptySetting = new Setting();
            var ss = new StrongSettings()
            {
                ShowKeyword = bool.Parse(settings.FindOrDefault(x => x.Key == "ShowKeyword", defaultEmptySetting).Value ?? bool.TrueString),
                ShowTitle = bool.Parse(settings.FindOrDefault(x => x.Key == "ShowTitle", defaultEmptySetting).Value ?? bool.TrueString),
                AllowStuntRetry = bool.Parse(settings.FindOrDefault(x => x.Key == "AllowStuntRetry", defaultEmptySetting).Value ?? bool.TrueString),
                EnableUserRegistration = bool.Parse(settings.FindOrDefault(x => x.Key == "EnableUserRegistration", defaultEmptySetting).Value ?? bool.TrueString),
                EnableTeamRegistration = bool.Parse(settings.FindOrDefault(x => x.Key == "EnableTeamRegistration", defaultEmptySetting).Value ?? bool.TrueString),
                EnableTeamJoining = bool.Parse(settings.FindOrDefault(x => x.Key == "EnabledTeamJoining", defaultEmptySetting).Value ?? bool.TrueString),
                GuestStuntsVisible = bool.Parse(settings.FindOrDefault(x => x.Key == "GuestStuntsVisible", defaultEmptySetting).Value ?? bool.TrueString),
                GuestTeamsVisible = bool.Parse(settings.FindOrDefault(x => x.Key == "GuestTeamsVisible", defaultEmptySetting).Value ?? bool.TrueString),
                GuestSummaryVisible = bool.Parse(settings.FindOrDefault(x => x.Key == "GuestSummaryVisible", defaultEmptySetting).Value ?? bool.TrueString),
                ScavengerHuntTitle = settings.FindOrDefault(x => x.Key == "ScavengerHuntTitle", defaultEmptySetting).Value,
                ScavengerHuntTagline = settings.FindOrDefault(x => x.Key == "ScavengerHuntTagline", defaultEmptySetting).Value,
                DisplayCurrentRankings = bool.Parse(settings.FindOrDefault(x => x.Key == "DisplayCurrentRankings", defaultEmptySetting).Value ?? bool.TrueString),
                DisplayStunts = bool.Parse(settings.FindOrDefault(x => x.Key == "DisplayStunts", defaultEmptySetting).Value ?? bool.TrueString)
            };
            return ss;
        }
    }
}