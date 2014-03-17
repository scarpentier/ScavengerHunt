using System.Data.Entity;
using System.Net;
using System.Web.Mvc;

using ScavengerHunt.Web.Models;

namespace ScavengerHunt.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StuntTranslationController : BaseController
    {
        public ActionResult IndexPartial(int? id)
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

            return PartialView(stunt);
        }

        // GET: /StuntTranslation/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var stunt = db.Stunts.Find(id);
            if (stunt == null)
            {
                return HttpNotFound();
            }

            ViewBag.Stunt = stunt;

            return View();
        }

        // POST: /StuntTranslation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Language,Title,Description")] StuntTranslation stunttranslation, int id)
        {
            if (ModelState.IsValid)
            {
                // Get Stunt object
                var stunt = db.Stunts.Find(id);

                // Bind translation with stunt
                stunttranslation.Stunt = stunt;

                db.StuntTranslations.Add(stunttranslation);
                db.SaveChanges();
                return RedirectToAction("Edit", "Stunt", new { id = id });
            }

            return View(stunttranslation);
        }

        // GET: /StuntTranslation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StuntTranslation stunttranslation = db.StuntTranslations.Find(id);
            if (stunttranslation == null)
            {
                return HttpNotFound();
            }
            return View(stunttranslation);
        }

        // POST: /StuntTranslation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Language,Title,Description")] StuntTranslation stunttranslation)
        {
            if (ModelState.IsValid)
            {
                // Get full object
                var st = db.StuntTranslations.Find(stunttranslation.Id);
                st.Language = stunttranslation.Language;
                st.Title = stunttranslation.Title;
                st.Description = stunttranslation.Description;

                db.Entry(st).State = EntityState.Modified;
                db.SaveChanges();
               
                // Return to Stunt Edit Mode
                return RedirectToAction("Edit", "Stunt", new { id = st.Stunt.Id });
            }
            return View(stunttranslation);
        }

        // GET: /StuntTranslation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StuntTranslation stunttranslation = db.StuntTranslations.Find(id);
            if (stunttranslation == null)
            {
                return HttpNotFound();
            }
            return View(stunttranslation);
        }

        // POST: /StuntTranslation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StuntTranslation stunttranslation = db.StuntTranslations.Find(id);
            var stuntid = stunttranslation.Stunt.Id;
            db.StuntTranslations.Remove(stunttranslation);
            db.SaveChanges();
            return RedirectToAction("Edit", "Stunt", new { id = stuntid });
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
