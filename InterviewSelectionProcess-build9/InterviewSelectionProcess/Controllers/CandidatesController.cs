using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Interview.Domain.Model;
using System.IO;
using PagedList;

namespace InterviewSelectionProcess.Controllers
{
    public class CandidatesController : Controller
    {
        private InterviewSelectionProcessContext db = new InterviewSelectionProcessContext();

        // GET: Candidates
        public ActionResult Index(string SearchPost, string SearchTech, int? SearchExperience, int page=1)
        {
            var candidates = db.Candidates.Include(c => c.Post).Include(c => c.Technology).OrderBy(x=>x.CandidateID);
            candidates = candidates.Where(s => (s.Post.PostName.Contains(SearchPost) || String.IsNullOrEmpty(SearchPost)) && (s.Technology.Technology1.Contains(SearchTech) || String.IsNullOrEmpty(SearchTech)) && (s.Experience == SearchExperience || SearchExperience == null)).OrderBy(x => x.CandidateID);
            return View(candidates.ToPagedList(page, 10));
        }

        // GET: Candidates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // GET: Candidates/Create
        public ActionResult Create()
        {
            ViewBag.PostID = new SelectList(db.Posts, "PostID", "PostName");
            ViewBag.TechnologiesWorkedOnID = new SelectList(db.Technologies, "TechID", "Technology1");
            return View();
        }

        // POST: Candidates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CandidateID,FirstName,MiddleName,LastName,DateOfBirth,Address,Experience,TechnologiesWorkedOnID,PostID,MobileNumber,EmailAddress")] Candidate candidate, HttpPostedFileBase file)
        {

            if (file == null)
            {
                ModelState.AddModelError("CustomError", "Please select CV");
                return View();
            }

            if (!(file.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" ||
                file.ContentType == "application/pdf"))
            {
                ModelState.AddModelError("CustomError", "Only .docx and .pdf file allowed");
                return View();
            }
            

            if (ModelState.IsValid)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Path.Combine(Server.MapPath("~/UploadedCV"), fileName));

                candidate.ResumePath = fileName;
                db.Candidates.Add(candidate);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            ViewBag.PostID = new SelectList(db.Posts, "PostID", "PostName", candidate.PostID);
            ViewBag.TechnologiesWorkedOnID = new SelectList(db.Technologies, "TechID", "Technology1", candidate.TechnologiesWorkedOnID);
            return View(candidate);
        }

        // GET: Candidates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostID = new SelectList(db.Posts, "PostID", "PostName", candidate.PostID);
            ViewBag.TechnologiesWorkedOnID = new SelectList(db.Technologies, "TechID", "Technology1", candidate.TechnologiesWorkedOnID);
            return View(candidate);
        }

        // POST: Candidates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CandidateID,FirstName,MiddleName,LastName,DateOfBirth,Address,Experience,TechnologiesWorkedOnID,PostID,MobileNumber,EmailAddress")] Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PostID = new SelectList(db.Posts, "PostID", "PostName", candidate.PostID);
            ViewBag.TechnologiesWorkedOnID = new SelectList(db.Technologies, "TechID", "Technology1", candidate.TechnologiesWorkedOnID);
            return View(candidate);
        }

        // GET: Candidates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candidate candidate = db.Candidates.Find(id);
            db.Candidates.Remove(candidate);
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
