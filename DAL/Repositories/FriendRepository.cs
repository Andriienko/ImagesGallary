using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.EF;
using DAL.Entities;

namespace DAL.Repositories
{
    public class FriendRepository
    {
        private ApplicationContext Database;
        public FriendRepository(ApplicationContext db)
        {
            Database = db;
        }
        public void Create(Friend newFriend)
        {
            Database.Friends.Add(newFriend);
            Database.SaveChanges();
        }
        public IEnumerable<Friend> GetAll()
        {
            return Database.Friends;
        }

        public Friend Get(int id)
        {
            return Database.Friends.Find(id);
        }

        public void Delete(int id)
        {
            var friend = Database.Friends.Find(id);
            if (friend != null)
                Database.Friends.Remove(friend);
            Database.SaveChanges();
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
