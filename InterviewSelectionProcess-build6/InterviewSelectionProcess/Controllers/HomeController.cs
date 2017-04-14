using InterviewSelectionProcess.Models;
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
          
           ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name");

            return View();
        }


        public ActionResult Edit(int? EmployeeID)
        {
           
            var interviewSchedules = db.InterviewSchedules.Where(x => x.EmployeeID == EmployeeID && x.Note == "");
            ViewBag.Message = "Your contact page.";

            return View(interviewSchedules);
        }

        [HttpPost]
        public ActionResult Edit(InterviewSchedule interviewSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interviewSchedule).State = EntityState.Modified;
                db.SaveChanges();
                
            }
            ModelState.Clear();
            var interviewSchedules = db.InterviewSchedules.Where(x => x.EmployeeID == interviewSchedule.EmployeeID && x.Note=="");
            

            return View(interviewSchedules);
        }
    }
}