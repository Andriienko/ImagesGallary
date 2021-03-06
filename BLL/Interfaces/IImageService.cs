﻿using BLL.DTOs;
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
        bool AddImage(ImageDTO newImage,string userName);
        bool RemoveImage(ImageDTO image,string userName);
        bool RenameImage(ImageDTO image, string userName);
        MessageDTO AddMessage(MessageDTO newmsg,string userName);
        ImageDTO GetImageById(int id,string userName);
        IEnumerable<ImageDTO> GetAllImages(string userName);
        IEnumerable<MessageDTO> GetAllMessages(int imgId);
       int GetImagesCount(string userName);
    }
}
