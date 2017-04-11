using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InterviewSelectionProcess.Models;

namespace InterviewSelectionProcess.Controllers
{
    public class InterviewSchedulesController : Controller
    {
        private InterviewSelectionProcessContext db = new InterviewSelectionProcessContext();

        // GET: InterviewSchedules
        public ActionResult Index()
        {
            var interviewSchedules = db.InterviewSchedules.Include(i => i.Candidate).Include(i => i.Employee);
            return View(interviewSchedules.ToList());
        }

        // GET: InterviewSchedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewSchedule interviewSchedule = db.InterviewSchedules.Find(id);
            if (interviewSchedule == null)
            {
                return HttpNotFound();
            }
            return View(interviewSchedule);
        }

        // GET: InterviewSchedules/Create
        public ActionResult Create()
        {
            IList<int> l = (from y in db.InterviewSchedules select y.CandidateID).ToList();
            IList < Candidate > li = (from x in db.CandidateStatus
                                      where x.SelectionStatu.SelectionStatusID >= 3 && !(l.Contains(x.CandidateID))
                                      select x.Candidate).ToList();
            ViewBag.CandidateID = new SelectList(li, "CandidateID", "FirstName");
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name");
            return View();
        }

        // POST: InterviewSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InterviewID,EmployeeID,HRID,Note,InterviewDate,CandidateID")] InterviewSchedule interviewSchedule)
        {
            if (ModelState.IsValid)
            {
                db.InterviewSchedules.Add(interviewSchedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CandidateID = new SelectList(db.Candidates, "CandidateID", "FirstName", interviewSchedule.CandidateID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name", interviewSchedule.EmployeeID);
            return View(interviewSchedule);
        }

        // GET: InterviewSchedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewSchedule interviewSchedule = db.InterviewSchedules.Find(id);
            if (interviewSchedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.CandidateID = new SelectList(db.Candidates, "CandidateID", "FirstName", interviewSchedule.CandidateID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name", interviewSchedule.EmployeeID);
            return View(interviewSchedule);
        }

        // POST: InterviewSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InterviewID,EmployeeID,HRID,Note,InterviewDate,CandidateID")] InterviewSchedule interviewSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interviewSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CandidateID = new SelectList(db.Candidates, "CandidateID", "FirstName", interviewSchedule.CandidateID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name", interviewSchedule.EmployeeID);
            return View(interviewSchedule);
        }

        // GET: InterviewSchedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewSchedule interviewSchedule = db.InterviewSchedules.Find(id);
            if (interviewSchedule == null)
            {
                return HttpNotFound();
            }
            return View(interviewSchedule);
        }

        // POST: InterviewSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InterviewSchedule interviewSchedule = db.InterviewSchedules.Find(id);
            db.InterviewSchedules.Remove(interviewSchedule);
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
