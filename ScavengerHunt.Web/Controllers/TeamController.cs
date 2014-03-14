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
    public class TeamController : BaseController
    {
        private ScavengerHuntContext db = new ScavengerHuntContext();

        // GET: /Team/
        [Authorize(Roles="Admin")]
        public ActionResult IndexAdmin()
        {
            return View(db.Teams.ToList().OrderByDescending(x => x.Score));
        }

        public ActionResult Index()
        {
            Team team = null;

            // Get current user
            var userid = User.Identity.GetUserId();
            var user = db.Users.Find(userid); // Might be null

            // Make sure user is authenticated and has a team
            if (user != null && user.Team != null)
            {
                team = user.Team;
            }

            return View(team);
        }

        public ActionResult IndexPartial()
        {
            return PartialView(db.Teams.ToList().OrderByDescending(x => x.Score));
        }

        public ActionResult ShowToken(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return this.PartialView(team);
        }

        // GET: /Team/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        public ActionResult Start()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            return View();
        }

        // GET: /Team/Create
        public ActionResult CreatePartial()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            return PartialView();
        }

        // POST: /Team/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name")] Team team)
        {
            if (ModelState.IsValid)
            {              
                // Make sure the team is not already there
                if (db.Teams.FirstOrDefault(x => x.Name == team.Name) != null)
                {
                    ModelState.AddModelError("Name", "Team name already exists");
                    return View("Start", team);
                }

                // Add current user as leader and member
                string currentUserId = User.Identity.GetUserId();
                var currentUser = db.Users.Find(currentUserId);
                team.ContactUser = currentUser;
                team.Members = new List<ApplicationUser> { currentUser };

                // Copy stunts
                team.TeamStunts = new List<TeamStunt>();
                foreach (var stunt in db.Stunts)
                {
                    team.TeamStunts.Add(new TeamStunt() { Stunt = stunt, Status = TeamStuntStatusEnum.NotStarted});
                }
                
                // Generate password token
                team.Token = Guid.NewGuid().ToString();

                db.Teams.Add(team);
                db.SaveChanges();

                return RedirectToAction("CreateDone");
            }

            return PartialView("CreatePartial", team);
        }

        public ActionResult CreateDone()
        {
            // TODO: 3 things can go wrong here but we don't really care
            var userid = User.Identity.GetUserId();
            var user = db.Users.Find(userid);
            return View(user.Team);
        }

        // GET: /Team/Join
        public ActionResult JoinPartial()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            return PartialView();
        }

        // GET: /Team/Join/password
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Join([Bind(Include = "Token")] string token)
        {
            // Get User
            string currentUserId = User.Identity.GetUserId();
            var user = db.Users.Find(currentUserId);

            // TODO: Qu'est-ce qui arrive quand ce user est déjà membre d'une équipe?

            // Get Team
            var team = db.Teams.First(t => t.Token == token);

            // Add user to team and save changes
            team.Members.Add(user);
            db.SaveChanges();

            return RedirectToAction("Index", "TeamStunt");
        }

        public ActionResult Leave()
        {
            // TODO: Implement
            throw new NotImplementedException();
        }

        // GET: /Team/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: /Team/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include="Id,Name,BonusPoints")] Team team)
        {
            if (ModelState.IsValid)
            {
                var t = db.Teams.Find(team.Id);

                t.Name = team.Name;
                t.BonusPoints = team.BonusPoints;
                
                db.Entry(t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexAdmin");
            }
            return View(team);
        }

        // GET: /Team/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: /Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);

            // Kick everyone from that team
            // TODO: Do with CASCADE NULL instead
            team.Members.ForEach(x => x.Team = null);

            db.Teams.Remove(team);
            db.SaveChanges();
            return RedirectToAction("IndexAdmin");
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
