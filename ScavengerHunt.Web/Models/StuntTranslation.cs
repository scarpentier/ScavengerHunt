using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace ScavengerHunt.Web.Models
{
    public class StuntTranslation
    {
        [JsonIgnore]
        public int Id { get; set; } // TODO: Should be composite key with Stunt and Language
        
        [JsonIgnore]
        public virtual Stunt Stunt { get; set; }
        public string Language { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public StuntTranslation()
        {
            Language = "en";
        }
    }
}