using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Interview.Domain.Model;

namespace InterviewSelectionProcess.Controllers
{
    public class CandidateStatusController : Controller
    {
        private InterviewSelectionProcessContext db = new InterviewSelectionProcessContext();

        // GET: CandidateStatus
        public ActionResult Index(string SearchPost,string SearchTech,int? SearchExperience)
        {
            var candidateStatus = db.CandidateStatus.Include(c => c.Candidate).Include(c => c.InterviewStatu).Include(c => c.SelectionStatu);

        //    if (!String.IsNullOrEmpty(SearchPost) && !String.IsNullOrEmpty(SearchTech) && !(SearchExperience==null))
          //  {
                candidateStatus = candidateStatus.Where(s =>( s.Candidate.Post.PostName.Contains(SearchPost) || String.IsNullOrEmpty(SearchPost)) && (s.Candidate.Technology.Technology1.Contains(SearchTech) || String.IsNullOrEmpty(SearchTech)) && (s.Candidate.Experience==SearchExperience|| SearchExperience == null));
            //}

            return View(candidateStatus.ToList());
        }

        public ActionResult Autocomplete(string term)
        {
            var model = db.Posts.Where(s => s.PostName.StartsWith(term)).Select(r=>new { label=r.PostName});
            return Json(model,JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutocompleteTech(string term)
        {
            var model = db.Technologies.Where(s => s.Technology1.StartsWith(term)).Select(r => new { label = r.Technology1 });
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutocompleteExp(string term)
        {
            var model = db.Candidates.Where(s => s.Experience.ToString().StartsWith(term)).Select(r => new { label = r.Experience }).Distinct();
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        // GET: CandidateStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateStatu candidateStatu = db.CandidateStatus.Find(id);
         //   Candidate candidate = db.Candidates.Find(candidateStatu.CandidateID);
            if (candidateStatu == null)
            {
                return HttpNotFound();
            }
            return View(candidateStatu);
        }

        // GET: CandidateStatus/Create
        public ActionResult Create(int CandidateID)
        {
            ViewBag.CandidateID = new SelectList(db.Candidates.Where(x=>x.CandidateID==CandidateID), "CandidateID", "FirstName");

            if (db.CandidateStatus.Where(x=>x.CandidateID==CandidateID).Select(y=>y.InterviewStatus).Single()<3)
                ViewBag.InterviewStatus = new SelectList(db.InterviewStatus.Where(x => x.InterviewStatusID < 3), "InterviewStatusID", "InterviewStatusName");
            else
                ViewBag.InterviewStatus = new SelectList(db.CandidateStatus.Where(x=>x.CandidateID==CandidateID).Select(x=>x.InterviewStatu), "InterviewStatusID", "InterviewStatusName");

            if (db.CandidateStatus.Where(x => x.CandidateID == CandidateID).Select(y => y.SelectionStatus).Single() < 4)
                ViewBag.SelectionStatus = new SelectList(db.SelectionStatus.Where(x=>x.SelectionStatusID<4), "SelectionStatusID", "SelectionStatusName");
            else
                ViewBag.SelectionStatus = new SelectList(db.CandidateStatus.Where(x => x.CandidateID == CandidateID).Select(x => x.SelectionStatu), "SelectionStatusID", "SelectionStatusName");

            return View();
        }

        // POST: CandidateStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CandidateStatusID,CandidateID,InterviewStatus,SelectionStatus")] CandidateStatu candidateStatu)
        {
            if (ModelState.IsValid)
            {
                db.CandidateStatus.Add(candidateStatu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CandidateID = new SelectList(db.Candidates, "CandidateID", "FirstName", candidateStatu.CandidateID);
            ViewBag.InterviewStatus = new SelectList(db.InterviewStatus, "InterviewStatusID", "InterviewStatusName", candidateStatu.InterviewStatus);
            ViewBag.SelectionStatus = new SelectList(db.SelectionStatus, "SelectionStatusID", "SelectionStatusName", candidateStatu.SelectionStatus);
            return View(candidateStatu);
        }

        // GET: CandidateStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateStatu candidateStatu = db.CandidateStatus.Find(id);
            if (candidateStatu == null)
            {
                return HttpNotFound();
            }
            ViewBag.CandidateID = new SelectList(db.Candidates, "CandidateID", "FirstName", candidateStatu.CandidateID);
            ViewBag.InterviewStatus = new SelectList(db.InterviewStatus, "InterviewStatusID", "InterviewStatusName", candidateStatu.InterviewStatus);
            ViewBag.SelectionStatus = new SelectList(db.SelectionStatus, "SelectionStatusID", "SelectionStatusName", candidateStatu.SelectionStatus);
            return View(candidateStatu);
        }

        // POST: CandidateStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CandidateStatusID,CandidateID,InterviewStatus,SelectionStatus")] CandidateStatu candidateStatu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidateStatu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CandidateID = new SelectList(db.Candidates, "CandidateID", "FirstName", candidateStatu.CandidateID);
            ViewBag.InterviewStatus = new SelectList(db.InterviewStatus, "InterviewStatusID", "InterviewStatusName", candidateStatu.InterviewStatus);
            ViewBag.SelectionStatus = new SelectList(db.SelectionStatus, "SelectionStatusID", "SelectionStatusName", candidateStatu.SelectionStatus);
            return View(candidateStatu);
        }

        // GET: CandidateStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateStatu candidateStatu = db.CandidateStatus.Find(id);
            if (candidateStatu == null)
            {
                return HttpNotFound();
            }
            return View(candidateStatu);
        }

        // POST: CandidateStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CandidateStatu candidateStatu = db.CandidateStatus.Find(id);
            db.CandidateStatus.Remove(candidateStatu);
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
