using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using ScavengerHunt.Web.Models;

using WebGrease.Css.Extensions;

namespace ScavengerHunt.Web.Controllers
{
    public class TeamStuntController : BaseController
    {
        // GET: /TeamStunt/
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Index", "Stunt");

            // Get current user
            string currentUserId = User.Identity.GetUserId();
            var user = db.Users.Find(currentUserId);

            if (user.Team == null)
            {
                return RedirectToAction("Index", "Stunt");
            }

            // Filter and sort stunts
            var stunts =
                db.TeamStunts.Where(x => x.Team.Id == user.Team.Id && x.Stunt.Published)
                    .OrderByDescending(x => x.Status == TeamStuntStatusEnum.Pending)
                    .ThenByDescending(x => x.Status == TeamStuntStatusEnum.WorkInProgress)
                    .ThenByDescending(x => x.Status == TeamStuntStatusEnum.NotStarted)
                    .ThenByDescending(x => x.Status == TeamStuntStatusEnum.Abandon);

            return View(stunts.ToList().Globalize(Language));
        }

        public ActionResult ActivityPartial()
        {
            return PartialView(db.TeamStunts.ToList().Globalize(Language));
        }

        // GET: /TeamStunt/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TeamStunt teamstunt = db.TeamStunts.Find(id);
            if (teamstunt == null) return HttpNotFound();

            // Make sure the current user is registered, part of a team and have access to this stunt
            var userid = User.Identity.GetUserId();
            var user = db.Users.Find(userid);

            if (user == null) return RedirectToAction("Login", "Account");
            if (user.Team == null) return RedirectToAction("Start", "Team");
            if (user.Team.TeamStunts.All(x => x.Id != id)) return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            return View(teamstunt.Globalize(Language));
        }

        // POST: /TeamStunt/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,TeamNotes,Submission,Status")] TeamStunt teamstunt)
        {
            if (ModelState.IsValid)
            {
                if (!User.IsInRole("Admin"))
                {
                    ModelState.AddModelError("Submission", "The scavenger hunt is now closed. Sorry");
                    return View(teamStunt.Globalize(Language));
                }

                // Get previous stunt object
                var teamStunt = db.TeamStunts.Find(teamstunt.Id);

                teamStunt.TeamNotes = teamstunt.TeamNotes;
                teamStunt.Submission = teamstunt.Submission;
                teamStunt.Status = teamstunt.Status;
                teamStunt.DateUpdated = DateTime.Now;

                // Special logic if it's a flag
                if (teamStunt.Stunt.Type == StuntTypeEnum.Flag && !string.IsNullOrEmpty(teamStunt.Stunt.JudgeNotes) && !string.IsNullOrEmpty(teamStunt.Submission))
                {
                    if (teamstunt.Submission.ToLower() == teamStunt.Stunt.JudgeNotes.ToLower())
                    {
                        teamStunt.Score = teamStunt.Stunt.MaxScore;
                        teamStunt.Status = TeamStuntStatusEnum.Done;
                    }
                    else
                    {
                        // Store the amount of failed tries
                        int tries;
                        int.TryParse(teamStunt.JudgeNotes, out tries);

                        teamStunt.JudgeNotes = (++tries).ToString();
                        db.Entry(teamStunt);
                        db.SaveChanges();
                        
                        ModelState.AddModelError("Submission", "Wrong flag");
                        return View(teamStunt.Globalize(Language));
                    }
                }

                db.Entry(teamStunt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teamstunt.Globalize(Language));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
