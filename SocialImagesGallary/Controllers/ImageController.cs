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
        public ActionResult Photo()
        {
            return View();
        }
        public ActionResult PartialPhoto(int id=0)
        {
            var model = GetImageHelper(id);
            return PartialView("_PartialPhoto",model);
        }
        [HttpPost]
        public void AddImage(HttpPostedFileBase file,string title)
        {
            var userName = User.Identity.Name;
            try
            {
                if (file.ContentLength > 0)
                {
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
                //ViewBag.Message = "Upload successful";
                //return RedirectToAction("Index","Home");
            }
            catch
            {
                //return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult GetAllImages()
        {
            var userName = User.Identity.Name;
            var allImages=ImageService.GetAllImages(userName);
            return Json(allImages,JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetImage(int id=0)
        {
            var imag = GetImageHelper(id);
            return Json(imag, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RenderImage(string path)
        {
            byte[] image = System.IO.File.ReadAllBytes(path);
            return File(image, "image/jpg");
        }
        public ActionResult GetAllMessages(int imgId=0)
        {
            var userName = User.Identity.Name;
            var allImages=ImageService.GetAllMessages(imgId);
            return Json(allImages, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddMessage(MessageDTO newMessage)
        {
            var userName = User.Identity.Name;
            var msg=ImageService.AddMessage(newMessage,userName);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        
        private ImageDTO GetImageHelper(int id)
        {
            var userName = User.Identity.Name;
            var imag = ImageService.GetImageById(id, userName);
            return imag;
        }
    }
}