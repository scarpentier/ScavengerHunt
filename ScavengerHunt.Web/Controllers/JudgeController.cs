using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ScavengerHunt.Web.Models;

namespace ScavengerHunt.Web.Controllers
{
    public class JudgeController : Controller
    {
        private ScavengerHuntContext db = new ScavengerHuntContext();

        // GET: /Judge/
        public ActionResult Index()
        {
            return View(db.TeamStunts.ToList());
        }


        // GET: /Judge/Edit/5
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

        // POST: /Judge/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Score,DateUpdated,Submission,NotesTeam,NotesJudges,Status")] TeamStunt teamstunt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamstunt).State = EntityState.Modified;
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
