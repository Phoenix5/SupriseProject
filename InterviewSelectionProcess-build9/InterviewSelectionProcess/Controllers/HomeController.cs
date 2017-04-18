using Interview.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterviewSelectionProcess.Controllers
{
    public class HomeController : Controller
    {
        private InterviewSelectionProcessContext db = new InterviewSelectionProcessContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Employee()
        {
          
           ViewBag.EmployeeID = new SelectList(db.Employees.Where(x=>x.InterviewSchedules.Count()>0), "EmployeeID", "Name");

            return View();
        }


        public ActionResult Edit(int? EmployeeID)
        {
           
            var interviewSchedules = db.InterviewSchedules.Where(x => x.EmployeeID == EmployeeID);

           ViewBag.candidatestatus = db.CandidateStatus.Where(x => x.InterviewStatus > 1).Select(y => y.InterviewStatu).Distinct();
          //  ViewBag.InterviewStatus = new SelectList(db.CandidateStatus.Where(x=>x.InterviewStatus>3).Select(y=>y.InterviewStatu), "InterviewStatusID", "InterviewStatusName"); 

            return View(interviewSchedules);
        }

        [HttpPost]
        public ActionResult Edit(InterviewSchedule interviewSchedule , int InterviewStatus)
        {
            if (ModelState.IsValid)
            {
                CandidateStatu i = db.CandidateStatus.Where(x => x.CandidateID == interviewSchedule.CandidateID).Single();
                i.InterviewStatus = InterviewStatus;
                db.Entry(i).State = EntityState.Modified;
                db.Entry(interviewSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit","Home",new { EmployeeID=interviewSchedule.EmployeeID});
            }
            //ModelState.Clear();
            var interviewSchedules = db.InterviewSchedules.Where(x => x.EmployeeID == interviewSchedule.EmployeeID);

            ViewBag.candidatestatus = db.CandidateStatus.Where(x => x.InterviewStatus > 1).Select(y => y.InterviewStatu).Distinct();


            return View(interviewSchedules);
        }
    }
}