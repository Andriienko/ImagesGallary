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
    class ImageRepository:IRepository<Image>
    {
        public ApplicationContext Database { get; set; }
        public ImageRepository(ApplicationContext db)
        {
            Database = db;
        }
        public void Create(Image item)
        {
            Database.Images.Add(item);
            Database.SaveChanges();
        }
        public IEnumerable<Image> GetAll()
        {
            return Database.Images;
        }

        public Image Get(string id)
        {
            return Database.Images.Find(Int32.Parse(id));
        }

        public void Update(Image item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<Image> Find(Func<Image, bool> predicate)
        {
            return Database.Images.Where(predicate).ToList();
        }

        public void Delete(string id)
        {
            var image = Database.Images.Find(Int32.Parse(id));
            if (image != null)
                Database.Images.Remove(image);
            Database.SaveChanges();
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
