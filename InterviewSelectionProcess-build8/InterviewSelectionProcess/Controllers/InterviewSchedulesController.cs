using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Interview.Domain.Model;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

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
        public ActionResult Create(DateTime? InterviewDate)
        {
            IList<int> l = (from y in db.InterviewSchedules select y.CandidateID).ToList();
            IList < Candidate > li = (from x in db.CandidateStatus
                                      where x.SelectionStatu.SelectionStatusID >= 3 && !(l.Contains(x.CandidateID))
                                      select x.Candidate).ToList();
            ViewBag.CandidateID = new SelectList(li, "CandidateID", "FirstName");


            //IList<Employee> li2=(from x in db.InterviewSchedules
            //                     group x by x.InterviewDate into g
            //                     select g.Max()).ToList()
            // ViewBag.InterviewDate = InterviewDate;
            IList<Employee> l2 = (from x in db.Employees
                                  where x.InterviewSchedules.Where(y=>y.InterviewDate==InterviewDate).Count()<2            
                                  select x).ToList();


            ViewBag.EmployeeID = new SelectList(l2, "EmployeeID", "Name");
            return View();
        }

        // POST: InterviewSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create([Bind(Include = "InterviewID,EmployeeID,HRID,Note,InterviewDate,CandidateID")] InterviewSchedule interviewSchedule)
        {
            if (ModelState.IsValid)
            {
                db.InterviewSchedules.Add(interviewSchedule);
                db.SaveChanges();


                //mailling code start
                var body = "<p>Email From: Admin (itsupport@creativecapsule.com)</p><p>Message:Dear {0} your interview will be taken by {1} on {2} at 10:30am arrive on time.</p><p>Admin</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(db.Candidates.Where(x=>x.CandidateID== interviewSchedule.CandidateID).Select(x=>x.EmailAddress).Single()));  // replace with valid value 
                message.To.Add(new MailAddress(db.Employees.Where(x=>x.EmployeeID== interviewSchedule.EmployeeID).Select(x=>x.EmailID).Single()));  // replace with valid value 
               // message.To.Add(new MailAddress(interviewSchedule.Employee.EmailID.Where());  // replace with valid value 
                message.From = new MailAddress("itsupport@creativecapsule.com");  // replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, interviewSchedule.CandidateID, interviewSchedule.EmployeeID, interviewSchedule.InterviewDate);
                message.IsBodyHtml = true;


                ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object s, X509Certificate certificate,
                               X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    { return true; };


                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "",  // replace with valid value
                        Password = ""  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "zuari.creativecapsule.local";
                    smtp.Port = 25;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                   // return RedirectToAction("Sent");
                }


                //Mailing code end
                return RedirectToAction("Index", "CandidateStatus");
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
