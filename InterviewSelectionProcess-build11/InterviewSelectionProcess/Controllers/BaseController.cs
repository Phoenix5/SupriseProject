using Interview.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interview.Domain.MVC.Controllers
{
    public class BaseController : Controller
    {

        private InterviewSelectionProcessContext db = new InterviewSelectionProcessContext();

        public string GetLoggedInUserId()
        {
            string id;
            var y = HttpContext.User.Identity.Name;
            if (User.Identity.IsAuthenticated && Session["UserID"] == null)
            {
                id = db.Users.Where(x => x.UserName == y).Select(x => x.Id).Single();
                Session["UserID"] = id;
            }
            else
            {
                id = (string)Session["UserID"];
            }
            return id;
        }
    }
}