using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IProfileManager : IDisposable
    {
        void Create(UserProfile item);
        bool UploadImage(string userId,string path);
        string LoadAvatar(string userId);
        UserProfile GetProfile(string userName);
    }
}
