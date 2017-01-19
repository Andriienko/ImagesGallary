using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
   public class ImageDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Path { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public IEnumerable<MessageDTO> Messages { get; set; }

       public static IEnumerable<ImageDTO> CreateMany(IEnumerable<Image> images)
       {
            List<ImageDTO> imageDtos=new List<ImageDTO>();
           foreach (var image in images)
           {
               var imgDto=new ImageDTO
               {
                   Id=image.Id,
                   Title = image.Title,
                   Path = image.Path,
                   UserId = image.UserId,
                   UserName = image.User.UserName,
                   Messages = MessageDTO.CreateMany(image.Messages)
               };
                imageDtos.Add(imgDto);
           }
           return imageDtos;
       }
        public static ImageDTO Create(Image image)
        {
                var imgDto = new ImageDTO
                {
                    Id = image.Id,
                    Title = image.Title,
                    Path = image.Path,
                    UserId = image.UserId,
                    UserName = image.User.UserName,
                    Messages = MessageDTO.CreateMany(image.Messages)
                };
            return imgDto;
        }
    }
}
