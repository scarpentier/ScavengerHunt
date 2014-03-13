using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScavengerHunt.Web.Models
{
    public class Stunt
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxScore { get; set; }
        public StuntTypeEnum Type { get; set; }

        public virtual ICollection<TeamStunt> TeamStunts { get; set; } 

        // TODO: Support pour attacher des fichiers aux stunts
    }

    public enum StuntTypeEnum
    {
        Flag,
        Text,
        RichText,
        Url,
        Live
    }
}