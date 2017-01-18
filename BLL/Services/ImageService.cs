using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using BLL.DTOs;

namespace BLL.Services
{
    public class ImageService : IImageService
    {
        IUnitOfWork Database { get; set; }
        public ImageService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public bool AddImage(ImageDTO newImage,string userName)
        {
            var user= Database.UserManager.FindByName(userName);
            if (user != null)
            {
                var image = new Image {
                    Title=newImage.Title,
                    Path=newImage.Path,
                    UserId=user.Id
                };
                Database.Images.Create(image);
                Database.SaveAsync();
                return true;
            }
            return false;
        }

        public IEnumerable<ImageDTO> GetAllImages(string userName)
        {
            var user = Database.UserManager.FindByName(userName);
            if (user != null)
            {
                var allImages= user.Images;
                return ImageDTO.CreateMany(allImages);
            }
            return new List<ImageDTO>();
            }

        public Image GetImageById(int id,string userName)
        {
            var user = Database.UserManager.FindByName(userName);
            if (user != null)
            {
                return user.Images.FirstOrDefault(i=>i.Id==id);
            }
            return null;
        }

        public bool RemoveImage(ImageDTO image,string userName)
        {
            var user = Database.UserManager.FindByName(userName);
            if (user != null)
            {
                var img = user.Images.FirstOrDefault(i => i.Id == image.Id);
                if (img != null)
                {
                    user.Images.Remove(img);
                    Database.SaveAsync();
                    return true;
                }
            }
            return false;
        }
        public bool RenameImage(ImageDTO image, string userName)
        {
            var user = Database.UserManager.FindByName(userName);
            if (user != null)
            {
                var img = user.Images.FirstOrDefault(i=>i.Id==image.Id);
                img.Title = image.Title;
                Database.SaveAsync();
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
