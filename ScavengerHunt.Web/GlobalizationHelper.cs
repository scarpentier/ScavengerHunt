using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Ajax.Utilities;

using ScavengerHunt.Web.Models;

namespace ScavengerHunt.Web
{
    public static class GlobalizationHelper
    {
        public static Stunt Globalize(this Stunt stunt, string language)
        {
            var translation = stunt.Translations.SingleOrDefault(x => language.StartsWith(x.Language));

            // Fallback to English
            if (translation == null) translation = stunt.Translations.First(x => x.Language == "en");

            stunt.Title = translation.Title;
            stunt.Description = translation.Description;
            return stunt;
        }

        public static TeamStunt Globalize(this TeamStunt teamStunt, string language)
        {
            teamStunt.Stunt = teamStunt.Stunt.Globalize(language);
            return teamStunt;
        }

        public static ICollection<Stunt> Globalize(this ICollection<Stunt> stunts, string language)
        {
            foreach (var stunt in stunts)
            {
                stunt.Globalize(language);
            }
            return stunts;
        }

        public static ICollection<TeamStunt> Globalize(this ICollection<TeamStunt> teamStunts, string language)
        {
            foreach (var teamStunt in teamStunts)
            {
                teamStunt.Globalize(language);
            }
            return teamStunts;
        }
    }
}