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

        public ImageDTO GetImageById(int id,string userName)
        {
            var user = Database.UserManager.FindByName(userName);
            if (user != null)
            {
                var count = user.Images.Count;
                if (id < 0)
                    id = 0;
                else if(id>count-1)
                {
                    id = count-1;
                }
                Image image= user.Images.ElementAtOrDefault(id);
                var imageDto = ImageDTO.Create(image);
                return imageDto;
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
        //Message block
        public MessageDTO AddMessage(MessageDTO newmsg,string userName)
        {
            var user = Database.UserManager.FindByName(userName);
            if (user != null)
            {
                newmsg.UserId = user.Id;
                newmsg.SendTime=DateTime.Now.ToUniversalTime().ToString();
                newmsg.UserName = userName;
            }
            var image = MessageDTO.ConvertToMessage(newmsg);
            Database.Messages.Create(image);
            return newmsg;
        }

        public IEnumerable<MessageDTO> GetAllMessages(int imgId)
        {
            var img=Database.Images.GetAll().FirstOrDefault(i=>i.Id==imgId);
            var foo = Database.Messages.GetAll().Where(m => m.ImageId == imgId);
            var allMessages = img.Messages.ToList();
            var allDtos = MessageDTO.CreateMany(foo.ToList());
            return allDtos;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
