using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Upload.Models;

namespace Upload.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new MyViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(MyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            byte[] uploadedFile = new byte[model.File.InputStream.Length];
            model.File.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

            // now you could pass the byte array to your model and store wherever 
            // you intended to store it

            return File(uploadedFile, "application/"+ uploadedFile.GetType());
        }
    }
}