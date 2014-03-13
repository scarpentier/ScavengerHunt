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
        [Display(Name = "Notes", Description = "Internal notes for use by your team")]
        public string NotesTeam { get; set; }

        /// <summary>
        /// Internal notes for the judges
        /// </summary>
        [Display(Name = "Judges's notes", Description = "Internal notes only visible for the judges")]
        public string NotesJudges { get; set; }

        // TODO: Add support for stunt owner

        public TeamStuntStatusEnum Status { get; set; }
        public virtual Team Team { get; set; }
        public virtual Stunt Stunt { get; set; }

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
        Abandon
    }
}