using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Upload.Models
{
    public class MyViewModel
    {
        [Required]
        public HttpPostedFileBase File { get; set; }
    }
}