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
        public bool Enabled { get; set; }

        public virtual ICollection<TeamStunt> TeamStunts { get; set; }

        public Stunt()
        {
            Enabled = true;
        }

        // TODO: Support pour attacher des fichiers aux stunts
    }

    public enum StuntTypeEnum // TODO: À définir les types et ce que ça implique dans le système
    {
        /// <summary>
        /// Automatically corrected by the system according to provided answer
        /// </summary>
        Flag,
        Text,
        RichText,
        Photo,
        Video,
        Live
    }
}