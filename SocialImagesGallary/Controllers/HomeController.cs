using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialImagesGallary.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var userName = User.Identity.Name;
            @ViewData["UserName"] = userName;
            return View();
        }
    }
}