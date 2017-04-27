using Interview.Domain.Model;
using Interview.Domain.MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterviewSelectionProcess.Controllers
{
    [Authorize(Roles = "Employee")]
    public class HomeController : BaseController
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private InterviewSelectionProcessContext db = new InterviewSelectionProcessContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            logger.Error("Visited Index:");
            return View();
        }

       

        public ActionResult Edit(/*string EmployeeID*/)
        {
            logger.Error("Visited Edit:");
            var id = GetLoggedInUserId();
            string EmployeeID = db.Employees.Where(x=>x.ID==id).Select(x=>x.ID).Single();
            var interviewSchedules = db.InterviewSchedules.Where(x => x.EmployeeID == EmployeeID);

           ViewBag.candidatestatus = db.CandidateStatuses.Where(x => x.InterviewStatusID > 1).Select(y => y.InterviewStatus).Distinct();
          // ViewBag.InterviewStatus = new SelectList(db.CandidateStatus.Where(x=>x.InterviewStatus>3).Select(y=>y.InterviewStatu), "InterviewStatusID", "InterviewStatusName"); 

            return View(interviewSchedules);
        }

        [HttpPost]
        public ActionResult Edit(InterviewSchedule interviewSchedule , int InterviewStatus)
        {
            logger.Error("Attempted Edit:");
            if (ModelState.IsValid)
            {
                CandidateStatus i = db.CandidateStatuses.Where(x => x.CandidateID == interviewSchedule.CandidateID).First();
                i.InterviewStatusID = InterviewStatus;
                db.Entry(i).State = EntityState.Modified;
                db.Entry(interviewSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit","Home",new { EmployeeID=interviewSchedule.EmployeeID});
            }
            //ModelState.Clear();
            var interviewSchedules = db.InterviewSchedules.Where(x => x.EmployeeID == interviewSchedule.EmployeeID);

            ViewBag.candidatestatus = db.CandidateStatuses.Where(x => x.InterviewStatusID > 1).Select(y => y.InterviewStatus).Distinct();


            return View(interviewSchedules);
        }
    }
}