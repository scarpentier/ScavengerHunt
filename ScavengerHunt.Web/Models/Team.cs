using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;

using Newtonsoft.Json;

namespace ScavengerHunt.Web.Models
{
    public class Team
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string Tagline { get; set; }
        public string Url { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "BonusPoints")]
        public int BonusPoints { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<ApplicationUser> Members { get; set; }

        [JsonIgnore]
        public virtual ApplicationUser ContactUser { get; set; }

        [JsonIgnore]
        public virtual ICollection<TeamStunt> TeamStunts { get; set; }

        // TODO: Team URL

        // TODO: Team Logo

        [JsonIgnore]
        public virtual int Score
        {
            get
            {
                return (this.TeamStunts == null ? 0 : this.TeamStunts.Sum(x => x.Score)) + this.BonusPoints;
            }
        }

        [Display(ResourceType = typeof(Resources.Resources), Name = "Stunts")]
        [JsonIgnore]
        public virtual int StuntCount
        {
            get
            {
                return this.TeamStunts == null ? 0 : this.TeamStunts.Count(x => x.Status == TeamStuntStatusEnum.Done);
            }
        }
    }
}