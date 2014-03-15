using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

using Newtonsoft.Json;

namespace ScavengerHunt.Web.Models
{
    public class Stunt
    {
        [JsonIgnore]
        public int Id { get; set; }
        
        public int MaxScore { get; set; }

        public string Keyword { get; set; }
        public StuntTypeEnum Type { get; set; }

        public bool Enabled { get; set; }
        public string JudgeNotes { get; set; }

        public virtual ICollection<StuntTranslation> Translations { get; set; }

        [JsonIgnore]
        public virtual ICollection<TeamStunt> TeamStunts { get; set; }

        [JsonIgnore]
        public virtual string Title { get; set; }

        [JsonIgnore]
        public virtual string Description { get; set; }

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
        Live,
        Url,
        File
    }
}