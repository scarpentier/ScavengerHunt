using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace ScavengerHunt.Web.Models
{
    public class TeamStunt
    {
        public int Id { get; set; }
        public int Score { get; set; }

        public DateTime DateUpdated { get; set; }
        public string Submission { get; set; }
        
        /// <summary>
        /// Internal notes for the team
        /// </summary>
        [Display(ResourceType = typeof(Resources.Resources), Name = "TeamNotes")]
        public string TeamNotes { get; set; }

        /// <summary>
        /// Internal notes for the judges
        /// </summary>
        [Display(ResourceType = typeof(Resources.Resources), Name = "JudgeFeedback")]
        public string JudgeFeedback { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "JudgeNotes")]
        public string JudgeNotes { get; set; }

        // TODO: Comment system internal and shared between team / judges

        // TODO: Add support for stunt owner

        public TeamStuntStatusEnum Status { get; set; }
        public virtual Team Team { get; set; }
        public virtual Stunt Stunt { get; set; }

        public virtual bool Done
        {
            get
            {
                return Status == TeamStuntStatusEnum.Done || Score > 0;
            }
        }

        public TeamStunt()
        {
            Score = 0;
            Status = TeamStuntStatusEnum.NotStarted;
            DateUpdated = DateTime.Now;
        }
    }

    public enum TeamStuntStatusEnum
    {
        /// <summary>
        /// Not started by the team yet
        /// </summary>
        [Display(Name = "Not Started")]
        NotStarted,

        /// <summary>
        /// Currently being done by the team
        /// </summary>
        [Display(Name = "Work In Progress")]
        WorkInProgress,

        /// <summary>
        /// Pending judgement
        /// </summary>
        [Display(Name = "Ready for Judgement")]
        Pending,

        /// <summary>
        /// Judged
        /// </summary>
        [Display(Name = "Done")]
        Done,

        /// <summary>
        /// Abandoned; Will not be done the team; 
        /// </summary>
        [Display(Name = "Abandonned")]
        Abandon,

        [Display(Name = "Sent back by judges")]
        Incomplete
    }
}