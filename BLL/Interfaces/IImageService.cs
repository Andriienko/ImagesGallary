using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
   public  interface IImageService:IDisposable
    {
        Task<bool> AddImage(ImageDTO newImage,string userName);
        Task<bool> RemoveImage(ImageDTO image,string userName);
        Task<bool> RenameImage(ImageDTO image, string userName);
        Image GetImageById(int id,string userName);
        IEnumerable<Image> GetAllImages(string userName);
    }
}
