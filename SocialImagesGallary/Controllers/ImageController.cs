using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SocialImagesGallary.Controllers
{
    public class ImageController : Controller
    {
        private IImageService ImageService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IImageService>();
            }
        }
        [HttpPost]
        public ActionResult AddImage(HttpPostedFileBase file,string title)
        {
            var userName = User.Identity.Name;
            try
            {
                if (file.ContentLength > 0)
                {
                    string[] keys = HttpContext.Request.Cookies.AllKeys;
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/Images"), userName);
                    bool existFolder = System.IO.Directory.Exists(path);
                    if (!existFolder)
                    {
                        Directory.CreateDirectory(path);
                    }
                    path = Path.Combine(path, fileName);
                    file.SaveAs(path);
                    var imageDto=new ImageDTO {
                        Title =title,
                        Path=path
                    };
                    ImageService.AddImage(imageDto,userName);
                }
                ViewBag.Message = "Upload successful";
                return RedirectToAction("Index","Home");
            }
            catch
            {
                ViewBag.Message = "Upload failed";
                string[] keys = HttpContext.Request.Cookies.AllKeys;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult GetAllImages()
        {
            var userName = User.Identity.Name;
            var allImages=ImageService.GetAllImages(userName);
            return Json(allImages,JsonRequestBehavior.AllowGet);
        }
    }
}