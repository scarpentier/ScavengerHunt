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

namespace ScavengerHunt.Web.Controllers
{
    public class TeamStuntController : Controller
    {
        private ScavengerHuntContext db = new ScavengerHuntContext();

        // GET: /TeamStunt/
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Index", "Stunt");

            // Get current user
            string currentUserId = User.Identity.GetUserId();
            var user = db.Users.Find(currentUserId);

            if (user.Team == null)
            {
                ModelState.AddModelError("", "You must be part of a team first.");
                return RedirectToAction("Start", "Team");
            }

            // TODO: Make sure the user is part of a team

            return View(db.TeamStunts.ToList());
        }

        public ActionResult ActivityPartial()
        {
            return PartialView(db.TeamStunts.ToList());
        }

        // GET: /TeamStunt/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamStunt teamstunt = db.TeamStunts.Find(id);
            if (teamstunt == null)
            {
                return HttpNotFound();
            }
            return View(teamstunt);
        }

        // POST: /TeamStunt/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,NotesTeam,Submission,Status")] TeamStunt teamstunt)
        {
            if (ModelState.IsValid)
            {
                // Get previous stunt object
                var teamStunt = db.TeamStunts.Find(teamstunt.Id);

                teamStunt.NotesTeam = teamstunt.NotesTeam;
                teamStunt.Submission = teamstunt.Submission;
                teamStunt.Status = teamstunt.Status;
                teamStunt.DateUpdated = DateTime.Now;

                db.Entry(teamStunt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teamstunt);
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
