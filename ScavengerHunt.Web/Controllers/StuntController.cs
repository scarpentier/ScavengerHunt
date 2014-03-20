using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

using Newtonsoft.Json;

using ScavengerHunt.Web.Models;

namespace ScavengerHunt.Web.Controllers
{
    public class StuntController : BaseController
    {
        // GET: /Stunt/
        [Authorize(Roles = "Admin")]
        public ActionResult IndexAdmin()
        {
            return View(db.Stunts.OrderBy(x => x.Keyword).ToList().Globalize(Language));
        }

        public ActionResult Index()
        {
            return View(db.Stunts.Where(x => x.Published).ToList().Globalize(Language));
        }

        // GET: /Stunt/Details/5
        [Authorize(Roles = "Admin")]
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
            return View(stunt.Globalize(Language));
        }

        // GET: /Stunt/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Stunt/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include="Id,Keyword,MaxScore,Type,JudgeNotes,Published,Collapsible")] Stunt stunt)
        {
            if (ModelState.IsValid)
            {
                // Créer une traduction par défaut et rediriger
                var st = new StuntTranslation();
                stunt.Translations = new Collection<StuntTranslation>() { st };

                db.Stunts.Add(stunt);
                db.SaveChanges();

                // Il faut aussi l'ajouter/assigner aux équipes déjà inscrites
                foreach (var team in db.Teams)
                {
                    var ts = new TeamStunt() { Stunt = stunt, Team = team };
                    db.TeamStunts.Add(ts);
                }

                db.SaveChanges();

                return RedirectToAction("Edit", "StuntTranslation", new { id = st.Id });
            }

            return View(stunt);
        }

        // GET: /Stunt/Edit/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Keyword,MaxScore,Type,JudgeNotes,Published,Collapsible")] Stunt stunt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stunt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexAdmin");
            }
            return View(stunt);
        }

        // GET: /Stunt/Delete/5
        [Authorize(Roles = "Admin")]
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
            return View(stunt.Globalize(Language));
        }

        // POST: /Stunt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Stunt stunt = db.Stunts.Find(id);
            db.Stunts.Remove(stunt);
            db.SaveChanges();
            return RedirectToAction("IndexAdmin");
        }

        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteAll()
        {
            db.Stunts.RemoveRange(db.Stunts);
            db.SaveChanges();
            return View("Data");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Data()
        {
            return View();
        }

        public ActionResult DataExportPartial()
        {
            return PartialView();
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DataExport()
        {
            var stunts = db.Stunts.ToList();

            ViewBag.ExportData = JsonConvert.SerializeObject(stunts);

            return View("Data");
        }

        public ActionResult DataImportPartial()
        {
            return PartialView();
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DataImport(string data)
        {
            var d = JsonConvert.DeserializeObject<List<Stunt>>(data);

            db.Stunts.AddRange(d);
            db.SaveChanges();

            // Create TeamStunts for the teams
            foreach (var stunt in d)
            {
                foreach (var team in db.Teams)
                {
                    var ts = new TeamStunt() { Stunt = stunt, Team = team };
                    db.TeamStunts.Add(ts);
                }
            }
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
