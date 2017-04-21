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
using Interview.Domain.MVC.Controllers;

namespace InterviewSelectionProcess.Controllers
{
    [Authorize(Roles = "Admin,Candidate")]
    public class CandidatesController : BaseController
    {
        public CandidatesController()
        {
            //var y = HttpContext.User.Identity.Name;
            //if (User.Identity.IsAuthenticated || Session["UserID"] == null)
            //{
            //   var id = db.Users.Where(x => x.UserName == y).Select(x => x.Id).Single();
            //    Session["UserID"] = id;
            //}

        }
        private InterviewSelectionProcessContext db = new InterviewSelectionProcessContext();

        // GET: Candidates
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string SearchPost, string SearchTech, int? SearchExperience, int page=1)
        {
            var candidates = db.Candidates.Include(c => c.Post).Include(c => c.Technology).OrderBy(x=>x.ID);
            candidates = candidates.Where(s => (s.Post.PostName.Contains(SearchPost) || String.IsNullOrEmpty(SearchPost)) && (s.Technology.TechnologyName.Contains(SearchTech) || String.IsNullOrEmpty(SearchTech)) && (s.Experience == SearchExperience || SearchExperience == null)).OrderBy(x => x.ID);
            return View(candidates.ToPagedList(page, 5));
        }

        // GET: Candidates/Details/5
        public ActionResult Details(/*int? id*/)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            string id = GetLoggedInUserId();
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
            //ViewBag.UserID = (string)Session["UserID"];
            ViewBag.PostID = new SelectList(db.Posts, "PostID", "PostName");
            ViewBag.TechnologyID = new SelectList(db.Technologies, "TechID", "TechnologyName");
            return View();
        }

        // POST: Candidates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,MiddleName,LastName,DateOfBirth,Address,Experience,TechnologyID,PostID,MobileNumber")] Candidate candidate, HttpPostedFileBase file)
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

                string id = GetLoggedInUserId();

                if (db.Candidates.Where(x => x.ID == id).Count() == 1)
                {
                    var x=db.Candidates.Find(id);
                    x.LastName = candidate.LastName;
                    x.MiddleName = candidate.MiddleName;
                    x.MobileNumber = candidate.MobileNumber;
                    x.PostID =candidate.PostID;
                    x.TechnologyID =candidate.TechnologyID;
                    x.FirstName =candidate.FirstName;
                    x.Experience =candidate.Experience;
                    x.DateOfBirth =candidate.DateOfBirth;
                    x.Address =candidate.Address;
                    x.ResumePath = candidate.ResumePath;
                  
                    db.SaveChanges();
                    return RedirectToAction("Details", "Candidates");
                }
                candidate.ID = (string)Session["UserID"];
                db.Candidates.Add(candidate);

                db.SaveChanges();
                return RedirectToAction("Details", "Candidates");
            }

            //ViewBag.UserID = (string)Session["UserID"];
            ViewBag.PostID = new SelectList(db.Posts, "PostID", "PostName", candidate.PostID);
            ViewBag.TechnologyID = new SelectList(db.Technologies, "TechID", "TechnologyName", candidate.TechnologyID);
            return View(candidate);
        }

        // GET: Candidates/Edit/5
        public ActionResult Edit(/*int? id*/)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}Session["UserID"] 
            string id = (string)Session["UserID"];

            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostID = new SelectList(db.Posts, "PostID", "PostName", candidate.PostID);
            ViewBag.TechnologiesWorkedOnID = new SelectList(db.Technologies, "TechID", "TechnologyName", candidate.TechnologyID);
            return View(candidate);
        }

        // POST: Candidates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,MiddleName,LastName,DateOfBirth,Address,Experience,TechnologyID,PostID,MobileNumber")] Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PostID = new SelectList(db.Posts, "PostID", "PostName", candidate.PostID);
            ViewBag.TechnologiesWorkedOnID = new SelectList(db.Technologies, "TechID", "TechnologyName", candidate.TechnologyID);
            return View(candidate);
        }

        // GET: Candidates/Delete/5
        public ActionResult Delete(/*int? id*/)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            string id = (string)Session["UserID"];

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
