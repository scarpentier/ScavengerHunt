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

        public static StrongSettings GetSettings(List<Setting> settings)
        {
            var ss = new StrongSettings()
                         {
                             ShowKeyword = bool.Parse(settings.Find(x => x.Key == "ShowKeyword").Value),
                             ShowTitle = bool.Parse(settings.Find(x => x.Key == "ShowTitle").Value),
                             AllowStuntRetry = bool.Parse(settings.Find(x => x.Key == "AllowStuntRetry").Value)
                         };

            return ss;
        }
    }
}