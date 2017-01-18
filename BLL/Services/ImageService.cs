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
        public async Task<bool> AddImage(ImageDTO newImage,string userName)
        {
            var user=await Database.UserManager.FindByNameAsync(userName);
            if (user != null)
            {
                var image = new Image {
                    Title=newImage.Title,
                    Path=newImage.Path,
                    UserId=user.Id
                };
                Database.Images.Create(image);
                await Database.SaveAsync();
                return true;
            }
            return false;
        }

        public IEnumerable<Image> GetAllImages(string userName)
        {
            var user = Database.UserManager.FindByName(userName);
            if (user != null)
            {
                var allImages= user.Images;
                return allImages;
            }
            return new List<Image>();
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

        public async Task<bool> RemoveImage(ImageDTO image,string userName)
        {
            var user = await Database.UserManager.FindByNameAsync(userName);
            if (user != null)
            {
                var img = user.Images.FirstOrDefault(i => i.Id == image.Id);
                if (img != null)
                {
                    user.Images.Remove(img);
                    await Database.SaveAsync();
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> RenameImage(ImageDTO image, string userName)
        {
            var user = await Database.UserManager.FindByNameAsync(userName);
            if (user != null)
            {
                var img = user.Images.FirstOrDefault(i=>i.Id==image.Id);
                img.Title = image.Title;
                await Database.SaveAsync();
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
