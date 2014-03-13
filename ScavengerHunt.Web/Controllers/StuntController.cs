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
    [Authorize(Roles="Admin")]
    public class StuntController : Controller
    {
        private ScavengerHuntContext db = new ScavengerHuntContext();

        // GET: /Stunt/
        public ActionResult Index()
        {
            return View(db.Stunts.ToList());
        }

        // GET: /Stunt/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stunt stunt = db.Stunts.Find(id);
            if (stunt == null)
            {
                return HttpNotFound();
            }
            return View(stunt);
        }

        // GET: /Stunt/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Stunt/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Title,Description,MaxScore,Type")] Stunt stunt)
        {
            if (ModelState.IsValid)
            {
                db.Stunts.Add(stunt);
                db.SaveChanges();

                // TODO: Il faut aussi l'ajouter/assigner aux équipes déjà inscrites

                return RedirectToAction("Index");
            }

            return View(stunt);
        }

        // GET: /Stunt/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stunt stunt = db.Stunts.Find(id);
            if (stunt == null)
            {
                return HttpNotFound();
            }
            return View(stunt);
        }

        // POST: /Stunt/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Title,Description,MaxScore,Type")] Stunt stunt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stunt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stunt);
        }

        // GET: /Stunt/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stunt stunt = db.Stunts.Find(id);
            if (stunt == null)
            {
                return HttpNotFound();
            }
            return View(stunt);
        }

        // POST: /Stunt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stunt stunt = db.Stunts.Find(id);
            db.Stunts.Remove(stunt);
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
