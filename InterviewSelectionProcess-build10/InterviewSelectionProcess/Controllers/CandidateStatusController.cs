using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Interview.Domain.Model;
using PagedList;

namespace InterviewSelectionProcess.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CandidateStatusController : Controller
    {
        private InterviewSelectionProcessContext db = new InterviewSelectionProcessContext();

        // GET: CandidateStatus
        public ActionResult Index(string SearchPost,string SearchTech,int? SearchExperience,int page=1)
        {
            var candidateStatus = db.CandidateStatuses.Include(c => c.Candidate).Include(c => c.InterviewStatus).Include(c => c.SelectionStatus);

        //    if (!String.IsNullOrEmpty(SearchPost) && !String.IsNullOrEmpty(SearchTech) && !(SearchExperience==null))
          //  {
                candidateStatus = candidateStatus.Where(s =>( s.Candidate.Post.PostName.Contains(SearchPost) || String.IsNullOrEmpty(SearchPost)) && (s.Candidate.Technology.TechnologyName.Contains(SearchTech) || String.IsNullOrEmpty(SearchTech)) && (s.Candidate.Experience==SearchExperience|| SearchExperience == null)).OrderBy(x=>x.CandidateID);
            //}

            return View(candidateStatus.ToPagedList(page,10));
        }

        public ActionResult Autocomplete(string term)
        {
            var model = db.Posts.Where(s => s.PostName.StartsWith(term)).Select(r=>new { label=r.PostName});
            return Json(model,JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutocompleteTech(string term)
        {
            var model = db.Technologies.Where(s => s.TechnologyName.StartsWith(term)).Select(r => new { label = r.TechnologyName });
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutocompleteExp(string term)
        {
            var model = db.Candidates.Where(s => s.Experience.ToString().StartsWith(term)).Select(r => new { label = r.Experience }).Distinct();
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        // GET: CandidateStatus/Details/5
        public ActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            CandidateStatus candidateStatus = db.CandidateStatuses.Find(id);
            //   Candidate candidate = db.Candidates.Find(candidateStatu.CandidateID);
            if (candidateStatus == null)
            {
                return HttpNotFound();
            }
            return View(candidateStatus);
        }

        // GET: CandidateStatus/Create
        public ActionResult Create(string CandidateID=null)
        {
            ViewBag.CandidateID = new SelectList(db.Candidates.Where(x=>x.ID==CandidateID ||(CandidateID==null)), "ID", "FirstName");


            if (CandidateID != null)
            {
                if (db.CandidateStatuses.Where(x => x.CandidateID == CandidateID).Select(y => y.InterviewStatusID).Single() < 3)
                    ViewBag.InterviewStatusID = new SelectList(db.InterviewStatuses.Where(x => x.InterviewStatusID < 3), "InterviewStatusID", "InterviewStatusName");
                else
                    ViewBag.InterviewStatusID = new SelectList(db.CandidateStatuses.Where(x => x.CandidateID == CandidateID).Select(x => x.InterviewStatus), "InterviewStatusID", "InterviewStatusName");

                if (db.CandidateStatuses.Where(x => x.CandidateID == CandidateID).Select(y => y.SelectionStatusID).Single() < 4)
                    ViewBag.SelectionStatusID = new SelectList(db.SelectionStatuses.Where(x => x.SelectionStatusID < 4), "SelectionStatusID", "SelectionStatusName");
                else
                    ViewBag.SelectionStatusID = new SelectList(db.CandidateStatuses.Where(x => x.CandidateID == CandidateID).Select(x => x.SelectionStatus), "SelectionStatusID", "SelectionStatusName");


            }
            else
            {
                
                    ViewBag.InterviewStatusID = new SelectList(db.InterviewStatuses, "InterviewStatusID", "InterviewStatusName");
               
                    ViewBag.SelectionStatusID = new SelectList(db.SelectionStatuses, "SelectionStatusID", "SelectionStatusName");

            }
            return View();
        }

        // POST: CandidateStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CandidateStatusID,CandidateID,InterviewStatusID,SelectionStatusID")] CandidateStatus candidateStatus)
        {
            if (ModelState.IsValid)
            {
                db.CandidateStatuses.Add(candidateStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CandidateID = new SelectList(db.Candidates, "CandidateID", "FirstName", candidateStatus.CandidateID);
            ViewBag.InterviewStatusID = new SelectList(db.InterviewStatuses, "InterviewStatusID", "InterviewStatusName", candidateStatus.InterviewStatusID);
            ViewBag.SelectionStatusID = new SelectList(db.SelectionStatuses, "SelectionStatusID", "SelectionStatusName", candidateStatus.SelectionStatusID);
            return View(candidateStatus);
        }

        // GET: CandidateStatus/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CandidateStatu candidateStatu = db.CandidateStatus.Find(id);
        //    if (candidateStatu == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CandidateID = new SelectList(db.Candidates, "CandidateID", "FirstName", candidateStatu.CandidateID);
        //    ViewBag.InterviewStatus = new SelectList(db.InterviewStatus, "InterviewStatusID", "InterviewStatusName", candidateStatu.InterviewStatus);
        //    ViewBag.SelectionStatus = new SelectList(db.SelectionStatus, "SelectionStatusID", "SelectionStatusName", candidateStatu.SelectionStatus);
        //    return View(candidateStatu);
        //}

        // POST: CandidateStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CandidateStatusID,CandidateID,InterviewStatusID,SelectionStatusID")] CandidateStatus candidateStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidateStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CandidateID = new SelectList(db.Candidates, "CandidateID", "FirstName", candidateStatus.CandidateID);
            ViewBag.InterviewStatus = new SelectList(db.InterviewStatuses, "InterviewStatusID", "InterviewStatusName", candidateStatus.InterviewStatusID);
            ViewBag.SelectionStatus = new SelectList(db.SelectionStatuses, "SelectionStatusID", "SelectionStatusName", candidateStatus.SelectionStatusID);
            return View(candidateStatus);
        }

        // GET: CandidateStatus/Delete/5
        public ActionResult Delete(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            CandidateStatus candidateStatus = db.CandidateStatuses.Find(id);
            if (candidateStatus == null)
            {
                return HttpNotFound();
            }
            return View(candidateStatus);
        }

        // POST: CandidateStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CandidateStatus candidateStatus = db.CandidateStatuses.Find(id);
            db.CandidateStatuses.Remove(candidateStatus);
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
