using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class MessageRepository:IRepository<Message>,IDisposable
    {
        public ApplicationContext Database { get; set; }
        public MessageRepository(ApplicationContext db)
        {
            Database = db;
        }
        public void Create(Message item)
        {
            Database.Messages.Add(item);
            Database.SaveChanges();
        }
        public IEnumerable<Message> GetAll()
        {
            return Database.Messages;
        }

        public Message Get(string id)
        {
            return Database.Messages.Find(Int32.Parse(id));
        }

        public void Update(Message item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<Message> Find(Func<Message, bool> predicate)
        {
            return Database.Messages.Where(predicate).ToList();
        }

        public void Delete(string id)
        {
            var message = Database.Messages.Find(Int32.Parse(id));
            if (message != null)
               Database.Messages.Remove(message);
            Database.SaveChanges();
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
