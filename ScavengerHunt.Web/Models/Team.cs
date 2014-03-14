using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;

namespace ScavengerHunt.Web.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "BonusPoints")]
        public int BonusPoints { get; set; }
        public virtual ICollection<ApplicationUser> Members { get; set; }
        public virtual ApplicationUser ContactUser { get; set; }
        public virtual ICollection<TeamStunt> TeamStunts { get; set; }

        // TODO: Team URL

        // TODO: Team Logo

        public virtual int Score
        {
            get
            {
                return (this.TeamStunts == null ? 0 : this.TeamStunts.Sum(x => x.Score)) + this.BonusPoints;
            }
        }

        [Display(ResourceType = typeof(Resources.Resources), Name = "Stunts")]
        public virtual int StuntCount
        {
            get
            {
                return this.TeamStunts == null ? 0 : this.TeamStunts.Count(x => x.Status == TeamStuntStatusEnum.Done);
            }
        }
    }
}