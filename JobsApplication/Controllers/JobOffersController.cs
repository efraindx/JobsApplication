using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.ServiceModel.Syndication;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobsApplication.Models;

namespace JobsApplication
{
    public class JobOffersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: JobOffers
        public ActionResult Index()
        {
            var jobOffers = db.JobOffers.Include(j => j.Category).Include(j => j.JobType);
            return View(jobOffers.ToList());
        }

        // GET: JobOffers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobOffer jobOffer = db.JobOffers.Find(id);
            if (jobOffer == null)
            {
                return HttpNotFound();
            }
            return View(jobOffer);
        }

        // GET: JobOffers/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.JobTypeId = new SelectList(db.JobTypes, "Id", "Name");
            return View();
        }

        // Generate RSS
        public ActionResult PostFeed()
        {
            var jobOffers = db.JobOffers.Select(jo => new SyndicationItem(jo.Company, jo.Description, new Uri(jo.URL)));
            var feed = new SyndicationFeed("Job Offers", "List of Job Offers", new Uri("http://localhost:49292/JobOffers"), jobOffers)
            {
                Copyright = TextSyndicationContent.CreatePlaintextContent("@JobOffers"),
                Language = "en-US"
            };

            return new FeedResult(feed);
        }

        // POST: JobOffers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryId,JobTypeId,Company,Logo,URL,Position,Location,Description")] JobOffer jobOffer)
        {
            if (ModelState.IsValid)
            {
                db.JobOffers.Add(jobOffer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", jobOffer.CategoryId);
            ViewBag.JobTypeId = new SelectList(db.JobTypes, "Id", "Name", jobOffer.JobTypeId);
            return View(jobOffer);
        }

        // GET: JobOffers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobOffer jobOffer = db.JobOffers.Find(id);
            if (jobOffer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", jobOffer.CategoryId);
            ViewBag.JobTypeId = new SelectList(db.JobTypes, "Id", "Name", jobOffer.JobTypeId);
            return View(jobOffer);
        }

        public ActionResult APIs()
        {
            return View();
        }

        // POST: JobOffers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,JobTypeId,Company,Logo,URL,Position,Location,Description")] JobOffer jobOffer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobOffer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", jobOffer.CategoryId);
            ViewBag.JobTypeId = new SelectList(db.JobTypes, "Id", "Name", jobOffer.JobTypeId);
            return View(jobOffer);
        }

        // GET: JobOffers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobOffer jobOffer = db.JobOffers.Find(id);
            if (jobOffer == null)
            {
                return HttpNotFound();
            }
            return View(jobOffer);
        }

        // POST: JobOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobOffer jobOffer = db.JobOffers.Find(id);
            db.JobOffers.Remove(jobOffer);
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
