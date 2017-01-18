using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ProfileManager : IProfileManager
    {
        public ApplicationContext Database { get; set; }
        public ProfileManager(ApplicationContext db)
        {
            Database = db;
        }
        public void Create(UserProfile item)
        {
            Database.UserProfiles.Add(item);
            Database.SaveChanges();
        }

        public UserProfile GetProfile(string userId)
        {
            var profile = Database.UserProfiles.FirstOrDefault(p => p.Id == userId);
            return profile;
        }

        public bool UploadImage(string userId,string path)
        {
            var profile = Database.UserProfiles.FirstOrDefault(u => u.Id == userId);
            if (profile != null)
            {
                profile.UserImage = path;
                Database.SaveChanges();
                return true;
            }
            return false;
        }

        public string LoadAvatar(string userId)
        {
            var profile = Database.UserProfiles.FirstOrDefault(u => u.Id == userId);
            if (profile != null)
            {
                return profile.UserImage;
            }
            return  String.Empty;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
