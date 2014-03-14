using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScavengerHunt.Web.Models
{
    public class StuntTranslation
    {
        public int Id { get; set; } // TODO: Should be composite key with Stunt and Language
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