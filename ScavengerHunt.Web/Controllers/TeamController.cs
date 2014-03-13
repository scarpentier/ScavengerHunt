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
    public class TeamController : Controller
    {
        private ScavengerHuntContext db = new ScavengerHuntContext();

        // GET: /Team/
        public ActionResult Index()
        {
            return View(db.Teams.ToList());
        }

        // GET: /Team/Details/5
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

        // GET: /Team/Create
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            return View();
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

                return RedirectToAction("CreateDone", new { id = team.Id });
            }

            return View(team);
        }

        public ActionResult CreateDone(int? id)
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

        // GET: /Team/Join
        public ActionResult Join()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            return View();
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

            return this.Details(team.Id);
        }

        public ActionResult Leave()
        {
            // TODO: Implement
            throw new NotImplementedException();
        }

        // GET: /Team/Edit/5
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
        public ActionResult Edit([Bind(Include="Id,Name")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: /Team/Delete/5
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
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Index");
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
