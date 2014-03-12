using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScavengerHunt.Web.Models
{
    public class TeamStunt
    {
        public int Id { get; set; }
        public int? Score { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string Submission { get; set; }
        public virtual Team Team { get; set; }
        public virtual Stunt Stunt { get; set; }
    }
}