using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.IO;

namespace SocialImagesGallary.Controllers
{
    public class ProfileController : Controller
    {
        private IUserService UserService
        {
            get { return HttpContext.GetOwinContext().GetUserManager<IUserService>(); }
        }

        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadUserImage(HttpPostedFileBase file)
        {
            if (file != null)
            {
                var userName = User.Identity.Name;
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/Images/" + userName), "Avatar");
                    bool existFolder = System.IO.Directory.Exists(path);
                    if (!existFolder)
                    {
                        Directory.CreateDirectory(path);
                    }
                    path = Path.Combine(path, fileName);
                    file.SaveAs(path);
                    UserService.UploadUserImage(userName, path);
                }
            }
            return RedirectToAction("Index");
            //return Json(200, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult RenderAvatar()
        {
            var userName = User.Identity.Name;
            var path = UserService.RenderAvatar(userName);
            if (String.IsNullOrEmpty(path))
            {
               path = Path.Combine(Server.MapPath("~/Content/Images"),"ava.jpg");
            }
            byte[] image = System.IO.File.ReadAllBytes(path);
            return File(image, "image/jpg");
        }
        [HttpGet]
        public ActionResult GetProfile()
        {
            var userName = User.Identity.Name;
            var profile = UserService.GetProfile(userName);
            return Json(profile, JsonRequestBehavior.AllowGet);
        }
    }
}